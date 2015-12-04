using UnityEngine;
using System.Collections;

public class ScriptCanvasSceneMenu : MonoBehaviour {

	public void OpenLevel(string levelName )
	{
		Application.LoadLevel(levelName);
	}

	public void ExitGame()
	{	
		Application.Quit() ;
	}

}
