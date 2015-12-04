// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{

	// CameraFollowing manage cameras. It has to be applied directly to cameras. Cameras and Extreme Background follows character.


	#region main methods

		void Start () 
		{
			player = GameObject.FindGameObjectWithTag ("Player");	
		}

		void FixedUpdate () 
		{
			float posX = player.transform.position.x;
			transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
			transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
			GameObject extremeBackground = GameObject.FindGameObjectWithTag ("BackgroundPlan");
			extremeBackground.transform.position = new Vector3(posX,extremeBackground.transform.position.y,200.0f);
		}

	#endregion


	#region private properties	
	
		private GameObject player;
		
	#endregion
}
