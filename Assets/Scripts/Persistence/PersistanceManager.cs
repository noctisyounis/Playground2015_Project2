using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// persistance of data in xml file 
public class PersistanceManager {
	private const int sizeGramo = 5;
	private const string DustWriteFileName="Assets\\Resources\\dusts.xml";
	private const string DustReadFileName="dusts";
	private const string gramoWriteFileName="Assets\\Resources\\gramophone.xml";
	private const string gramoReadFileName="gramophone";
//	private bool[] tabGramophone = new bool[sizeGramo];
	private float pourcentage;



	public static bool[] GetTabGramophone()
	{	ScriptGramoItemsContainer temp = new ScriptGramoItemsContainer ();
		temp = ScriptGramoItemsContainer.LoadGramophone(gramoReadFileName);
		bool[] tabGramo= new bool[temp.Items.Count] ;
		int i = 0;

		foreach(ScriptGramoItems item in temp.Items)
		{
			tabGramo[i]= item.GramoCollected;
			i++;
		}
		return tabGramo;
	}

	public void SetTabGramophone(bool[] tempTab)
	{
		ScriptGramoItemsContainer save = new ScriptGramoItemsContainer ();
		ScriptGramoItems saveItem;//= new ScriptGramoItems ();
		for (int i=0; i< sizeGramo; i++)
		{	
			saveItem = new ScriptGramoItems ();
			saveItem.GramoCollected = tempTab[i];
			save.Items.Add(saveItem);
		}
		
		save.WriteGramophone(gramoWriteFileName);
	}





	public static bool[] GetTabDust()
	{	ScriptDustItemsContainer temp = new ScriptDustItemsContainer ();
		temp = ScriptDustItemsContainer.LoadDust (DustReadFileName);
		bool[] tabDust= new bool[temp.Items.Count] ;
		int i = 0;

		foreach(ScriptDustItems item in temp.Items)
		{
			tabDust[i]= item.Dustcollected;
			i++;
		}
		return tabDust;
	}


	public void SetTabDust(bool[] tempTab, int tailleTab)
	{
		ScriptDustItemsContainer save = new ScriptDustItemsContainer ();
		ScriptDustItems saveItem ;//= new ScriptDustItems ();
		for (int i=0; i< tailleTab; i++)
		{	
			saveItem = new ScriptDustItems ();
			saveItem.Dustcollected = tempTab[i];
			save.Items.Add(saveItem);
		}

		save.WriteDust(DustWriteFileName);
	}








}
