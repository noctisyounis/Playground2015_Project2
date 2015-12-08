using UnityEngine;
using System.Collections;

public class ScriptPlatFormMoveHorizontal : MonoBehaviour {

    [Range(0.001f,0.500f)]
    public float m_speedPlatForm;

    public float m_interSecondePlatForm; 
	float m_posX; // the point (m_posX,m_pos) will move to the right first
	float m_posY;
	

	private float positionX;
	private float positionY;
		
	private int timeCompter;
	
	private enum statePistonArray {PlatformRight,PlatformLeft,PlatformPause };
	private statePistonArray platformState;

	private float DistancePlateForm = 2.0f;
	private float posX2;
	

	void Start () 
	{
        m_posX = transform.position.x;
        m_posY = transform.position.y;


        positionX = m_posX;
		positionY = m_posY;

		posX2 = m_posX + DistancePlateForm;
		
		
		platformState = statePistonArray.PlatformRight;
		
		transform.position = new Vector3(positionX,positionY, 0);





        //	timeCompter = 0;
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
			transform.position = new Vector3(positionX,positionY, 0);
			positionX += m_speedPlatForm;
			
			
			if (transform.position.x> posX2)
			{
				platformState = statePistonArray.PlatformLeft;
			}
		}
		
		
		if ( (platformState==statePistonArray.PlatformLeft) ) //&& (count%15)==0)
		{	
			transform.position = new Vector3(positionX,positionY , 0);
			positionX -= m_speedPlatForm;
			
			if ( transform.position.x < m_posX)
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
