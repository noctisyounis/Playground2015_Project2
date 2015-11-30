using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{

	// LevelGenerator generate the foreground plateforms. It needs prefabs of patterns named "PrefabGameplayPatternX"(where X is 
	// the pattern's number) and placed in Resources/Prefabs/Level/GameplayPatterns

	#region public properties

		public int[] m_level = new int[]
		{
			1, 1, 1, 2, 2, 2, 3, 3, 3, 1, 2, 3, 2, 1
		};

	#endregion


	#region main methods

		void Start () 
		{
			Generate ();
		}
		
		void Update () 
		{
			
		}
		
		void Generate()
		{
			GameObject pattern;
			string prefabPath;
			float x = 0;
			int arraySize = m_level.Length;

			for (int i = 0; i < arraySize; i++) 
			{
				prefabPath = "Prefabs/Level/GameplayPatterns/PrefabGameplayPattern" + m_level[i];

				pattern = (GameObject)GameObject.Instantiate (Resources.Load (prefabPath));
				pattern.transform.position = new Vector3(x, 0, 0);
				x += pattern.GetComponent<Renderer>().bounds.size.x;
			}
				
		}

	#endregion

}

