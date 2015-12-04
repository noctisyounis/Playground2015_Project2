using UnityEngine;
using System.Collections;

// this script is to test the persistence Script . Not to use in the project

public class ScriptDustLoader : MonoBehaviour {

	private bool[] tabPoussiere = {true,false,false,false,true};

	void Start ()
	{ 
	//	 SaveXml ();
	//	 LoadXml ();


		SaveXmlGramo ();
		LoadXmlGramo ();
	}


	void SaveXmlGramo ()
	{
		PersistanceManager per = new PersistanceManager ();
		per.SetTabGramophone(tabPoussiere);
		
	}	
	
	void LoadXmlGramo ()
	{
		
		tabPoussiere = PersistanceManager.GetTabGramophone ();
		Debug.Log(tabPoussiere.Length);
		foreach(bool item in tabPoussiere)
		{
			Debug.Log(item);
		}
		
	}	

	//---------------------------------


	void SaveXml ()
	{
		PersistanceManager per = new PersistanceManager ();
		per.SetTabDust(tabPoussiere,5);

	}	

	void LoadXml ()
	{
		
		tabPoussiere=PersistanceManager.GetTabDust ();
		Debug.Log(tabPoussiere.Length);
		foreach(bool item in tabPoussiere)
		{
			Debug.Log(item);
		}

	}	
}
