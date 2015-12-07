using UnityEngine;
using System.Collections;

public class CharacterSound : MonoBehaviour
{

    #region Public properties
    public AudioClip m_PushShootSound;
    public AudioClip m_DestroyShootSound;
    public AudioClip m_JumpSound;
    public AudioClip m_HurtSound;
    public AudioClip m_SwitchAmmoSound;
    #endregion

    void Start ()
    {
        m_AudioSource = GetComponentInChildren<AudioSource>();
        m_AudioSource.loop = false;
	}
	

	void Update ()
    {
	
	}

    public void PlayJumpSound()
    {
        m_AudioSource.clip = m_JumpSound;
        m_AudioSource.volume = 0.5f;
        m_AudioSource.Play();
    }

    public void PlayPushShootSound()
    {
        m_AudioSource.clip = m_PushShootSound;
        m_AudioSource.Play();
    }

    public void PlayDestroyShootSound()
    {
        m_AudioSource.clip = m_DestroyShootSound;
        m_AudioSource.Play();
    }

    public void PlayHurtSound()
    {
        m_AudioSource.clip = m_HurtSound;
        m_AudioSource.Play();
    }

    public void PlaySwitchAmmoSound()
    {
        m_AudioSource.clip = m_SwitchAmmoSound;
        m_AudioSource.Play();
    }

    #region Private properties
    AudioSource m_AudioSource;
    #endregion

}
