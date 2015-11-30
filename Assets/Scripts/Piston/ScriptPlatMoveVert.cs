using UnityEngine;
using System.Collections;

public class ScriptPlatMoveVert : MonoBehaviour {
	public string m_plateFormName;
	public float m_interSecondePlatForm;
	public float m_posX;
	public float m_posY;
	
	private GameObject tempPlatForm ;
	private float positionX;
	private float positionY;
	
	
	private int timeCompter;
	
	private enum statePistonArray {PlatFormUp,PlatFormDown,PlatFormPause};
	private statePistonArray platFormState;
	private const float speedPlatForm=0.1f;
	private float DistancePlateForm = 2.0f;
	private float posY2;
	
	
	void Start () 
	{	positionX = m_posX;
		positionY = m_posY;
		posY2 = m_posY + DistancePlateForm;
		
		m_interSecondePlatForm = m_interSecondePlatForm*60; 
		
		platFormState = statePistonArray.PlatFormUp;
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
		if (platFormState == statePistonArray.PlatFormUp) {
			platFormState = statePistonArray.PlatFormDown;
		} 
		else 
		{
			platFormState = statePistonArray.PlatFormUp;
		}
		
	}
	
	void PlateFormManager()
	{
		if ( (platFormState==statePistonArray.PlatFormUp) )// && (count%15)==0)
		{	
			tempPlatForm.transform.position = new Vector3(positionX,positionY, 0);
			positionY +=speedPlatForm;
			
			
			if (tempPlatForm.transform.position.y> posY2)
			{
				platFormState = statePistonArray.PlatFormDown;
			}
		}
		
		
		if ( (platFormState==statePistonArray.PlatFormDown) ) //&& (count%15)==0)
		{	
			tempPlatForm.transform.position = new Vector3(positionX,positionY , 0);
			positionY -=speedPlatForm;
			
			if (tempPlatForm.transform.position.y < m_posY)
			{	platFormState = statePistonArray.PlatFormUp;
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
