using UnityEngine;
using System.Collections;

public class CameraFollow_Test : MonoBehaviour
{

	public GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");	
	}

	void FixedUpdate () 
	{
		float posX = player.transform.position.x;
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
