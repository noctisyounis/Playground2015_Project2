using UnityEngine;
using System.Collections;

public class ScriptSelectNiveau : MonoBehaviour {
	const int ButtonWidth = 150;
	const int ButtonHeight = 40;
	const int Spaces = 10;


    void OnMouseDown()
    {
		Application.LoadLevel ("SceneLevel1");

    }  


 }
