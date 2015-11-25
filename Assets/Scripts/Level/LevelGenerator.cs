using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	/*
	 * 0 = Pattern_0
	 * 1 = Pattern_1
	 * 2 = Pattern_2
	 * 3 = Pattern_3
	 * 4 = Pattern_4
	 * 5 = Pattern_5
	*/
	
	int[] m_level = new int[]
	{
		1,1,1,1,1,1,1,1,1,1,
		3,2,3,1,1,1,1,1,2,3,
		0,5,0,3,1,1,1,2,0,0,
		4,5,2,0,0,3,2,0,0,0
	};	
	int yMax = 4;
	int xMax = 10;
	int arraySize;
	float patternSize = 10;

	
	void Start () 
	{
		arraySize = xMax * yMax;
		Generate ();
	}
	
	void Update () 
	{
		
	}
	
	void Generate()
	{
		float x = 0;
		float y = 0;
		GameObject pattern;

		for (int i = 0; i < arraySize; ++i) 
		{
			x = (i % xMax) * patternSize;
			y = ((i-(i % xMax)) / xMax) * patternSize;

			// Remplacer les Prefabs et ajouter des "case" pour les différents patterns
			
			switch(m_level[i])
			{
			case 0:
				break;
				
			case 1:
				pattern = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				pattern.transform.position = new Vector3(x, y, 0);
				break;
				
			case 2:
				pattern = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				pattern.transform.position = new Vector3(x, y, 0);
				break;
				
			case 3:
				pattern = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				pattern.transform.position = new Vector3(x, y, 0);
				break;
				
			case 4:
				pattern = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				pattern.transform.position = new Vector3(x, y, 0);
				break;
				
			case 5:
				pattern = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				pattern.transform.position = new Vector3(x, y, 0);
				break;
			}
		}
	}
}

