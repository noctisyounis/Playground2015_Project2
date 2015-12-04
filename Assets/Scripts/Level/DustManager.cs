// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class DustManager : MonoBehaviour 
{
	// DustManager manage the collected dust. Each dust is unique and has a proper index. Dust are not showed on interface but influence the grayscale background effect.


	#region main methods

		void Start ()
		{
			// Dust initialization
			initializeDust ();
		}	
	
		private void initializeDust()
		{
			// Get each dust on the level
			GameObject[] dust = GameObject.FindGameObjectsWithTag ("Dust");
			setDustTotal(dust.Length);

			float totalSavedCollectedDust = 0f;
			for (int i = 0; i < dustTotal; i++)
			{
				// Get the saved collected properties
				bool dustIsCollected = getCollectedDust()[i];

				// Set properties on each component
				dust[i].GetComponent<DustBehaviour>().setDustIndex(i);
				dust[i].GetComponent<DustBehaviour>().setCollected(dustIsCollected);

				// Update the total collected dust
				if(dustIsCollected)
				{
					totalSavedCollectedDust++;
				}
			}
			setTotalCollectedDust(totalSavedCollectedDust);			
		}
		
		public void collectDust(float dustIndex)
		{		
			setCollectedDust(dustIndex,true);
			setTotalCollectedDust (getTotalCollectedDust () + 1);
		}

	#endregion

	#region accessors
		
		public float getDustTotal()
		{	
			return dustTotal;
		}
		
		public void setDustTotal(float newTotal)
		{
			if (newTotal >= 0) {
				dustTotal = newTotal;
			}
		}
		
		public bool[] getCollectedDust()
		{
			return collectedDust;
		}
		
		public void setCollectedDust(float dustIndex, bool isCollected)
		{
			if (dustIndex >= 0 && dustIndex < dustTotal) 
			{
				collectedDust[(int)dustIndex] = isCollected;
			}
		}
		
		public void setAllCollectedDust(bool[] collection)
		{
			collectedDust = collection;
		}
		
		public float getTotalCollectedDust()
		{
			return totalCollectedDust;
		}
		
		public void setTotalCollectedDust(float newCollectedDust)
		{
			if (newCollectedDust >= 0 && newCollectedDust <= dustTotal) 
			{
				totalCollectedDust = newCollectedDust;
			}
		}

	#endregion


	#region private properties

		private float dustTotal;					// Total of dust in the level
		private bool[] collectedDust;				// Player's actual collected dust. Each array location represent one dust;
		private float totalCollectedDust;			// Total of collected dust;

	#endregion
}
