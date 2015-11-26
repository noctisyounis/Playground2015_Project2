using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptPiston : MonoBehaviour {

	public Transform piston;
	private Transform pistonTemp;
	private GameObject platForme;
	private int count;
	private const int INSTERVAL_PISTON=60*10; 
	private const int CAMERASIZE=10;
	private enum statePistonArray {PistonDown,PistonUp,PistonPause };
	private statePistonArray pistonState1;
	private statePistonArray pistonState2;
	private statePistonArray pistonState3;
	private statePistonArray pistonState4;

	private List<Transform> piston1List;



	private int posX1,posY1;
	private int posX2,posY2;
	private int posX3,posY3;
	private int posX4,posY4;


	void Start () 
	{	
		posX1 = -8;
		posY1 = 10;

		posX2 = 7;
		posY2 = 10;

		posX3 =12;
		posY3 = 10;

		posX4 = 17;
		posY4 = 10;


		pistonState1 = statePistonArray.PistonPause;
		pistonState2 = statePistonArray.PistonPause;
		pistonState3 = statePistonArray.PistonPause;
		pistonState4 = statePistonArray.PistonPause;

		Instantiate(piston, new Vector3(9, 9,  0), Quaternion.identity);
		Debug.Log (Screen.height/10+"     "+Screen.width/10);
		count = 0;
	}

	
	// Update is called once per frame
	void FixedUpdate () 
	{	count += 1;
		GestionPiston ();


	}

	void GestionPiston()
	{
		if ((count % INSTERVAL_PISTON) == 0) 
		{	
			if (pistonState1== statePistonArray.PistonPause )
			{	count=0;
				pistonState1=statePistonArray.PistonDown;
			}
						
		}

		if ( (pistonState1==statePistonArray.PistonDown) && (count%30)==0)
		{	Debug.Log ("POPOPOPOP");
			//platForme=Instantiate(piston, new Vector3(posX1,posY1, 0), Quaternion.identity).;

			posY1 -=1;
		//	piston1List.Add(pistonTemp);


		}


	}

	void OnCollisionEnter(Collision collision) {

	//	foreach (ContactPoint contact in collision.contacts) {
	//		Debug.DrawRay(contact.point, contact.normal, Color.white);
	//	}
	//	if (collision.relativeVelocity.magnitude > 2)
	//		audio.Play();


		Debug.Log ("jojojjo");
	}

}






















