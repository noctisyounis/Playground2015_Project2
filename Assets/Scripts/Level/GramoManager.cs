// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class GramoManager : MonoBehaviour 
{
	
	// GramoManager manage the collected gramophone pieces interface. Each piece is unique and has a proper index.
	//It needs prefabs icons, 1 more than pieces in the level (the extra piece for good level completion).
	
	
	#region main methods	
	
		void Start ()
		{
			// Gramophone pieces initialization
			initializeGramo ();
			// Camera initialization
			camera = GameObject.FindGameObjectWithTag("Interface").GetComponent<Camera>().transform;
			previousCameraPosition = camera.transform.position;		
			// Icons initialization
			float totalIcons = 5.0f;
			icons = new GameObject[(int)totalIcons];
			for (int i = 0; i < totalIcons; i++) {
				icons[i] = GameObject.FindGameObjectWithTag("GramoIcon" + (i+1));
				if (i % 2 == 0)
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = true;
				}
				else
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = false;
				}
			}	
			ghostIcon = GameObject.FindGameObjectWithTag("GramoIconGhost");
			// Icons display
			hideCoroutine = hideGramoPiecesAfter(displayDelay);
			iconsShown = false;
			showGramo ();
			StartCoroutine(hideCoroutine);	
		}
		
		void Update () 
		{
			// Following of camera
			cameraFollow ();
		}
		
		private void initializeGramo()
		{
			// Get each instance of gramo pieces on the level
			GameObject[] gramoPieces = GameObject.FindGameObjectsWithTag ("GramoPiece");
			float maxGramoInLevel = gramoPieces.Length;

			// Initialize gramo icons
			float totalIcons = 5.0f;
			icons = new GameObject[(int)getMaxGramoPieces()];
			for (int i = 0; i < getMaxGramoPieces(); i++) {
				icons[i] = GameObject.FindGameObjectWithTag("GramoIcon" + (i+1));
				if (getCollectedGramo()[i])
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = true;
				}
				else
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = false;
				}
			}

			// Initialize in game gramo
			float totalSavedGramoPieces = 0f;
			for (int i = 0; i < (maxGramoInLevel); i++)	
			{
				// Get the saved collected properties
				bool gramoIsCollected = getCollectedGramo()[i];
				
				// Set properties
				gramoPieces[i].GetComponent<GramoPieceBehaviour>().setGramoIndex(i);
				gramoPieces[i].GetComponent<GramoPieceBehaviour>().setCollected(gramoIsCollected);
				
				// Update the total collected dust
				if(gramoIsCollected)
				{
					totalSavedGramoPieces++;
				}
			}
			setTotalCollectedGramo(totalSavedGramoPieces);	
		}

		public void collectGramo(float gramoIndex)
		{		
			setTotalCollectedGramo(getTotalCollectedGramo() + 1.0f);
			showGramo();
			updateCoroutine(hideCoroutine, getSlipDelay()+getDisplayDelay());
			slipCollectedGramoPiece (gramoIndex);
			setCollectedGramo (gramoIndex, true);
		}
		
		private void showGramo()
		{
			for (int i = 0; i < getMaxGramoPieces(); i++)
			{	
				if (getCollectedGramo ()[i]) 
				{
					icons [i].GetComponent<SpriteRenderer> ().enabled = true;
				} 
				else 
				{
					icons [i].GetComponent<SpriteRenderer> ().enabled = false;
				}				
				ghostIcon.GetComponent<SpriteRenderer> ().enabled = true;
			}
			iconsShown = true;
		}
		
		private IEnumerator hideGramoPiecesAfter(float seconds)
		{
			yield return new WaitForSeconds(seconds);
			for (int i = 0; i < getMaxGramoPieces(); i++)
			{
				icons[i].GetComponent<SpriteRenderer> ().enabled = false;			
			}
			ghostIcon.GetComponent<SpriteRenderer> ().enabled = false;
			iconsShown = false;
		}
		
		private void updateCoroutine (IEnumerator oldCoroutine, float newDelay)
		{		
			if (iconsShown) 
			{
				StopCoroutine (oldCoroutine);
				hideCoroutine = hideGramoPiecesAfter (newDelay);
				StartCoroutine (hideCoroutine);
			}
		}

		private void slipCollectedGramoPiece(float gramoIndex)
		{
			GameObject effectiveCollectedGramo = GameObject.FindGameObjectsWithTag("GramoPiece")[(int)gramoIndex];
			GameObject effectiveCollectedGramoIcon = GameObject.FindGameObjectWithTag("GramoIcon" + (gramoIndex+1));
			Vector2 startPosition = effectiveCollectedGramo.transform.position;
			Vector2 finalPosition = effectiveCollectedGramoIcon.transform.position;

			GameObject slippingGramo = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Level/Interface/GramoIcon" + (gramoIndex+1)), startPosition, new Quaternion());
			slippingGramo.transform.localScale = effectiveCollectedGramo.GetComponent<Renderer> ().bounds.size;
			slippingGramo.GetComponent<SpriteRenderer> ().enabled = true;
			slippingGramo.layer = LayerMask.NameToLayer ("Interface");

			float slippingScale;

			StartCoroutine(slippingCoroutine(effectiveCollectedGramoIcon, effectiveCollectedGramo,slippingGramo, startPosition, finalPosition, getSlipDelay()));
		}
		
		
		private IEnumerator slippingCoroutine(GameObject effectiveCollectedGramoIcon, GameObject effectiveCollectedGramo, GameObject slippingGramo, Vector3 startPosition, Vector3 finalPosition, float slippingTime)
		{
			float slippingStartTime = Time.time;
			float localXCorrection = 0;

			while (Time.time < slippingStartTime + slippingTime) 
			{
				slippingGramo.transform.localScale = Vector3.Lerp (effectiveCollectedGramo.transform.localScale, effectiveCollectedGramoIcon.transform.localScale,(Time.time - slippingStartTime)/slippingTime);
				slippingGramo.transform.position = Vector3.Lerp (startPosition, finalPosition,(Time.time - slippingStartTime)/slippingTime);
				localXCorrection += XCorrection;
				Vector3 adjustedPosition = slippingGramo.transform.position;
				adjustedPosition.x += localXCorrection;
				slippingGramo.transform.position = adjustedPosition;
				yield return null;
			}
		
			slippingGramo.transform.localScale = effectiveCollectedGramoIcon.transform.localScale;
			slippingGramo.transform.position = effectiveCollectedGramoIcon.transform.position;
			effectiveCollectedGramoIcon.GetComponent<SpriteRenderer> ().enabled = true;
			Destroy(slippingGramo,0.01f);
		}
		
		private void cameraFollow()
		{		
			XCorrection = camera.transform.position.x - previousCameraPosition.x;
			for (int i = 0; i < icons.Length; i++) 
			{
				icons[i].transform.position = new Vector3 (icons[i].transform.position.x + XCorrection, icons[i].transform.position.y, icons[i].transform.position.z);			
			}		
			ghostIcon.transform.position = new Vector3 (ghostIcon.transform.position.x + XCorrection, ghostIcon.transform.position.y, ghostIcon.transform.position.z);
			previousCameraPosition = camera.transform.position;
		}

	#endregion	

			
	#region accessors

		public float getMaxGramoPieces()
		{	
			return maxGramoPieces;
		}

		public void setMaxGramoPieces(float newMaxGramoPieces)
		{
			if (newMaxGramoPieces >= 0) {
				maxGramoPieces = newMaxGramoPieces;
			}
		}
		
		public bool[] getCollectedGramo()
		{
			return collectedGramo;
		}
		
		public void setCollectedGramo(float gramoIndex, bool isCollected)
		{
			if (gramoIndex >= 0 && gramoIndex < getMaxGramoPieces()) 
			{
				collectedGramo[(int)gramoIndex] = isCollected;
				}
		}
		
		public void setAllCollectedGramo(bool[] collection)
		{
			collectedGramo = collection;
		}
		
		public float getTotalCollectedGramo()
		{
			return totalCollectedGramo;
		}
		
		public void setTotalCollectedGramo(float newCollectedGramo)
		{
			if (newCollectedGramo >= 0 && newCollectedGramo <= getMaxGramoPieces()) 
			{
				totalCollectedGramo = newCollectedGramo;
			}
		}
		
		public float getDisplayDelay()
		{
			return displayDelay;
		}
		
		public void setDisplayDelay(float newDisplayDelay)
		{
			if (newDisplayDelay >= 0) 
			{
				displayDelay = newDisplayDelay;
			}
		}
		
		public float getSlipDelay()
		{
			return slipDelay;
		}
		
		public void setSlipDelay(float newSlipDelay)
		{
			if (newSlipDelay >= 0) 
			{
				slipDelay = newSlipDelay;
			}
		}
			
	#endregion
	
	
	#region private properties
	
		private float maxGramoPieces;				// Total of Gramophone pieces in the level
		private bool[] collectedGramo;				// Player's actual collected gramophone pieces. Each array location represent one gramo piece;
		private float totalCollectedGramo;			// Total of collected gramophone pieces;
		
		private GameObject[] icons;					// Array of icons representing collected Gramo pieces. Order of icons is important.	
		private GameObject ghostIcon;				// Shadowed template for gramophone pieces
		
		private float displayDelay;					// Time in seconds while the icons will be displayed
		private IEnumerator hideCoroutine;			// Coroutine to hide the icons
		private bool iconsShown;					// Display of icons
		private float slipDelay;					// Time in seconds while the icons will be slipped when collected

		private Transform camera;  					// Interface camera
		private float XCorrection;					// Axis correction to manage the camera movement
		private Vector3 previousCameraPosition;


	
	#endregion

}
