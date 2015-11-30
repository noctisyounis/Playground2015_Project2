using UnityEngine;
using System.Collections;

public class AmmoManager : MonoBehaviour 
{

	// AmmoManager manage the ammunition interface. It needs prefabs icons.


	#region public properties
		
		public GameObject m_player;					// Player (with Tag "Player" in Unity)
		public GameObject[] m_icons;				// Array of icons representing player's amunition. Order of icons is important.

		public string m_effectivePlayerAmmo;		// Effective player's amunition. Only for test to simulate the switch of ammo. Replace every mention by "player.getSelectedAmmo()"
		public Material m_colorFilled;				// Only for test to simulate the filled icon.
		public Material m_colorEmpty;				// Only for test to simulate the empty icon.

	#endregion


	# region main methods

		void Start ()
		{
			// Player initialization
			m_player = GameObject.FindGameObjectWithTag ("Player");
			setPlayerAmmo("pushWave");
			// Camera initialization
			camera = Camera.main.transform;
			previousCameraPosition = camera.transform.position;
		}

		void Update () 
		{
			// Listener for player's ammo
			string newPlayerAmmo = m_effectivePlayerAmmo;

			if (newPlayerAmmo != getPlayerAmmo() && (newPlayerAmmo == "pushWave" || newPlayerAmmo == "destroyWave"))
			{
				switchAmmo(newPlayerAmmo);
			} 
			
			// Following of camera
			cameraFollow();
		}

		public void switchAmmo (string newAmmo)
		{	
			if (newAmmo == "pushWave") 
			{	
				m_icons [0].GetComponent<Renderer> ().material = m_colorFilled;
				m_icons [1].GetComponent<Renderer> ().material = m_colorEmpty;
				setPlayerAmmo("pushWave");
			} 
			else 
			{
				m_icons [1].GetComponent<Renderer> ().material = m_colorFilled;
				m_icons [0].GetComponent<Renderer> ().material = m_colorEmpty;
				setPlayerAmmo("destroyWave");
			}
		}

		public void cameraFollow()
		{		
			for (int i = 0; i < m_icons.Length; i++) 
			{
				float posX = (camera.transform.position.x - previousCameraPosition.x) + m_icons[i].transform.position.x;
				m_icons[i].transform.position = new Vector3 (posX, m_icons[i].transform.position.y, m_icons[i].transform.position.z);
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
				case "pushWave" :
					playerAmmo = "pushWave";
					break;
				case "destroyWave" :
					playerAmmo = "destroyWave";
					break;
				default : 
					break;
			}
		}

	#endregion
	
	
	#region private properties
	
		private string playerAmmo;				// Player's ammunition. Can be "pushWave" or "destroyWave"
		private Transform camera;  
		private Vector3 previousCameraPosition;
	
	#endregion
}
