using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	/*
	 * 0 = empty
	 * 1 = floor_filled
	 * 2 = floor_topleft
	 * 3 = floor_topright
	 * 4 = floor_bottomleft
	 * 5 = floor_bottomright
	*/
	
	int[] m_level = new int[]
	{
		1,1,1,1,1,1,1,1,1,1,
		3,2,3,1,1,1,1,1,2,3,
		0,0,0,3,1,1,1,2,0,0,
		0,0,0,0,0,3,2,0,0,0,
		0,0,0,0,0,0,0,0,0,0,
		0,0,0,0,0,0,0,0,0,0
	};	
	int arraySize = 60;
	int xMax = 10;
	
	void Start () 
	{
		Generate ();
	}
	
	void Update () 
	{
		
	}
	
	void Generate()
	{
		// Position of the Top Left tile
		int x = 0;
		int y = 0;
		// Tiles scale
		float tilesScale = 3.2f;

		for (int i = 0; i < arraySize; ++i) 
		{
			x = i % xMax;
			y = -(i-(i % xMax)) / xMax;
			
			switch(m_level[i])
			{
			case 0:
				break;
				
			case 1:
				GameObject floor_filled = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileFilled_Test"));
				floor_filled.transform.position = new Vector3(x, y, 0);
				floor_filled.transform.localScale = new Vector3(tilesScale, tilesScale, 0);
				break;
				
			case 2:
				GameObject floor_topleft = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileHalf_Test"));
				floor_topleft.transform.position = new Vector3(x, y, 0);
				floor_topleft.transform.localScale = new Vector3(tilesScale, -tilesScale, 0);
				break;
				
			case 3:
				GameObject floor_topright = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileHalf_Test"));
				floor_topright.transform.position = new Vector3(x, y, 0);
				floor_topright.transform.localScale = new Vector3(-tilesScale, -tilesScale, 0);
				break;
				
			case 4:
				GameObject floor_bottomleft = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileHalf_Test"));
				floor_bottomleft.transform.position = new Vector3(x, y, 0);
				floor_bottomleft.transform.localScale = new Vector3(tilesScale, tilesScale, 0);
				break;
				
			case 5:
				GameObject floor_bottomright = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Level/Tiles/PrefabEdgeTileHalf_Test"));
				floor_bottomright.transform.position = new Vector3(x, y, 0);
				floor_bottomright.transform.localScale = new Vector3(-tilesScale, tilesScale, 0);
				break;
			}
		}
	}
}

