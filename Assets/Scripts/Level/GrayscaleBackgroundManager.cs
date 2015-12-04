// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class GrayscaleBackgroundManager : MonoBehaviour 
{

	// GrayScaleBackgroundManager manage the grayscale effect applied on the background. It fade to colored from grayscale when dust and gramophone pieces are collected.


	#region main methods

		void Start()
		{
			initialized = false;
			StartCoroutine(initializeShaderAfter(getDisplayDelay()));
		}
		
		void Update () 
		{
			updateShader();
		}
		
		private IEnumerator initializeShaderAfter (float seconds) 
		{
			// delay
			yield return new WaitForSeconds(seconds);

			// percentage initialization
			gameManager = GameObject.FindGameObjectWithTag ("GameManager");
            gramoPercentage = (100 * getGramoWeight() / getGoalPercentage()) / gameManager.GetComponent<GramoManager> ().getMaxGramoPieces ();
			dustPercentage = (100 * (100 - getGramoWeight()) / getGoalPercentage()) / gameManager.GetComponent<DustManager> ().getDustTotal ();	
			
			playerGramoPercentage = 0;
			playerDustPercentage = 0;
			
			// grayScale initialization
			camera = GameObject.FindGameObjectWithTag("Background").GetComponent<Camera>();        
            initialized = true;		

			// Initial percentages calculation
			tempGramoPercentage = (gameManager.GetComponent<GramoManager> ().getTotalCollectedGramo () * gramoPercentage) * (getGoalPercentage() / 100);
            tempDustPercentage = gameManager.GetComponent<DustManager> ().getTotalCollectedDust () * dustPercentage;
            // set initial effect
            setShaderIntensity(1.0f - ((tempGramoPercentage + tempDustPercentage) / 100));
			camera.GetComponent<BWEffect> ().setIntensity (getShaderIntensity());
		}
		
		public void updateShader()
        {
        if (initialized && getShaderIntensity () > 0) 
			{
				// Percentages listener
				tempGramoPercentage = (gameManager.GetComponent<GramoManager> ().getTotalCollectedGramo () * gramoPercentage) * (getGoalPercentage() / 100);
				tempDustPercentage = gameManager.GetComponent<DustManager> ().getTotalCollectedDust () * dustPercentage;

				// set grayscale effect intensity
				setShaderIntensity(1 - ((tempGramoPercentage + tempDustPercentage) / 100));
				camera.GetComponent<BWEffect> ().setIntensity (getShaderIntensity());
			}
		}

	#endregion


	#region accessors

		public float getShaderIntensity()
		{
			return shaderIntensity;
		}
		
		public void setShaderIntensity(float intensity)
		{
			if (intensity < 0) 
			{
				shaderIntensity = 0;
			} 
			else if (intensity > 1) 
			{
				shaderIntensity = 1;
			} 
			else 
			{
				shaderIntensity = intensity;
			}
		}
	
		public float getGoalPercentage()
		{
			return goalPercentage;
		}
		
		public void setGoalPercentage(float newPercentage)
		{
			if (newPercentage >= 0 && newPercentage <= 100) 
			{
				goalPercentage = newPercentage;
			}
		}
		
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
		
		public float getDisplayDelay()
		{
			return displayDelay;
		}
		
		public void setDisplayDelay(float newdDisplayDelay)
		{
			if (newdDisplayDelay >= 0) 
			{
				displayDelay = newdDisplayDelay;
			}
		}

	#endregion


	#region private properties
	
		private float goalPercentage;				// Percentage of objectives to pick up to obtain a full colored background
		private float gramoWeight;					// Importance of gramophone pieces compared to dust, in percentage
		private float displayDelay;					// Latency in seconds before the shader applied at level loading

		private float gramoPercentage;				// proportion value for one gramophone piece
		private float playerGramoPercentage;		// proportion of collected gramophone pieces
		private float tempGramoPercentage;			
		private float dustPercentage;				// proportion value for one dust
		private float playerDustPercentage;			// proportion of collected dust
		private float tempDustPercentage;			

		private GameObject gameManager;				// GameManager with GramoManager and DustManager scripts
		private Camera camera;						// Background camera on which is applied the BWEffect script
		private float shaderIntensity;	
		private bool initialized;					// Boolean for begining latency	

	#endregion


	
	// Update is called once per frame
}