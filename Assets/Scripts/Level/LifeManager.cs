// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour 
{

	// LifeManager manage the life interface. It needs prefabs icons



	#region main methods

		void Start ()
		{
			// Player initialization
			player = GameObject.FindGameObjectWithTag ("Player");
			// Camera initialization
			camera = GameObject.FindGameObjectWithTag("Interface").GetComponent<Camera>().transform;
			previousCameraPosition = camera.transform.position;
			// Icons initialization
			float totalIcons = (int)getMaxPlayerLife () * 2;
			icons = new GameObject[(int)totalIcons];
			for (int i = 0; i < totalIcons; i++) {
				icons[i] = GameObject.FindGameObjectWithTag("LifeIcon" + (i+1));
				if (i % 2 == 0)
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = true;
				}
				else
				{
					icons[i].GetComponent<SpriteRenderer>().enabled = false;
				}
			}			
		}

		void Update () 
		{
        // Listener for player's life points
        float newPlayerLife = player.GetComponent<CharacterBehaviour>().GetPv();

			if (newPlayerLife != getPlayerLife())
			{
				if (newPlayerLife > getPlayerLife()) 
				{
					while (newPlayerLife != getPlayerLife() && getPlayerLife() <= maxPlayerLife - 1)
					{
						winLife();
					}
				}
				else 
				{
					while (newPlayerLife != getPlayerLife() && getPlayerLife() >= 1)
					{
						looseLife();
					}
				}
			} 
			
			// Following of camera
			cameraFollow();
		}

		public void winLife()
		{
			int iconToFill = (int)getPlayerLife () * 2;
			icons [iconToFill].GetComponent<SpriteRenderer> ().enabled = true;
			icons [iconToFill+1].GetComponent<SpriteRenderer> ().enabled = false;
			setPlayerLife(getPlayerLife() + 1.0f);
		}
		
		public void looseLife()
		{
			int iconToEmpty = (int)getPlayerLife () * 2 - 2;
			icons [iconToEmpty+1].GetComponent<SpriteRenderer> ().enabled = true;
			icons [iconToEmpty].GetComponent<SpriteRenderer> ().enabled = false;
			setPlayerLife(getPlayerLife() - 1.0f);
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

	#endregion


	#region accessors

		public float getPlayerLife()
		{
			return playerLife;
		}
		
		public void setPlayerLife(float newPlayerLife)
		{
			if (newPlayerLife >= 0 && newPlayerLife <= maxPlayerLife) 
			{
			playerLife = newPlayerLife;
			}
		}
	
		public float getMaxPlayerLife()
		{
			return maxPlayerLife;
		}
		
		public void setMaxPlayerLife(float newMaxPlayerLife)
		{
			if (newMaxPlayerLife >= 0) 
			{
				maxPlayerLife = newMaxPlayerLife;
			}
		}

	#endregion
	
	
	#region private properties
	
		private GameObject player;					// Player (with Tag "Player" in Unity)
		private float playerLife;					// Player's life. Between 0 and maxPlayerLife		
		private float maxPlayerLife;				// The maximum of lifes the player can reach	
		private GameObject[] icons;					// Array of icons representing player's life. Order of icons is important.

		private Transform camera;  
		private Vector3 previousCameraPosition;
	
	#endregion

}
