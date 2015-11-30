using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour 
{

	// Parallaxing manage background's parallax. It needs prefabs transforms to be attached.


	#region public properties
    
		public Transform[] m_backgrounds;         // Array of Back- & Foregrounds to be parallaxed
	    public float m_smoothing = 1f;            // Smooth of parallax effect

	#endregion


	#region main methods

		void Awake ()
		{
			camera = Camera.main.transform;
		}

		void Start () 
		{
			// Camera initialization
			previousCameraPosition = camera.position;

			// Parallax Scales initialization
			parallaxScales = new float[m_backgrounds.Length];
			for (int i = 0; i< m_backgrounds.Length; i++) 
			{
				parallaxScales [i] = m_backgrounds [i].position.z * -1;
			}
		}

		void Update () 
		{
			// For each backgrounds
			for (int i = 0; i< m_backgrounds.Length; i++) 
			{
				// Parallax calculation
				float parallax = (previousCameraPosition.x - camera.position.x) * parallaxScales[i];
				// New X position calculation
				float backgroundTargetPositionX = m_backgrounds[i].position.x + parallax;
				// New position vector creation
				Vector3 backgroundTargetPosition = new Vector3 (backgroundTargetPositionX, m_backgrounds[i].position.y, m_backgrounds[i].position.z);
				// Fade between old and new position
				m_backgrounds[i].position = Vector3.Lerp (m_backgrounds[i].position, backgroundTargetPosition, m_smoothing * Time.deltaTime);
			}

			// Set the camera position to the previousCameraPosition
			previousCameraPosition = camera.position;
		}

	#endregion


	#region private properties

		private float[] parallaxScales;
		private Transform camera;               
		private Vector3 previousCameraPosition; 

	#endregion
}
