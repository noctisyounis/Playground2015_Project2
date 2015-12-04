// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class AmmoManager : MonoBehaviour 
{

	// AmmoManager manage the ammunition interface. It needs prefabs icons.


	#region public properties		

		public string m_effectivePlayerAmmo;		// Effective player's amunition. Only for test to simulate the switch of ammo. Replace every mention by "player.getSelectedAmmo()"
		
	#endregion


	# region main methods

		void Start ()
		{
			// Player initialization
			m_player = GameObject.FindGameObjectWithTag ("Player");
			setPlayerAmmo(m_effectivePlayerAmmo);
			// Camera initialization
			camera = GameObject.FindGameObjectWithTag("Interface").GetComponent<Camera>().transform;
			previousCameraPosition = camera.transform.position;
			// Icons initialization
			float totalAmmoIcons = 2.0f;
			icons = new GameObject[(int)totalAmmoIcons];
			for (int i = 0; i < totalAmmoIcons; i++)
			{
				icons[i] = GameObject.FindGameObjectWithTag("AmmoIcon" + (i+1));
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
			// Listener for player's ammo
			string newPlayerAmmo = m_effectivePlayerAmmo;

			if (newPlayerAmmo != getPlayerAmmo() && (newPlayerAmmo == "PushWave" || newPlayerAmmo == "DestroyWave"))
			{
				switchAmmo(newPlayerAmmo);
			} 
			
			// Following of camera
			cameraFollow();
		}

		public void switchAmmo (string newAmmo)
		{    
			if (newAmmo == "PushWave") 
			{    
				icons [0].GetComponent<SpriteRenderer> ().enabled = true;
				icons [1].GetComponent<SpriteRenderer> ().enabled = false;
				setPlayerAmmo("PushWave");
			} 
			else 
			{
				icons [1].GetComponent<SpriteRenderer> ().enabled = true;
				icons [0].GetComponent<SpriteRenderer> ().enabled = false;
				setPlayerAmmo("DestroyWave");
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

	# endregion


	#region accessors

		public string getPlayerAmmo()
		{
			return playerAmmo;
		}
		
		public void setPlayerAmmo(string newAmmo)
		{
			switch (newAmmo) 
			{
				case "PushWave" :
					playerAmmo = "PushWave";
					break;
				case "DestroyWave" :
					playerAmmo = "DestroyWave";
					break;
				default : 
					break;
			}
		}

	#endregion
	
	
	#region private properties
	
		private GameObject m_player;					// Player (with Tag "Player" in Unity)
		private string playerAmmo;						// Player's ammunition. Can be "pushWave" or "destroyWave"
		private GameObject[] icons;						// icons representing player's amunition.

		private Transform camera;  
		private Vector3 previousCameraPosition;
	
	#endregion
}
