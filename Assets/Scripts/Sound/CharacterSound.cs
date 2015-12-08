using UnityEngine;
using System.Collections;

public class CharacterSound : MonoBehaviour
{

    #region Public properties
    public AudioSource m_AudioSource1;
    public AudioSource m_AudioSource2;
    public AudioSource m_AudioSource3;
    public AudioSource m_AudioSource4;

    public AudioClip m_PushShootSound;
    public AudioClip m_DestroyShootSound;
    public AudioClip m_JumpSound;
    public AudioClip m_HurtSound;
    public AudioClip m_SwitchAmmoSound;
    public AudioClip m_LifeUpSound;
    public AudioClip m_DeathSound;
    #endregion

    void Start ()
    {
        // Need to instantiate many AudioSources to play many sounds at the same time

        m_AudioSource1.loop = false;       
        m_AudioSource2.loop = false;
        m_AudioSource3.loop = false;
        m_AudioSource4.loop = false;
    }
	

	void Update ()
    {
	
	}

    public void PlayJumpSound()
    {
        m_AudioSource3.clip = m_JumpSound;
        m_AudioSource3.volume = 0.5f;
        m_AudioSource3.Play();
    }

    public void PlayPushShootSound()
    {
        m_AudioSource1.clip = m_PushShootSound;
        m_AudioSource1.Play();
    }

    public void PlayDestroyShootSound()
    {
        m_AudioSource1.clip = m_DestroyShootSound;
        m_AudioSource1.Play();
    }

    public void PlayHurtSound()
    {
        m_AudioSource2.clip = m_HurtSound;
        m_AudioSource2.Play();
    }

    public void PlaySwitchAmmoSound()
    {
        m_AudioSource4.clip = m_SwitchAmmoSound;
        m_AudioSource4.Play();
    }

    public void PlayLifeUpSound()
    {
        m_AudioSource2.clip = m_LifeUpSound;
        m_AudioSource2.Play();
    }

    public void PlayDeathSound()
    {
        m_AudioSource3.clip = m_DeathSound;
        m_AudioSource3.Play();
    }



}
