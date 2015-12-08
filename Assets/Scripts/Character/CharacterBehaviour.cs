using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{

    //      TODO :

    //      Piston (scale one part) length = 3

    //      End level music when game over
    //      End level animation
    //      End level music
    //      Door animation



    //      (Desactive rub on sides)
    //      (Player rebound when hurts)
    //      (Sound menu)


    #region Public properties
    public PlayerState m_playerState;
    public string m_Axis;
    public float m_MoveSpeed;
    public float m_JumpForce;
    public Transform m_GroundCheck;
    public Transform m_CeilingCheck;
    public Transform m_LeftWallCheck;
    public Transform m_RightWallCheck;
    public LayerMask m_GroundLayer;
    public Ammo m_SelectedAmmo;
    public float m_gravityForce;
    public float m_ShootForce;
    public float m_airControlRatio;
    public float m_initialFriction;
    #endregion

    #region Enums
    public enum Ammo
    {
        DestroyWave ,
        PushWave
    }

    public enum PlayerState
    {
        Idle ,
        Walk ,
        Jump ,
        Shoot ,
        Dead
    }
    #endregion

    #region Main methods
    void Start()
    {
        m_playerState = PlayerState.Idle;
        m_rgbd = GetComponent<Rigidbody2D>();
        m_boxColl = GetComponent<BoxCollider2D>();
        m_FacingRight = true;
        m_Animator = GetComponentInChildren<Animator>();
        m_SelectedAmmo = Ammo.PushWave;
        m_Pv = 3;
        m_MaxPV = 3;
        m_IsVulnerable = true;
        m_IsShooting = false;
        m_RendList = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        m_Sound = GetComponent<CharacterSound>();
    }
    
    void FixedUpdate()
    {
        m_CurrentPosition = transform.position;

        switch (m_playerState)
        {
            case PlayerState.Idle :
                Move();
                Jump();
                m_Animator.Play("Idle");
                m_stateChanged = false;
                m_boxColl.sharedMaterial.friction = m_initialFriction;
                StartCoroutine(Shoot());
                SwitchAmmo();
                ChangeState();
                break;

            case PlayerState.Walk :
                Move(); // IMPORTANT !  When the character is walking, he must be able to continue to walk (at the next frame) !!
                Jump();
                StartCoroutine(Shoot());
                m_Animator.Play("Walk");
                m_stateChanged = false;
                m_boxColl.sharedMaterial.friction = m_initialFriction;
                SwitchAmmo();
                ChangeState();
                break;

            case PlayerState.Jump :
                ApplyGravity();
                Move();
                m_boxColl.sharedMaterial.friction = 0;      // Disable friction
                m_Animator.Play("Jump");
                SwitchAmmo();
                ChangeState();
                break;

            case PlayerState.Shoot :
                m_Animator.Play("Shoot");
                m_stateChanged = false;
                ChangeState();
                break;

            case PlayerState.Dead :
                m_Animator.Play("Fail");
                break;

            default:
                break;
        }

        
        CheckCeiling();
        CheckWallTouchLeft();
        CheckWallTouchRight();      
    }
    #endregion

    #region Utils
   
    void Move()
    {
        Axis = Input.GetAxis(m_Axis);

        CheckFacingDirection(Axis);

        if (Mathf.Abs(Axis) > 0)
        {
            if (!CheckGround())
            {
                m_rgbd.velocity = new Vector2(Axis * m_MoveSpeed * m_airControlRatio, m_rgbd.velocity.y);
                return;
            }

            m_rgbd.velocity = new Vector2(Axis * m_MoveSpeed, m_rgbd.velocity.y);
        }



    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(m_Sound);
            m_Sound.PlayJumpSound();

            Vector2 AddJumpForce = new Vector2(Mathf.Clamp(m_rgbd.velocity.x,-4f,4f), m_JumpForce);
            m_rgbd.AddForce(AddJumpForce);
        }
    }

    IEnumerator Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            m_IsShooting = true;

            Vector3 ShootPosition = new Vector3(1.3f, 1.4f, 0);
            Vector2 ShootDirection = Vector2.right;

            // Synchronise with animation
            yield return new WaitForSeconds(0.5f);

            // Adapt prefab type and intantiate the wave
            string PrefabName = (m_SelectedAmmo == Ammo.DestroyWave) ? "DestroyWavePrefab" : "PushWavePrefab";

            if (PrefabName == "PushWavePrefab")
            {
                m_Sound.PlayPushShootSound();
            }
            else
            {
                m_Sound.PlayDestroyShootSound();
            }

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
            yield return new WaitForSeconds(0.485f);

            m_IsShooting = false;

        }
    }

    void ChangeState()
    {
        if (CheckGround())
        {
            if (CheckWalk() && !m_IsShooting)
            {
                m_playerState = PlayerState.Walk;
            }

            else if (m_IsShooting)
            {
                m_previousState = m_playerState;
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
        //m_Animator.Play("Walk");
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

        float ColSizeX = m_boxColl.size.x;

        // Calcul a clipping compared to Center groundcheck
        Vector3 DecalPos = new Vector3(ColSizeX*0.5f, 0, 0);
        Vector3 DecalNeg = new Vector3(ColSizeX*0.5f*-1, 0, 0);

        IsGroundedCenter = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);
        IsGroundedRight = Physics2D.Linecast(transform.position + DecalPos, m_GroundCheck.position, m_GroundLayer);
        IsGroundedLeft = Physics2D.Linecast(transform.position + DecalNeg, m_GroundCheck.position, m_GroundLayer);

        // --- Display lines on scene
        //Debug.DrawLine(transform.position, m_GroundCheck.position);
        //Debug.DrawLine(transform.position + DecalPos, m_GroundCheck.position + DecalPos);
        //Debug.DrawLine(transform.position + DecalNeg, m_GroundCheck.position + DecalNeg);

        return (IsGroundedCenter || IsGroundedRight || IsGroundedLeft);
    }

    bool CheckCeiling()
    {
       
        bool IsCeilingCenter;
        bool IsCeilingRight;
        bool IsCeilingLeft;

        float ColSizeX = m_boxColl.size.x;
        float ColSizeY = m_boxColl.size.y;

        // Calcul a clipping compared to Center Ceilingcheck
        Vector3 DecalPos = new Vector3(ColSizeX * 0.5f, 0, 0);
        Vector3 DecalNeg = new Vector3(ColSizeX * 0.5f * -1, 0, 0);

        // Grab the player collider height and apply to position
        Vector3 StartPoint = new Vector3(0, ColSizeY) + transform.position;

        IsCeilingCenter = Physics2D.Linecast(StartPoint, m_CeilingCheck.position, m_GroundLayer);
        IsCeilingRight = Physics2D.Linecast(StartPoint + DecalPos, m_CeilingCheck.position, m_GroundLayer);
        IsCeilingLeft = Physics2D.Linecast(StartPoint + DecalNeg, m_CeilingCheck.position, m_GroundLayer);

        // --- Display lines on scene
        //Debug.DrawLine(StartPoint, m_CeilingCheck.position);
        //Debug.DrawLine(StartPoint + DecalPos, m_CeilingCheck.position + DecalPos);
        //Debug.DrawLine(StartPoint + DecalNeg, m_CeilingCheck.position + DecalNeg);

        return (IsCeilingCenter || IsCeilingRight || IsCeilingLeft);
    }

    bool CheckWallTouchLeft()
    {
        bool IsWallTouchMiddle;
        bool IsWallTouchTop;
        bool IsWallTouchBottom;

        float ColSizeX = m_boxColl.size.x;
        float ColSizeY = m_boxColl.size.y;

        // Calcul a clipping compared to Center groundcheck
        float Offset = 0.2f;
        Vector3 DecalPos = new Vector3(0, ColSizeY * 0.5f - Offset, 0);
        Vector3 DecalNeg = new Vector3(0, ColSizeY * -0.5f + Offset, 0);

        Vector3 StartPoint = new Vector3();
        if (!m_FacingRight)
        {
            StartPoint = new Vector3(ColSizeX * 0.5f, ColSizeY * 0.5f) + transform.position;
        }

        else
        {
            StartPoint = new Vector3(-ColSizeX * 0.5f, ColSizeY * 0.5f) + transform.position;
        }

        IsWallTouchMiddle = Physics2D.Linecast(StartPoint, m_LeftWallCheck.position, m_GroundLayer);
        IsWallTouchTop = Physics2D.Linecast(StartPoint + DecalPos, m_LeftWallCheck.position, m_GroundLayer);
        IsWallTouchBottom = Physics2D.Linecast(StartPoint + DecalNeg, m_LeftWallCheck.position, m_GroundLayer);

        // --- Display lines on scene
        //Debug.DrawLine(StartPoint, m_LeftWallCheck.position);
        //Debug.DrawLine(StartPoint + DecalPos, m_LeftWallCheck.position + DecalPos);
        //Debug.DrawLine(StartPoint + DecalNeg, m_LeftWallCheck.position + DecalNeg);

        return (IsWallTouchMiddle || IsWallTouchTop || IsWallTouchBottom);
    }

    bool CheckWallTouchRight()
    {
        bool IsWallTouchMiddle;
        bool IsWallTouchTop;
        bool IsWallTouchBottom;

        float ColSizeX = m_boxColl.size.x;
        float ColSizeY = m_boxColl.size.y;

        // Calcul a clipping compared to Center groundcheck
        float Offset = 0.2f;
        Vector3 DecalPos = new Vector3(0, ColSizeY * 0.5f - Offset, 0);
        Vector3 DecalNeg = new Vector3(0, ColSizeY * -0.5f + Offset, 0);

        Vector3 StartPoint = new Vector3();
        if (!m_FacingRight)
        {
            StartPoint = new Vector3(-ColSizeX * 0.5f, ColSizeY * 0.5f) + transform.position;
        }

        else
        {
            StartPoint = new Vector3(ColSizeX * 0.5f, ColSizeY * 0.5f) + transform.position;
        }

        IsWallTouchMiddle = Physics2D.Linecast(StartPoint, m_RightWallCheck.position, m_GroundLayer);
        IsWallTouchTop = Physics2D.Linecast(StartPoint + DecalPos, m_RightWallCheck.position, m_GroundLayer);
        IsWallTouchBottom = Physics2D.Linecast(StartPoint + DecalNeg, m_RightWallCheck.position, m_GroundLayer);

        // --- Display lines on scene
        //Debug.DrawLine(StartPoint, m_RightWallCheck.position);
        //Debug.DrawLine(StartPoint + DecalPos, m_RightWallCheck.position + DecalPos);
        //Debug.DrawLine(StartPoint + DecalNeg, m_RightWallCheck.position + DecalNeg);

        return (IsWallTouchMiddle || IsWallTouchTop || IsWallTouchBottom);
    }

    bool CheckDirection()
    {
        return (m_rgbd.velocity.x > 0); 
    }

    void SwitchAmmo()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            m_Sound.PlaySwitchAmmoSound();

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
        // When player wins a life point
        if (value >= 0)
        {
            m_Sound.PlayLifeUpSound();

            if (m_Pv + value > m_MaxPV)
            {
                m_Pv = m_MaxPV;
            }

            else
            {
                m_Pv += value;
            }
        }

        else if (value < 0 && m_IsVulnerable)
        {

            if (m_Pv + value <= 0)
            {
                m_Pv = 0;

                StartCoroutine(Death());
            }

            else
            {
                // When player is hurt !!
                m_Sound.PlayHurtSound();

                StartCoroutine(SetInvulnerable());
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

    IEnumerator Death()
    {
        // Necessary delay to display all level and interface elements while character's death
        yield return new WaitForSeconds(0.1f);

        // When character die, his state change and the player loose all the controls and interactions
        m_playerState = PlayerState.Dead;
        m_Sound.PlayDeathSound();

        // TODO Respawn ..
    }

    public string GetAmmo()
    {
        if (m_SelectedAmmo == Ammo.DestroyWave)
        {
            return "DestroyWave";
        }
        else
        {
            return "PushWave";
        }
    }

    public int GetPv()
    {
        return m_Pv;
    }

    public void SetPv(int NbPv)
    {
        m_Pv = NbPv;
    }


    #endregion

    #region Tools
    void OnGUI()
    {
        // GUILayout.Button("State : " + m_playerState + "" + "\n" + " Axis " + Input.GetAxisRaw("Horizontal") + "\n" + "x: " + m_rgbd.velocity.x + " \n y:" + m_rgbd.velocity.y);

        // GUILayout.Button("State : " + m_playerState + "\n" +"PV : "+m_Pv);
    }
    #endregion

    #region Private properties
    Rigidbody2D m_rgbd;
    BoxCollider2D m_boxColl;
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
    int m_Pv;
    CharacterSound m_Sound;
    
    #endregion


}


