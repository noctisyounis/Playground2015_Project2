using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour 
{

	// LifeManager manage the life interface. It needs prefabs icons


	#region public properties
	
		public GameObject m_player;				// Player (with Tag "Player" in Unity)
		public int m_maxPlayerLife = 3;			// The maximum of lifes the player can reach
		public GameObject[] m_icons;				// Array of icons representing player's life. Order of icons is important.
		public float m_displayDelay = 3.0f;		// Time in seconds while the life points will be displayed

		public int m_effectivePlayerLife;			// Effective player's life. Only for test to simulate the win or lost of lifes. Replace every mention by "player.getLife()"
		public Color m_colorFilled;				// Only for test to simulate the filled icon.
		public Color m_colorEmpty;				// Only for test to simulate the empty icon.
		public Color m_colorTransparent;			// Only for test to simulate the transparent icon.

	#endregion


	#region main methods

		void Start ()
		{
			// Player initialization
			m_player = GameObject.FindGameObjectWithTag ("Player");
			setPlayerLife(m_effectivePlayerLife);
			// Camera initialization
			camera = Camera.main.transform;
			previousCameraPosition = camera.transform.position;
			// Icons display
			hideCoroutine = hideLifePointsAfter(m_displayDelay);
			iconsShown = false;
			showLifePoints ();
			StartCoroutine(hideCoroutine);
		}

		void Update () 
		{
			// Listener for player's life points
			int newPlayerLife = m_effectivePlayerLife;

			if (newPlayerLife != getPlayerLife())
			{
				if (newPlayerLife > getPlayerLife()) 
				{
					showLifePoints();
					while (newPlayerLife != getPlayerLife() && getPlayerLife() <= m_maxPlayerLife - 1)
					{
						winLife();
					}
					updateCoroutine(hideCoroutine);
				}
				else 
				{
					showLifePoints();
					while (newPlayerLife != getPlayerLife() && getPlayerLife() >= 1)
					{
						looseLife();
					}
					updateCoroutine(hideCoroutine);
				}
			} 
			
			// Following of camera
			cameraFollow();
		}

		public void winLife()
		{
			m_icons[getPlayerLife()].GetComponent<Renderer>().material.color = m_colorFilled;
			setPlayerLife(getPlayerLife() + 1);
		}
		
		public void looseLife()
		{
			m_icons[getPlayerLife() - 1].GetComponent<Renderer>().material.color = m_colorEmpty;
			setPlayerLife(getPlayerLife() - 1);
		}

		public void showLifePoints()
		{
			if (!iconsShown) 
			{
				for (int i = 0; i < m_icons.Length; i++) 
				{
					if (i < getPlayerLife ()) 
					{
						m_icons [i].GetComponent<Renderer> ().material.color = m_colorFilled;
					} 
					else 
					{
						m_icons [i].GetComponent<Renderer> ().material.color = m_colorEmpty;
					}			
				}
				iconsShown = true;
			}
		}
	
		public IEnumerator hideLifePointsAfter(float seconds)
		{
			yield return new WaitForSeconds(seconds);
			for (int i = 0; i < m_icons.Length; i++)
			{
				m_icons[i].GetComponent<Renderer>().material.color = m_colorTransparent;			
			}
			iconsShown = false;
		}

		public void updateCoroutine (IEnumerator oldCoroutine)
		{	
			if (iconsShown) 
			{	
				StopCoroutine (oldCoroutine);
				hideCoroutine = hideLifePointsAfter (m_displayDelay);
				StartCoroutine (hideCoroutine);
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

	#endregion


	#region accessors

		public int getPlayerLife()
		{
			return playerLife;
		}
		
		public void setPlayerLife(int newPlayerLife)
		{
			if (newPlayerLife >= 0 && newPlayerLife <= m_maxPlayerLife) 
			{
				playerLife = newPlayerLife;
			}
		}

	#endregion
	
	
	#region private properties
	
		private int playerLife;					// Player's life. Between 0 and maxPlayerLife	
		
		private IEnumerator hideCoroutine;		// Coroutine to hide the icons
		private bool iconsShown;				// Display of icons
		private Transform camera;  
		private Vector3 previousCameraPosition;
	
	#endregion

}
