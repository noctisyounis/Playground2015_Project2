﻿// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class DustBehaviour : MonoBehaviour 
{

	// DustBehaviour manage dust behaviour when collected. It have to pe applied to each dust on the level.


	#region main methods

		void Start () 
		{
		}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			if ((other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("DestroyWave")) && !isCollected()) 
			{
				GetComponent<SpriteRenderer>().enabled = false;
				GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
				gameManager.GetComponent<DustManager>().collectDust(getDustIndex());
				setCollected(true);
			}
		}

	#endregion


	#region accessors

		public float getDustIndex()
		{	
			return dustIndex;
		}

		public void setDustIndex(float newIndex)
		{
			if (newIndex >= 0) {
				dustIndex = newIndex;
			}
		}
		
		public bool isCollected()
		{	
			return collected;
		}
		
		public void setCollected(bool isCollected)
		{
			collected = isCollected;
		}

	#endregion


	#region private properties

		private float dustIndex;
		private bool collected;
		
		private Sprite spriteCollected;
		private Sprite spriteUncollected;

	#endregion

}
