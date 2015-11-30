using UnityEngine;
using System.Collections;

public class DustManager : MonoBehaviour 
{
	// DustManager manage the count of dust the player pick up


	#region public properties

		public GameObject m_player;				// Player (with Tag "Player" in Unity)
	private int playerDust;					// Player's dust amount. Betxeen 0 and maxDust
	public float maxDust = 100.0f;				// The maximum of dust the player can pick up;
	public int effectivePlayerDust;			// Effective player's dust amount. Only for test to simulate the win of dust. Replace every mention by "player.getDust()"
	
	private BWEffect shader;				// B&W effect

	#endregion


	#region main methods

		void Start () 
		{		
			// Player initialization
			m_player = GameObject.FindGameObjectWithTag ("Player");
			setPlayerDust(effectivePlayerDust);
			// Shader initialization
			shader = Camera.main.GetComponent<BWEffect>();
			updateShader ();
		}
		
		void Update () 
		{
			// Listener for player's dust
			int newPlayerDust = effectivePlayerDust;
			
			if (newPlayerDust != getPlayerDust())
			{
				setPlayerDust(newPlayerDust);
				updateShader();
			} 	
		}

		public void updateShader()
		{
			float shaderIntensity = 1-((1 / maxDust) * getPlayerDust ());
			shader.setIntensity(shaderIntensity);
		}

	#endregion


	#region accessors

		public int getPlayerDust()
		{
			return playerDust;
		}
		
		public void setPlayerDust(int newPlayerDust)
		{
			if (newPlayerDust >= 0 && newPlayerDust <= maxDust) 
			{
				playerDust = newPlayerDust;
			}
		}

	#endregion
}
