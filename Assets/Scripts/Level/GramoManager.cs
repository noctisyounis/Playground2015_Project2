using UnityEngine;
using System.Collections;

public class GramoManager : MonoBehaviour {
	
	public GameObject player;					// Player (with Tag "Player" in Unity)
	public int maxGramo = 3;					// The maximum of Gramo pieces the player can pick up;
	private int collectedGramo;					// Player's actual amount of Gramo pieces. Between 0 and maxGramo
	public int effectiveCollectedGramo;			// Effective player's amount of Gramo pieces. Only for test to simulate the win of Gramo. Replace every mention by "player.getGramoPieces()"
	
	public GameObject[] icons;				// Array of icons representing collected Gramo pieces. Order of icons is important.
	public float displayDelay = 3.0f;		// Time in seconds while the icons will be displayed
	private IEnumerator hideCoroutine;		// Coroutine to hide the icons
	private bool iconsShown;				// Display of icons
	
	private Transform camera;  
	private Vector3 previousCameraPosition;
	
	public Material colorFilled;
	public Material colorEmpty;
	public Material colorTransparent;
	
	
	
	
	
	void Start ()
	{
		// Player initialization
		player = GameObject.FindGameObjectWithTag ("Player");
		setCollectedGramo (effectiveCollectedGramo);
		// Camera initialization
		camera = Camera.main.transform;
		previousCameraPosition = camera.transform.position;
		// Icons display
		hideCoroutine = hideGramoPiecesAfter(displayDelay);
		iconsShown = false;
		showGramo ();
		StartCoroutine(hideCoroutine);
	}
	
	void Update () 
	{
		// Listener for player's collected Gramo pieces
		int newCollectedGramo = effectiveCollectedGramo;
		
		if (newCollectedGramo > getCollectedGramo())
		{
			if (newCollectedGramo > maxGramo)
			{
				newCollectedGramo = maxGramo;
			}
			showGramo();
			if (getCollectedGramo() <= maxGramo - 1)
			{
				winGramo();
				setCollectedGramo(getCollectedGramo() + 1);
				updateCoroutine(hideCoroutine);
			}
		} 
		
		
		// Following of camera
		cameraFollow ();
	}
	
	
	
	
	
	
	public void winGramo()
	{
		icons[getCollectedGramo()].GetComponent<Renderer>().material = colorFilled;
	}
	
	public void showGramo()
	{
		if (!iconsShown) {
			for (int i = 0; i < icons.Length; i++)
			{	
				if (i < getCollectedGramo ()) 
				{
					icons [i].GetComponent<Renderer> ().material = colorFilled;
				} 
				else 
				{
					icons [i].GetComponent<Renderer> ().material = colorEmpty;
				}		
			}
			iconsShown = true;
		}
	}
	
	public IEnumerator hideGramoPiecesAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		for (int i = 0; i < icons.Length; i++)
		{
			icons[i].GetComponent<Renderer>().material = colorTransparent;			
		}
		iconsShown = false;
	}
	
	public void updateCoroutine (IEnumerator oldCoroutine)
	{		
		if (iconsShown) 
		{
			StopCoroutine (oldCoroutine);
			hideCoroutine = hideGramoPiecesAfter (displayDelay);
			StartCoroutine (hideCoroutine);
		}
	}
	
	public void cameraFollow()
	{		
		for (int i = 0; i < icons.Length; i++) 
		{
			float posX = (camera.transform.position.x - previousCameraPosition.x) + icons[i].transform.position.x;
			icons[i].transform.position = new Vector3 (posX, icons[i].transform.position.y, icons[i].transform.position.z);
		}			
		previousCameraPosition = camera.transform.position;
	}
	
	
	
	
	
	
	public int getCollectedGramo()
	{
		return collectedGramo;
	}
	
	public void setCollectedGramo(int newCollectedGramo)
	{
		if (newCollectedGramo >= 0 && newCollectedGramo <= maxGramo) 
		{
			collectedGramo = newCollectedGramo;
		}
	}
}
