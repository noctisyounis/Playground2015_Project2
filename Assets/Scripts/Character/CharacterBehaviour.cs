using UnityEngine;
using System.Collections;
using UnityEditor;

public class CharacterBehaviour : MonoBehaviour
{

    //      TODO :
    //      Stop walking when shooting
    //      Link jump animator

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
    public int m_Pv;
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
        m_Animator = GetComponentInChildren<Animator>();
        m_SelectedAmmo = Ammo.DestroyWave;
        m_Pv = 3;
        m_IsVulnerable = true;
        m_IsShooting = false;
        m_RendList = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
    }
    
    void FixedUpdate()
    {
        switch (m_playerState)
        {
            case PlayerState.Idle :
                Move();
                Jump();

                if(m_stateChanged)
                { 
                    m_Animator.CrossFade("Idle",0.1f);
                    m_stateChanged = false;
                }

                StartCoroutine(Shoot());

                ChangeState();
                break;

            case PlayerState.Walk :
                Move(); // IMPORTANT !  When the character is walking, he must be able to continue to walk (at the next frame) !!
                Jump();
                if(m_stateChanged)
                { 
                    m_Animator.CrossFade("Walk",0.1f);
                    m_stateChanged = false;
                }
                ChangeState();
                break;

            case PlayerState.Jump :
                ApplyGravity();
                Move();

                ChangeState();
                break;

            case PlayerState.Shoot :
                if(m_stateChanged)
                { 
                    m_Animator.CrossFade("Shoot",0.1f);
                    m_stateChanged = false;
                }
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
        }
    }

    IEnumerator Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_IsShooting = true;

            Vector3 ShootPosition = new Vector3(1.3f, 1.4f, 0);
            Vector2 ShootDirection = Vector2.right;

            // Synchronise with animation
            yield return new WaitForSeconds(0.5f);

            // Adapt prefab type and intantiate the wave
            string PrefabName = (m_SelectedAmmo == Ammo.DestroyWave) ? "DestroyWavePrefab" : "PushWavePrefab";
            GameObject WavePrefab = (GameObject)Instantiate(Resources.Load("prefabs/Character/" + PrefabName));

            // If the character is looking in the other direction
            if (!m_FacingRight)
            {
                // Flip position and direction
                ShootPosition.x = ShootPosition.x * -1;
                ShootDirection = Vector2.left;

                // Flip the wave renderer on x
                Vector3 WaveScale = WavePrefab.transform.localScale;
                WaveScale.x *= -1;
                WavePrefab.transform.localScale = WaveScale;
            }

            // Put the wave position at the current character position
            WavePrefab.transform.position = m_CurrentPosition + ShootPosition;
            // Apply force and direction to the Wave velocity
            WavePrefab.GetComponent<Rigidbody2D>().velocity = ShootDirection * m_ShootForce;
            // Delay to close animation
            yield return new WaitForSeconds(0.3f);

            m_IsShooting = false;

        }
    }

    void ChangeState()
    {
        if (CheckGround())
        {
            if (CheckWalk())
            {
                m_previousState = m_playerState;
                m_playerState = PlayerState.Walk;
            }

            else if (m_IsShooting)
            {
                m_previousState = m_playerState;
                m_playerState = PlayerState.Shoot;
            }

            else
            {
                m_previousState = m_playerState;
                m_playerState = PlayerState.Idle;
            }
        }
        else
        {
            m_previousState = m_playerState;
            m_playerState = PlayerState.Jump;
        }

        if(m_playerState != m_previousState)
        {
            m_stateChanged = true;
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

    public void AddPV(int value)
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

            if (m_Pv + value <= 0)
            {
                m_Pv = 0;
                Death();
            }

            else
            {
                m_Pv += value;
            }
        }
    }

    IEnumerator SetInvulnerable(float seconds = 2.5f)
    {
        m_IsVulnerable = false;
        StartCoroutine("Blink");
        yield return new WaitForSeconds(seconds);
        StopCoroutine("Blink");
        foreach (SpriteRenderer sr in m_RendList)
        {
            sr.enabled = true;
        }
        m_IsVulnerable = true;
    }

    IEnumerator Blink()
    {
        bool value = false;
        
        while (true)
        {
            foreach (SpriteRenderer sr in m_RendList)
            {
                sr.enabled = value;
            }

            value = !value;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Death()
    {
        Destroy(gameObject);

        //string MsgTitre = "Mort";
        //string MsgContent = "Vous êtes mort";
        //string MsgOk = "Ok";
        //EditorUtility.DisplayDialog(MsgTitre, MsgContent, MsgOk);

        // Respawn ..
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
    int m_MaxPV;
    bool m_IsShooting;
    SpriteRenderer[] m_RendList;
    bool m_stateChanged = true;
    PlayerState m_previousState;
    #endregion


}


