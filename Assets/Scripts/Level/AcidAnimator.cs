using UnityEngine;
using System.Collections;

public class AcidAnimator : MonoBehaviour 
{
	public Material m_acidMaterial;
	public Vector2 m_speed;

	public bool m_sinusOnX;

	void Update()
	{
		float offsetX = m_speed.x * Time.deltaTime;
		float offsetY = m_speed.y * Time.deltaTime;

		if (m_sinusOnX) 
		{
			offsetX *= Mathf.Sin( Time.time ); 
		}
		m_acidMaterial.mainTextureOffset = new Vector2(m_acidMaterial.mainTextureOffset.x + offsetX, m_acidMaterial.mainTextureOffset.y + offsetY);
	} 
}
