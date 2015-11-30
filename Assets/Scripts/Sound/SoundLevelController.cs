using UnityEngine;
using System.Collections;

public class SoundLevelController : MonoBehaviour
{

    #region Public Properties
    public AudioSource m_AudioSource1;
    public AudioSource m_AudioSource2;
    public float m_TotalCount;
    public float m_GrabbedCount;
    #endregion

    void Start ()
    {
        m_HighPassFilter = m_AudioSource1.GetComponent<AudioHighPassFilter>();
        m_HighPassFilter.cutoffFrequency = 600;

        m_DistortionFilter = m_AudioSource1.GetComponent<AudioDistortionFilter>();
        m_DistortionFilter.distortionLevel = 0.8f;

        m_AudioSource1.volume = 0.00f;
        m_AudioSource2.volume = 1.00f;

        m_AudioSource1.Play();
        m_AudioSource2.Play();
    }


    void FixedUpdate ()
    {
        Balance();
	}


    void Balance()
    {
        // Calcul percentage of items found
        float BalancePercent = m_GrabbedCount / m_TotalCount;

        // Ajust High Pass Filter
        m_HighPassFilter.cutoffFrequency = 600 - (600 * BalancePercent);

        // Ajust distortion level
        m_DistortionFilter.distortionLevel = 0.8f - (0.8f * BalancePercent);

        // Up the song volume
        m_AudioSource1.volume = 0.00f + BalancePercent;
        // Down the scratches volume
        m_AudioSource2.volume = 1.00f - BalancePercent;
    }




    #region Private properties
    AudioHighPassFilter m_HighPassFilter;
    AudioDistortionFilter m_DistortionFilter;
    #endregion

}
