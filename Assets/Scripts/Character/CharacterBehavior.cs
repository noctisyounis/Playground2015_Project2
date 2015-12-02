﻿using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

    //      TODO :
    //      Stop walking when shooting
    //      Link jump animator
    //      yield WaitForAnimation( animation ); !!!!!


    #region Public properties
    public PlayerState m_playerState;
    public string m_Axis;
    public float m_MoveSpeed;
    public float m_JumpForce;
    public Transform m_GroundCheck;
    public LayerMask m_GroundLayer;
    public Ammo m_SelectedAmmo;
    public float m_gravityForce;
    public float m_ShootForce;
    public int Pv { get; private set; }
    #endregion

    #region Enums
    public enum Ammo
    {
        DestroyWave ,
        PushWave
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Jump ,
        Shoot
    }
    #endregion

    #region Main methods
    void Start()
    {
        m_playerState = PlayerState.Idle;
        m_rgbd = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
        m_Animator = GetComponent<Animator>();
        m_SelectedAmmo = Ammo.DestroyWave;
        Pv = 3;
        m_IsVulnerable = true;
        m_IsShooting = false;
    }
    
    void FixedUpdate()
    {
        switch (m_playerState)
        {
            case PlayerState.Idle :
                Move();
                Jump();
                StartCoroutine(Shoot());
                m_Animator.Play("Idle");

                ChangeState();
                break;

            case PlayerState.Walk :
                Move(); // IMPORTANT !  When the character is walking, he must be able to continue to walk (at the next frame) !!
                Jump();
                m_Animator.Play("Walk");

                ChangeState();
                break;

            case PlayerState.Jump :
                ApplyGravity();
                Move();

                ChangeState();
                break;

            case PlayerState.Shoot :
                m_Animator.Play("Shoot");

                ChangeState();
                break;

            default:
                break;
        }

        SwitchAmmo();
        m_CurrentPosition = transform.position;    
    }
    #endregion

    #region Utils
   
    void Move()
    {
        Axis = Input.GetAxis(m_Axis);

        CheckFacingDirection(Axis);

        if (Mathf.Abs(Axis) > 0)
        {
            //m_Animator.Play("Walk");
            m_rgbd.velocity = new Vector2(Axis * m_MoveSpeed, m_rgbd.velocity.y);
        }

    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_rgbd.AddForce(new Vector2(m_rgbd.velocity.x, m_JumpForce));
            Debug.Log("Jumped");
        }
    }

    IEnumerator Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_IsShooting = true;

            Vector3 ShootPosition = new Vector3(1.5f, 1.4f, 0);

            if (!m_FacingRight)
            {
                ShootPosition.x = ShootPosition.x * -1;
            }

            // Synchronisation with animation
            yield return new WaitForSeconds(0.5f);
            // Check the current character direction
            Vector2 ShootDirection = m_FacingRight ? Vector2.right : Vector2.left;
            // Adapt prefab type and intantiate the wave
            string PrefabName = (m_SelectedAmmo == Ammo.DestroyWave) ? "DestroyWavePrefab" : "PushWavePrefab";
            GameObject WavePrefab = (GameObject)Instantiate(Resources.Load("prefabs/Character/" + PrefabName));
            // Bullet position at the current character position    
            WavePrefab.transform.position = m_CurrentPosition + ShootPosition;
            // Apply force and direction to the Wave velocity
            WavePrefab.GetComponent<Rigidbody2D>().velocity = ShootDirection * m_ShootForce;
            // Delay
            yield return new WaitForSeconds(0.5f);
            // After 1sec, destroy the Wave GameObject
            Destroy(WavePrefab);

            m_IsShooting = false;

        }
    }

    void ChangeState()
    {
        if (CheckGround())
        {
            if (CheckWalk())
            {
                m_playerState = PlayerState.Walk;
            }

            else if (m_IsShooting)
            {
                m_playerState = PlayerState.Shoot;
            }

            else
            {
                m_playerState = PlayerState.Idle;
            }
        }
        else
        {
            m_playerState = PlayerState.Jump;
        }
    }

    void CheckFacingDirection(float Axis)
    {       
        if ((Axis < 0 && m_FacingRight) || (Axis > 0 && !m_FacingRight))
        {
            m_FacingRight = !m_FacingRight;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 CharacterScale = transform.localScale;
        CharacterScale.x *= -1;
        transform.localScale = CharacterScale;
    }

    bool CheckWalk()
    {
        return ( Mathf.Abs(Input.GetAxisRaw("Horizontal") ) > 0 );
    }

    bool CheckGround()
    {      
        bool IsGroundedCenter;
        bool IsGroundedRight;
        bool IsGroundedLeft;

        // Calcul a clipping compared to Center groundcheck
        Vector3 DecalPos = new Vector3(0.35f, 0, 0);
        Vector3 DecalNeg = new Vector3(-0.35f, 0, 0);

        IsGroundedCenter = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);
        IsGroundedRight = Physics2D.Linecast(transform.position + DecalPos, m_GroundCheck.position, m_GroundLayer);
        IsGroundedLeft = Physics2D.Linecast(transform.position + DecalNeg, m_GroundCheck.position, m_GroundLayer);
        
        // --- Display lines on scene
         Debug.DrawLine(transform.position, m_GroundCheck.position);
         Debug.DrawLine(transform.position + DecalPos, m_GroundCheck.position + DecalPos);
         Debug.DrawLine(transform.position + DecalNeg, m_GroundCheck.position + DecalNeg);

        return (IsGroundedCenter || IsGroundedRight || IsGroundedLeft);
    }

    bool CheckDirection()
    {
        return (m_rgbd.velocity.x > 0); 
    }

    void SwitchAmmo()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (m_SelectedAmmo == Ammo.DestroyWave)
            {
                m_SelectedAmmo = Ammo.PushWave;
                Debug.Log("Ammo switched to Push Wave");
            }

            else
            {
                m_SelectedAmmo = Ammo.DestroyWave;
                Debug.Log("Ammo switched to Destroy Wave");
            }
        }
    }

    void ApplyGravity()
    {
        m_rgbd.velocity = new Vector2(m_rgbd.velocity.x, m_rgbd.velocity.y - m_gravityForce * Time.deltaTime);
    }

    void AddPV(int value)
    {
        if (value >= 0)
        {
            if (m_Pv + value > m_MaxPV)
            {
                m_Pv = m_MaxPV;
            }

            else
            {
                m_Pv += value;
            }
        }

        else if (m_IsVulnerable)
        {
            StartCoroutine(SetInvulnerable());

            if (m_Pv - value < 0)
            {
                m_Pv = 0;
            }

            else
            {
                m_Pv += value;
            }
        }
    }

    IEnumerator SetInvulnerable(float seconds = 4)
    {
        m_IsVulnerable = false;
        StartCoroutine("Blink");
        yield return new WaitForSeconds(seconds);
        StopCoroutine("Blink");
        m_IsVulnerable = true;
    }

    IEnumerator Blink()
    {
        bool value = false;
        SpriteRenderer[] RendList = gameObject.GetComponentsInChildren<SpriteRenderer>(true);

        while (true)
        {
            foreach (SpriteRenderer sr in RendList)
            {
                sr.enabled = value;
            }

            value = !value;
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion

    #region Tools
    void OnGUI()
    {
        GUILayout.Button("State : " + m_playerState + "" + "\n" + " Axis " + Input.GetAxisRaw("Horizontal") + "\n" + "x: " + m_rgbd.velocity.x + " \n y:" + m_rgbd.velocity.y);
    }
    #endregion

    #region Private properties
    Rigidbody2D m_rgbd;
    InputManager m_inputMngr;
    float Axis;
    bool m_FacingRight;
    bool m_IsVulnerable;
    Animator m_Animator;
    Vector3 m_CurrentPosition;
    int m_Pv;
    int m_MaxPV;
    bool m_IsShooting;
    #endregion


}


