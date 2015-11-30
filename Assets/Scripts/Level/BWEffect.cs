using UnityEngine;
using System.Collections;

public class BWEffect : MonoBehaviour 
{

	// BWEffect is the shader applied to the camera to manage the background's greyscale fade


	#region Public properties

		public float m_intensity;		// Intensity of the shader

	#endregion


	#region main methods

		void Awake ()
		{
			material = new Material( Shader.Find("Custom/BWDiffuse") );
		}

		void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
			if (getIntensity() == 0)
			{
				Graphics.Blit (source, destination);
				return;
			}
			
			material.SetFloat("_bwBlend", getIntensity());
			Graphics.Blit (source, destination, material);
		}

	#endregion


	#region accessors
		
		public float getIntensity()
		{
			return m_intensity;
		}

		public void setIntensity(float newIntensity)
		{
			if (newIntensity >= 0 && newIntensity <= 1)
			{
				m_intensity = newIntensity;
			}
		}

	#endregion


	#region private properties	
	
		private Material material;		// Custom Grayscale shader

	#endregion
}