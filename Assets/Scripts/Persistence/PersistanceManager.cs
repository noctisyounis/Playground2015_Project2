// Authors : Chitou Habib
// Creation : 12/2015

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistanceManager 
{
		
	// persistance of data in xml file 


	#region main methods

		// Gramophone pieces

		public static bool[] GetTabGramophone(float totalGramo)
		{	ScriptGramoItemsContainer temp = new ScriptGramoItemsContainer ();
			temp = ScriptGramoItemsContainer.LoadGramophone(gramoReadFileName);
			bool[] tabGramo= new bool[(int)totalGramo] ;
			if (temp != null) 
			{
			int i = 0;

			foreach(ScriptGramoItems item in temp.Items)
			{
				tabGramo[i]= item.GramoCollected;
				i++;
			}
			return tabGramo;
			} 
			else 
			{
				for (int i = 0; i < totalGramo; i++)
				{
					tabGramo[i] = false;
				}
				return tabGramo;
			}
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


		// Dust

		public static bool[] GetTabDust(float totalDust)
		{	
			ScriptDustItemsContainer temp = new ScriptDustItemsContainer ();
			temp = ScriptDustItemsContainer.LoadDust (DustReadFileName);
			bool[] tabDust = new bool[(int)totalDust];
			if (temp != null) 
			{
				int i = 0;

				foreach (ScriptDustItems item in temp.Items) {
					tabDust [i] = item.Dustcollected;
					i++;
				}
				return tabDust;
			} 
			else 
			{
				for (int i = 0; i < totalDust; i++)
				{
					tabDust[i] = false;
				}
				return tabDust;
			}
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

	#endregion

	public static bool[] GetTabGramophone2(float totalGramo)
	{	ScriptGramoItemsContainer2 temp = new ScriptGramoItemsContainer2 ();
		temp = ScriptGramoItemsContainer2.LoadGramophone2(gramoReadFileName2);
		bool[] tabGramo= new bool[(int)totalGramo] ;
		if (temp != null) 
		{
			int i = 0;
			
			foreach(ScriptGramoItems2 item in temp.Items)
			{
				tabGramo[i]= item.GramoCollected;
				i++;
			}
			return tabGramo;
		} 
		else 
		{
			for (int i = 0; i < totalGramo; i++)
			{
				tabGramo[i] = false;
			}
			return tabGramo;
		}
	}
	
	public void SetTabGramophone2(bool[] tempTab)
	{
		ScriptGramoItemsContainer2 save = new ScriptGramoItemsContainer2 ();
		ScriptGramoItems2 saveItem;//= new ScriptGramoItems ();
		for (int i=0; i< sizeGramo; i++)
		{	
			saveItem = new ScriptGramoItems2 ();
			saveItem.GramoCollected = tempTab[i];
			save.Items.Add(saveItem);
		}
		
		save.WriteGramophone2(gramoWriteFileName2);
	}
	
	
	// Dust
	
	public static bool[] GetTabDust2(float totalDust)
	{	
		ScriptDustItemsContainer2 temp = new ScriptDustItemsContainer2 ();
		temp = ScriptDustItemsContainer2.LoadDust (DustReadFileName2);
		bool[] tabDust = new bool[(int)totalDust];
		if (temp != null) 
		{
			int i = 0;
			
			foreach (ScriptDustItems2 item in temp.Items) {
				tabDust [i] = item.Dustcollected;
				i++;
			}
			return tabDust;
		} 
		else 
		{
			for (int i = 0; i < totalDust; i++)
			{
				tabDust[i] = false;
			}
			return tabDust;
		}
	}
	
	
	public void SetTabDust2(bool[] tempTab, int tailleTab)
	{
		ScriptDustItemsContainer2 save = new ScriptDustItemsContainer2 ();
		ScriptDustItems2 saveItem ;//= new ScriptDustItems ();
		for (int i=0; i< tailleTab; i++)
		{	
			saveItem = new ScriptDustItems2 ();
			saveItem.Dustcollected = tempTab[i];
			save.Items.Add(saveItem);
		}
		
		save.WriteDust2(DustWriteFileName2);
	}

	
	#region private properties
	
		private const int sizeGramo = 5;
		private const string DustWriteFileName="Assets\\Resources\\dusts.xml";
		private const string DustReadFileName="dusts";
		private const string gramoWriteFileName="Assets\\Resources\\gramophone.xml";
		private const string gramoReadFileName="gramophone";
		//	private bool[] tabGramophone = new bool[sizeGramo];
		private float pourcentage;

	private const string DustWriteFileName2="Assets\\Resources\\dusts2.xml";
	private const string DustReadFileName2="dusts2";
	private const string gramoWriteFileName2="Assets\\Resources\\gramophone2.xml";
	private const string gramoReadFileName2="gramophone2";
	
	#endregion

}
