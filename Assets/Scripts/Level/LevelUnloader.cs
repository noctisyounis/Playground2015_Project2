// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class LevelUnloader : MonoBehaviour 
{
	
	// LevelUnloader manage end of game and launch end panel
	
	
	#region main methods
	
		void Start () 
		{
			gameManager = GameObject.FindGameObjectWithTag ("GameManager");
			persistanceManager = new PersistanceManager ();
		}
		
		public void endLevel () 
		{
			// Final collected dust amount
			float totalFinalCollectedDust = 0f;
			bool[] finalCollectedDust = gameManager.GetComponent<DustManager>().getCollectedDust();
			float totalDust = finalCollectedDust.Length;
			for (int i = 0; i < totalDust; i++) 
			{
				if (finalCollectedDust[i])
				{
					totalFinalCollectedDust++;
				}
			}
			
			// final collected gramophone pieces amount
			float totalFinalCollectedGramo = 0f;
			bool[] finalCollectedGramo = gameManager.GetComponent<GramoManager>().getCollectedGramo();
			float totalGramo = finalCollectedGramo.Length;
			for (int i = 0; i < totalGramo; i++) 
			{
				if (finalCollectedGramo[i])
				{
					totalFinalCollectedGramo++;
				}
			}
			
			// completion percentage
			float dustCompletionPercentage = ((totalFinalCollectedDust / totalDust) * 100) * ((100 - gramoWeight) / 100);
			float gramoCompletionPercentage = ((totalFinalCollectedGramo / totalGramo) * 100) * (gramoWeight / 100);
			setCompletionPercentage(dustCompletionPercentage + gramoCompletionPercentage);

			// test for the bonus gramophone piece
			if (getCompletionPercentage() >= getCompletionMiddleGoal() && !finalCollectedGramo [(int)totalGramo - 1]) 
			{
				finalCollectedGramo [(int)totalGramo - 1] = true;
			}
			
			// save data
			saveCompletedLevelData (completionPercentage, finalCollectedDust, finalCollectedGramo);
			
			// launch completed level panel
			launchEndPanel(completionPercentage, middleGoalAlreadyReached, totalFinalCollectedDust, finalCollectedGramo);
			
		}
		
		private void saveCompletedLevelData(float completionPercentage,bool[] finalCollectedDust, bool[] finalCollectedGramo)
		{
			persistanceManager.SetTabDust (finalCollectedDust, finalCollectedDust.Length);
			persistanceManager.SetTabGramophone (finalCollectedGramo);
		}
		
		private void launchEndPanel (float completionPercentage, bool middleGoalAlreadyReached, float totalFinalCollectedDust, bool[] finalCollectedGramo)
		{
			// TO DO : launch the panel with completion datas
			// the middleGoalAlreadyReached properties allow to know if the player just won the bonus gramophone piece (maybe a little animation on the panel to show that?)
			print ("---LAUNCH END PANEL---");
		}
	
	#endregion
	
	
	#region accessors
	
		public float getGramoWeight()
		{
			return gramoWeight;
		}
		
		public void setGramoWeight(float newGramoWeight)
		{
			if (newGramoWeight >= 0 && newGramoWeight <= 100) 
			{
				gramoWeight = newGramoWeight;
			}
		}
		
		public float getCompletionPercentage()
		{
			return completionPercentage;
		}
		
		public void setCompletionPercentage(float newCompletionPercentage)
		{
			if (newCompletionPercentage >= 0 && newCompletionPercentage <= 100) 
			{
				completionPercentage = newCompletionPercentage;
			}
		}
		
		public float getCompletionMiddleGoal()
		{
			return completionMiddleGoal;
		}
		
		public void setCompletionMiddleGoal(float newCompletionMiddleGoal)
		{
			if (newCompletionMiddleGoal >= 0 && newCompletionMiddleGoal <= 100) 
			{
				completionMiddleGoal = newCompletionMiddleGoal;
			}
		}
		
		public bool getMiddleGoalAlreadyReached()
		{
			return middleGoalAlreadyReached;
		}
		
		public void setMiddleGoalAlreadyReached(bool newMiddleGoalAlreadyReached)
		{
			middleGoalAlreadyReached = newMiddleGoalAlreadyReached;
		}
	
	#endregion
	
	
	#region private properties
	
		private GameObject gameManager;    
		private PersistanceManager persistanceManager;	// Manage of dust and gramophone pieces persistance
		private float gramoWeight;						// Importance of gramophone pieces in level completion compared to dust, in percentage
		private float completionPercentage;				// Completion percentage of the collected items to clean the level
		private float completionMiddleGoal;				// Level completion to reach to unlock the bonus gramophone piece, in percentage
		private bool middleGoalAlreadyReached;			// Verify if the middle goal have been reached before
	
	#endregion
	
}
