// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class LevelEndBehaviour : MonoBehaviour 
{
	// EndLevelBehaviour launch the LevelUnloader action. It have to pe applied to the end radio element.


	#region main methods

		void Start()
		{
			reached = false;
		}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag ("Player") && !reached) 
			{
				GameObject levelUnloader = GameObject.FindGameObjectWithTag("LevelUnloader");
				levelUnloader.GetComponent<LevelUnloader>().endLevel();
				reached = true;
			}
		}
	
	#endregion
	
	
	#region private properties

		private bool reached;
	
	#endregion
}
