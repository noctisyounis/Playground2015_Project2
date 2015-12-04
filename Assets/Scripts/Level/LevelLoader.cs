// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{

	// LevelLoader manage level loading, instanciate and initialize all components for Level scene

	#region public properties

		// General properties	
		public int m_levelIndex;							// Index of the level

		// LevelGenerator properties
		public int[] m_levelLayout;						// Array of gameplay patterns
		public float m_levelLayoutYCorrection;			// Y Position Correction for gameplay patterns

		// LifeManager properties
		public float m_maxPlayerLife;					// maximum player lifes
		
		// DustManager properties
		public float m_totalDust;						// Total of dust in the level

		// GramoManager properties
		public float m_totalGramo;						// Total of gramophone pieces in the level, including the bonus piece
		public float m_gramoDisplayDelay;				// Time in seconds while the gramophone icons will be displayed 
		public float m_gramoSlipDelay;					// Time in seconds while the icons will be slipped when collected

		// LevelUnloader properties
		public float m_gramoWeightGoalPercentage;		// Ratio of total gramophone pieces compared to total dust for completion goal, in percentage
														// e.g. : if set to 20, totality of gramophone pieces (including bonus one) will represents 20% of total completion
		public float m_gramoGoalPercentage;				// Level completion to reach to unlock the bonus gramophone piece, in percentage

		// GrayscaleBackgroundManager properties	
		public float m_BackgroundGoalPercentage;		// Percentage of objectives to pick up to obtain a full colored background
		public float m_grayscaleDisplayDelay;			// Latency in seconds before the shader applied at level loading

		// Character properties	
		public Vector3 m_playerInitialPosition;			// Spawn position of the player

	#endregion


	#region main methods
	
		void Awake () 
		{			
			loadLevel ();
			setProperties ();
		}

		private void loadLevel()
		{
			// Instanciations
			// LevelUnloader
			levelUnloader = new GameObject ();
			levelUnloader.tag = "LevelUnloader";
			LevelUnloader levelUnloaderScript = levelUnloader.AddComponent <LevelUnloader>();
			// LevelManager
			levelManager = new GameObject ();
			levelManager.tag = "LevelManager";
			LevelGenerator levelGeneratorScript = levelManager.AddComponent <LevelGenerator>();
			GrayscaleBackgroundManager grayscaleBackgroundManagerScript = levelManager.AddComponent <GrayscaleBackgroundManager>();
			// GameManager
			gameManager = new GameObject ();
			gameManager.tag = "GameManager";
			LifeManager lifeManagerScript = gameManager.AddComponent <LifeManager>();
			DustManager dustManagerScript = gameManager.AddComponent <DustManager>();
			GramoManager gramoManagerScript = gameManager.AddComponent <GramoManager>();
			AmmoManager ammoManagerScript = gameManager.AddComponent <AmmoManager>();
			// Player
			player = (GameObject)GameObject.Instantiate (Resources.Load("Prefabs/Character/Player"),m_playerInitialPosition,new Quaternion());
			player.tag = "Player";
			// TO DO : Here add instanciation of playerLife and playerAmmo to delete the "effectives" properties
		}


		private void setProperties()
		   {
            // LevelUnloader properties
            if (getPercentage() >= m_gramoGoalPercentage)
            {
                middleGoalAlreadyReached = true;
            }
            else
            {
                middleGoalAlreadyReached = false;
            }
			levelUnloader.GetComponent<LevelUnloader> ().setGramoWeight (m_gramoWeightGoalPercentage);
			levelUnloader.GetComponent<LevelUnloader> ().setCompletionMiddleGoal (m_gramoGoalPercentage);
			levelUnloader.GetComponent<LevelUnloader> ().setMiddleGoalAlreadyReached (middleGoalAlreadyReached);
			// LevelGenerator properties
			levelManager.GetComponent<LevelGenerator> ().setLevelLayout (m_levelLayout);
			levelManager.GetComponent<LevelGenerator> ().setYCorrection (m_levelLayoutYCorrection);
			// GrayscaleBackgroundManager properties
			levelManager.GetComponent<GrayscaleBackgroundManager> ().setGoalPercentage(m_BackgroundGoalPercentage);
			levelManager.GetComponent<GrayscaleBackgroundManager> ().setDisplayDelay (m_grayscaleDisplayDelay);
			levelManager.GetComponent<GrayscaleBackgroundManager> ().setGramoWeight (m_gramoWeightGoalPercentage);
            // LifeManager properties
            player.GetComponent<CharacterBehaviour>().SetPv((int)m_maxPlayerLife);
			gameManager.GetComponent<LifeManager> ().setMaxPlayerLife(m_maxPlayerLife);
			// DustManager properties
			collectedDust = getSavedCollectedDust(m_totalDust);
			gameManager.GetComponent<DustManager> ().setAllCollectedDust (collectedDust);
			// GramoManager properties
			collectedGramo = getSavedCollectedGramo(m_totalGramo);
			gameManager.GetComponent<GramoManager> ().setMaxGramoPieces(collectedGramo.Length);
			gameManager.GetComponent<GramoManager> ().setAllCollectedGramo (collectedGramo);
			gameManager.GetComponent<GramoManager> ().setDisplayDelay (m_gramoDisplayDelay);
			gameManager.GetComponent<GramoManager> ().setSlipDelay (m_gramoSlipDelay);

		}

		private float getPercentage ()
		{
			float collectedDust = gameManager.GetComponent<DustManager> ().getTotalCollectedDust();
			float collectedGramo = gameManager.GetComponent<GramoManager> ().getTotalCollectedGramo();
			
			float dustCompletionPercentage = ((collectedDust / m_totalDust) * 100) * ((100 - m_gramoWeightGoalPercentage) / 100);
			float gramoCompletionPercentage = ((collectedGramo / m_totalGramo) * 100) * (m_gramoWeightGoalPercentage / 100);
			float completionPercentage = dustCompletionPercentage + gramoCompletionPercentage;

        return completionPercentage;
		}

		private bool[] getSavedCollectedDust(float totalDust)
		{
			return PersistanceManager.GetTabDust (totalDust);
		}
		
		private bool[] getSavedCollectedGramo(float totalGramo)
		{
			return PersistanceManager.GetTabGramophone (totalGramo);
		}
	
	#endregion


	#region accessors

		public int getLevelIndex()
		{
			return m_levelIndex;
		}
		
		public void setLevelIndex(int newIndex)
		{
			if (newIndex >= 0) 
			{
				m_levelIndex = newIndex;
			}
		}

	#endregion


	#region private properties

		// Elements to instantiate
		private GameObject levelUnloader;
		private GameObject gameManager;
		private GameObject levelManager;
		private GameObject player;

		// Properties from previous saved game
		private bool[] collectedDust;					// Player's actual collected dust. Each array location represent one dust;
		private bool[] collectedGramo;					// Player's actual collected gramophone pieces. Each array location, except first, represent one piece
		private bool middleGoalAlreadyReached;			// Verify if the middle goal have been reached before		
	
	#endregion

}
