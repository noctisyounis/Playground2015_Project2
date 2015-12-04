// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour 
{

	// Parallaxing manage background's parallax. It needs prefabs transforms to be attached.


	#region public properties
    
		public Transform[] m_backgrounds;         // Array of Backgrounds to be parallaxed
		public float m_smoothing = 1f;            // Smooth of parallax effect

	#endregion


	#region main methods

		void Awake ()
		{
			camera = GameObject.FindGameObjectWithTag("Background").GetComponent<Camera>().transform;
		}

		void Start () 
		{
			// Camera initialization
			previousCameraPosition = camera.position;

			// Get all grounds
			float totalGrounds = m_backgrounds.Length + 1;
			allGrounds = new Transform[(int)totalGrounds];
			int i;
			for (i = 0; i< totalGrounds-1; i++) 
			{
				allGrounds[i] = m_backgrounds [i];
			}
			allGrounds [i] = GameObject.FindGameObjectWithTag ("Foreground").transform;
			
			// Parallax Scales initialization
			parallaxScales = new float[(int)totalGrounds];
			for (int j = 0; j< totalGrounds-1; j++) 
			{
				parallaxScales [j] = allGrounds [j].position.z * -1;
			}
		}

		void Update () 
		{		
			// Set looping limits
			limit.x = camera.position.x - 60;
			respawn.x = camera.position.x + 60;

			// For each backgrounds
			for (int i = 0; i< allGrounds.Length; i++) 
			{
				// Parallax calculation
				float parallax = (previousCameraPosition.x - camera.position.x) * parallaxScales[i];
				// New X position calculation
				float backgroundTargetPositionX = allGrounds[i].position.x + parallax;
				// New position vector creation
				Vector3 backgroundTargetPosition = new Vector3 (backgroundTargetPositionX, allGrounds[i].position.y, allGrounds[i].position.z);
				// Fade between old and new position
				allGrounds[i].position = Vector3.Lerp (allGrounds[i].position, backgroundTargetPosition, m_smoothing * Time.deltaTime);

				if (i < allGrounds.Length-1)
				{
					foreach(Transform child in allGrounds[i])
					{
						// Looping effect
						if (isBehindBorder (child, limit.x, respawn.x)) 
						{
							if (child.position.x < limit.x)
							{
								child.position = new Vector3 (respawn.x, child.position.y, child.position.z);
							}
							else
							{
								child.position = new Vector3 (limit.x, child.position.y, child.position.z);
							}
						}
					}
				}
			}

			// Set the camera position to the previousCameraPosition
			previousCameraPosition = camera.position;
		}
		
		bool isBehindBorder(Transform item, float limit, float respawn)
		{
			return (item.position.x < limit || item.position.x > respawn);		
		}

	#endregion


	#region private properties

		private Transform[] allGrounds;				// All grounds to be parallaxed
		private float[] parallaxScales;
		private Transform camera;               
		private Vector3 previousCameraPosition; 
		private Vector2 limit;
		private Vector2 respawn;

	#endregion
}
