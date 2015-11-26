using UnityEngine;
using System.Collections;

public class ScriptClickOpenLevel : MonoBehaviour {

	public void OpenLevel(string levelName )
	{
		Application.LoadLevel(levelName);
	}
}
