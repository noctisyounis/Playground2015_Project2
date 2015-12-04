// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class GramoPieceBehaviour : MonoBehaviour 
{
	// GramoPieceBehaviour manage gramophone pieces behaviour when collected.


	#region main methods

		void Start () 
		{
			// Material initialization
			colorCollected = (Material)Resources.Load("Materials/Level/darkPurple_Test");
			colorUncollected = (Material)Resources.Load("Materials/Level/lightPurple_Test");
			if (isCollected ()) 
			{
				GetComponent<Renderer> ().material = colorCollected;
			} 
			else 
			{
				GetComponent<Renderer> ().material = colorUncollected;
			}
		}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag ("Player") && !isCollected()) 
			{
				GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
				gameManager.GetComponent<GramoManager>().collectGramo(getGramoIndex());	
				setCollected(true);		
				GetComponent<Renderer>().material = colorCollected;			
			}
		}

	#endregion


	#region accessors

		public float getGramoIndex()
		{	
			return gramoIndex;
		}

		public void setGramoIndex(float newIndex)
		{
			if (newIndex >= 0) {
				gramoIndex = newIndex;
			}
		}
		
		public bool isCollected()
		{	
			return collected;
		}
		
		public void setCollected(bool isCollected)
		{
			collected = isCollected;
		}

	#endregion


	#region private properties

		private float gramoIndex;
		private bool collected;
	
		private Material colorCollected;				// Only for test to simulate the collected icon.	
		private Material colorUncollected;				// Only for test to simulate the uncollected icon.

	#endregion

}
