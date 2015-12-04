// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class BWEffect : MonoBehaviour 
{

	// BWEffect is the shader applied to the camera to manage the background greyscale fade


	#region main methods

		void Awake ()
		{
			material = new Material( Shader.Find("Custom/BWDiffuse") );
        Debug.Log(material);
		}

		void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            Debug.Log(getIntensity());
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
			return intensity;
		}

		public void setIntensity(float newIntensity)
		{
			if (newIntensity >= 0 && newIntensity <= 1)
			{
				intensity = newIntensity;
			}
		}

	#endregion


	#region private properties	
	
		private Material material;		// Custom Grayscale shader	
		private float intensity;		// Intensity of the shader

	#endregion
}