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
        m_AudioSource1.volume = 0.00f;
        m_AudioSource2.volume = 1.00f;

        m_AudioSource1.Play();
        m_AudioSource2.Play();
    }


    void FixedUpdate ()
    {
        Balance(m_TotalCount,m_GrabbedCount);
	}


    void Balance(float totalCount, float grabbedCount)
    {
        float BalanceRate;
        BalanceRate = grabbedCount / totalCount;

        m_AudioSource1.volume = 0.00f + BalanceRate;
        m_AudioSource2.volume = 1.00f - BalanceRate;
    }


    #region Private properties
    #endregion

}
