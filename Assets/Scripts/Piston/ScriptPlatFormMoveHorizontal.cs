using UnityEngine;
using System.Collections;

public class ScriptPlatFormMoveHorizontal : MonoBehaviour {

	public string m_plateFormName;
	public float m_interSecondePlatForm;
	public float m_posX;
	public float m_posY;
	
	private GameObject  tempPlatForm ;
	private float positionX;
	private float positionY;
		
	private int timeCompter;
	
	private enum statePistonArray {PlatformRight,PlatformLeft,PlatformPause };
	private statePistonArray platformState;
	private const float speedPlatForm=0.1f;
	private float DistancePlateForm = 2.0f;
	private float posX2;
	
	
	void Start () 
	{	positionX = m_posX;
		positionY = m_posY;
		posX2 = m_posX + DistancePlateForm;
		
		m_interSecondePlatForm = m_interSecondePlatForm*60; 
		
		platformState = statePistonArray.PlatformRight;
		tempPlatForm = GameObject.Find(m_plateFormName);
		//	tempPlatForm=(Transform)Instantiate(m_piston, new Vector3(positionX, positionY, 0), Quaternion.identity);
		
		timeCompter = 0;
	}
	
	
	void FixedUpdate () 
	{	
		//	timeCompter += 1;
		PlateFormManager ();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{  
		if (platformState == statePistonArray.PlatformRight) {
			platformState = statePistonArray.PlatformLeft;
		} 
		else 
		{
			platformState = statePistonArray.PlatformRight;
		}
		
	}
	
	void PlateFormManager()
	{
		
		
		if ( (platformState==statePistonArray.PlatformRight) )// && (count%15)==0)
		{	
			tempPlatForm.transform.position = new Vector3(positionX,positionY, 0);
			positionX +=speedPlatForm;
			
			
			if (tempPlatForm.transform.position.x> posX2)
			{
				platformState = statePistonArray.PlatformLeft;
			}
		}
		
		
		if ( (platformState==statePistonArray.PlatformLeft) ) //&& (count%15)==0)
		{	
			tempPlatForm.transform.position = new Vector3(positionX,positionY , 0);
			positionX -=speedPlatForm;
			
			if ( tempPlatForm.transform.position.x < m_posX)
			{	platformState = statePistonArray.PlatformRight;
			}
			
		}
		
		
		
		
		
	}
	
	float Distance(float p1,float p2)
	{  if (p1 - p2 < 0) {
			return -(p1 - p2);
		}
		else 
		{ return p1 - p2;
		}
	}

}
