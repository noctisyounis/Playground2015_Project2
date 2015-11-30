using UnityEngine;
using System.Collections;

public class ScriptPistons : MonoBehaviour {

	public string m_pistonName;
	public float m_interSecondePiston;
	public float m_posX;
	public float m_posY;

	private GameObject tempPiston;
	private float positionX;
	private float positionY;


	private int timeCompter;

	private enum statePistonArray {PistonDown,PistonUp,PistonPause };
	private statePistonArray pistonState1;
	private const float speedPiston=0.1f;
	private float DistancePiston = 2.0f;


	
	
	void Start () 
	{	positionX = m_posX;
		positionY = m_posY;

		m_interSecondePiston = m_interSecondePiston*60; 

		pistonState1 = statePistonArray.PistonPause;
		tempPiston = GameObject.Find(m_pistonName);
	//	tempPiston=(Transform)Instantiate(m_piston, new Vector3(positionX, positionY, 0), Quaternion.identity);
		Debug.Log (tempPiston.name );	
		timeCompter = 0;
	}
	
	

	void FixedUpdate () 
	{	
		timeCompter += 1;
		PistonManager ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{  
		Debug.Log (other.transform.position.x);
		pistonState1 = statePistonArray.PistonUp;
				
	}
	
	void PistonManager()
	{
		if ((timeCompter % m_interSecondePiston== 0) )
		{	
			if (pistonState1== statePistonArray.PistonPause )
			{	timeCompter=0;
				pistonState1=statePistonArray.PistonDown;
			}
			
		}
		
		if ( (pistonState1==statePistonArray.PistonDown) )// && (count%15)==0)
		{	
			tempPiston.transform.position = new Vector3(positionX,positionY, 0);
			positionY -=speedPiston;
			
			
			if ( Distance( m_posY, tempPiston.transform.position.y)  > DistancePiston)
			{
				pistonState1 = statePistonArray.PistonUp;
			}
		}
		
		
		if ( (pistonState1==statePistonArray.PistonUp) ) //&& (count%15)==0)
		{	
			tempPiston.transform.position = new Vector3(positionX,positionY , 0);
			positionY +=speedPiston;
		}
		
		if ( (pistonState1==statePistonArray.PistonUp) && (positionY >  m_posY))
		{	
			pistonState1 = statePistonArray.PistonPause ;
			
			positionY =m_posY;
		}

		
	}

	float Distance(float p1,float p2)
	{
		return p1 - p2;
	}
	


}
