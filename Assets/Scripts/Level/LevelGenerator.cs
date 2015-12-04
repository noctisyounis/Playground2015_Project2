// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{

	// LevelGenerator generate the foreground plateforms. It needs prefabs of patterns named "PrefabGameplayPatternX"(where X is 
	// the pattern's number) and placed in Resources/Prefabs/Level/GameplayPatterns.
	// For each pattern, the extreme left bottom point of the the extreme left bottom element have to be positioned exactly at (0,-5) with our camera resolution.


	#region main methods

		void Start () 
		{
			Generate ();
		}
		
		void Generate()
		{
			GameObject pattern;
			Renderer[] patternComponents;
			string prefabPath;
			float patternArraySize = levelLayout.Length;
			float initialPatternX = -14.0f;
			float patternX = initialPatternX;
			float maxX;
			float minX;
			float maxXComponent;
			float minXComponent;
			float sizeComponent;
			setLevelSize (0f);		

			GameObject foreground = new GameObject ("Foreground");
			foreground.tag = "Foreground";
			
			for (int i = 0; i < patternArraySize; i++) 
			{
				// Select the prefab
				prefabPath = "Prefabs/Level/GameplayPatterns/PrefabGameplayPattern" + levelLayout[i];
				
				// Instantiate the prefab
				pattern = (GameObject)GameObject.Instantiate (Resources.Load (prefabPath));
				pattern.transform.position = new Vector3(patternX, yCorrection, 0);
				pattern.transform.parent = foreground.transform;

				
				// Update next X position
				patternComponents = pattern.GetComponentsInChildren<Renderer>();
				float patternCompArraySize = patternComponents.Length;
				maxX = patternComponents[1].bounds.max.x;
				minX = patternComponents[1].bounds.min.x;
				
				for (int j = 1; j < patternCompArraySize; j++)
				{	
					// Update min and max x-position checking each component
					maxXComponent = patternComponents[j].bounds.max.x;
					minXComponent = patternComponents[j].bounds.min.x;

					if ( maxXComponent > maxX)
					{
						maxX = maxXComponent;
					}
					
					if ( minXComponent < minX)
					{
						minX = minXComponent;
					}
				}
					
				sizeComponent = maxX - minX;
				patternX += sizeComponent;			
			}

           foreground.layer = LayerMask.NameToLayer("Foreground");

        // Set Level Size when generate
        setLevelSize (patternX - initialPatternX);
		}
	
	#endregion


	#region accessors

		public float getLevelSize()
		{
			return levelSize;
		}
	
		public void setLevelSize(float newSize)
		{
			if (newSize >= 0) 
			{
				levelSize = newSize;
			}
		}

		public int[] getLevelLayout()
		{
			return levelLayout;
		}
		
		public void setLevelLayout(int[] newLevelLayout)
		{
			bool verif = true;
			foreach (int element in newLevelLayout) 
			{
				if (element < 0)
				{
					verif = false;
				}
			}
			if (verif) 
			{
				levelLayout = newLevelLayout;
			}
		}
		
		public float getYCorrection()
		{
			return yCorrection;
		}
		
		public void setYCorrection(float newYCorrection)
		{
			yCorrection = newYCorrection;
		}

	#endregion


	#region private properties

		private float levelSize;					// Size of the level in Unity unity
		private int[] levelLayout;					// array of pattern index
		private float yCorrection;					// Y position correction for gameplay patterns

	#endregion

}