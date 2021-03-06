﻿using UnityEngine;
using System.Collections;



public class BlockBehaviour : MonoBehaviour
{

    #region Public Properties
    [Range(0.0f,0.4f)]
    public float m_shakeIntensity;
    [Range(8000f,14000f)]
    public float m_forceMove;
    public AudioClip m_bumpSound;
    public AudioClip m_breakSound;
    public AudioClip m_moveSound;
    #endregion

    #region Main methods
    void Start()
    {
        m_rgbd2d = GetComponent<Rigidbody2D>();
        m_initialPosition = transform.position;
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_characterRGBD = m_player.GetComponent<Rigidbody2D>();
        m_BlockSound = GetComponent<AudioSource>();
        m_BlockSound.loop = false;

        if (gameObject.CompareTag("Destructible"))
        {
            m_animDestruct = GetComponentInChildren<Animator>();
            m_animDestruct.enabled = false;
            m_particleSystem = GetComponentInChildren<ParticleSystem>();
            m_particleSystem.Pause();
            m_boxColl = GetComponent<BoxCollider2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If a Pushable Block is triggered by a Push Wave
        if (gameObject.CompareTag("Pushable") && other.gameObject.CompareTag("PushWave"))
        {
            float OtherVeloX = other.GetComponent<Rigidbody2D>().velocity.x;
            PlayMoveSound();

            // Check direction and apply move forces
            if (OtherVeloX > 0)
            {
                m_rgbd2d.AddForce(Vector2.right * m_forceMove, ForceMode2D.Impulse);
            }

            else if (OtherVeloX < 0)
            {
                m_rgbd2d.AddForce(Vector2.left * m_forceMove, ForceMode2D.Impulse);
            }
        }

        // If a Destructible Block is triggered by a Destroy Wave 
        else if (gameObject.CompareTag("Destructible") && other.gameObject.CompareTag("DestroyWave"))
        {
            // Destroy Block
            StartCoroutine(DestroyBlock());
        }

        // In all other cases (Destructible block with Push Wave or Pushable Block with Destroy Wave)
        else
        {
            StartCoroutine(Shake());
        }
    }





    IEnumerator DestroyBlock()
    {
        PlayBreakSound();

        m_animDestruct.enabled = true;          // Enable animator
        m_particleSystem.Play();                // Start particle effect
        m_boxColl.enabled = false;              // Disable collider (don't stop the player)
        yield return new WaitForSeconds(1);     // Be sure the animation is ended before destroy object
        Destroy(gameObject);                    // Destroy it
    }

    IEnumerator Shake()
    {

        PlayBumpSound();

        m_initialPosition = transform.position;
        float m_initialShakeIntensity = m_shakeIntensity;

        while (m_shakeIntensity > 0)
        {
            transform.position = m_initialPosition + Random.insideUnitCircle * m_shakeIntensity;
            m_shakeIntensity -= Time.deltaTime * 0.8f;  // to decrease the shake effect
            yield return new WaitForSeconds(0.015f);
        }

        if (m_shakeIntensity <= 0)
        {
            transform.position = m_initialPosition;
            m_shakeIntensity = m_initialShakeIntensity;
        }
    }


    void PlayBumpSound()
    {
        m_BlockSound.clip = m_bumpSound;
        m_BlockSound.volume = 0.5f;
        m_BlockSound.Play(); 
    }

    void PlayBreakSound()
    {
        m_BlockSound.clip = m_breakSound;
        m_BlockSound.Play();
    }

    void PlayMoveSound()
    {
        m_BlockSound.clip = m_moveSound;
        m_BlockSound.Play();
    }
    #endregion

    #region Private properties
    Rigidbody2D m_rgbd2d;
    Vector2 m_initialPosition;
    Animator m_animDestruct;
    ParticleSystem m_particleSystem;
    Collider2D m_boxColl;
    GameObject m_player;
    Rigidbody2D m_characterRGBD;
    AudioSource m_BlockSound;
    #endregion

}

