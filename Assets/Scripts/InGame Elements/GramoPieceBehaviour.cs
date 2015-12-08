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
			// Sprite initialization
			Sprite[] spritesCollected = Resources.LoadAll<Sprite>("Sprites/GameplayElements/GramophonePiecesCollected");
			spriteCollected = spritesCollected[(int)getGramoIndex()];
			Sprite[] spritesUncollected = Resources.LoadAll<Sprite>("Sprites/GameplayElements/GramophonePiecesUncollected");
			spriteUncollected = spritesUncollected[(int)getGramoIndex()];
			if (isCollected ()) 
			{
				GetComponent<SpriteRenderer> ().sprite = spriteCollected;
			} 
			else 
			{
				GetComponent<SpriteRenderer> ().sprite = spriteUncollected;
			}
		}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag ("Player") && !isCollected()) 
			{
				GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
				gameManager.GetComponent<GramoManager>().collectGramo(getGramoIndex());	
				setCollected(true);		
				GetComponent<SpriteRenderer> ().sprite = spriteCollected;		
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
	
		private Sprite spriteCollected;
		private Sprite spriteUncollected;

	#endregion

}
