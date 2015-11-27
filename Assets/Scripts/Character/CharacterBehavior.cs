using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

public class CharacterBehavior : MonoBehaviour
{

    #region Public properties

    public string m_Axis;

    public float m_MoveSpeed = 10.0f;
    public float m_JumpSpeed = 150.0f;
    public float m_ShootForce = 15.0f;

    public Transform m_GroundCheck;
    public LayerMask m_GroundLayer;

    public Ammo m_SelectedAmmo;

    public float m_pv;

    #endregion

    //      TODO :
    //      Stop more longer walking when a non-stop shooting
    //      Stop walking when jumping (just before), then jump
    //      Fall gestion
    //      Directionnal flip-bug
    //      Debug jump+shoot with RETURN : max floor distance calcul !!


    #region Main methods
    void Start ()
    {
        m_RigidB2D = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
        m_MaxFloorDistance = 2f;
        m_CanWalk = true;
        m_SelectedAmmo = Ammo.DestroyWave;
        m_pv = 3f;
    }

    void FixedUpdate ()
    {       
        m_CurrentPosition = transform.position;

        CheckIfGrounded();
        Move();
        Jump();
        SwitchAmmo();

        StopCoroutine(Shoot());
        StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        Vector3 ShootPosition = new Vector3(0, 0.8f, 0);

        // if RETURN is pressed down
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // Disable walk
            m_CanWalk = false;
            // Check the current character direction
            Vector2 ShootDirection = m_FacingRight ? Vector2.right : Vector2.left;
            // Adapt prefab type and intantiate the wave
            string PrefabName = (m_SelectedAmmo == Ammo.DestroyWave) ? "DestroyWavePrefab" : "PushWavePrefab";
            GameObject WavePrefab = (GameObject)Instantiate(Resources.Load("prefabs/Character/" + PrefabName));
            // Bullet position at the current character position    
            WavePrefab.transform.position = m_CurrentPosition + ShootPosition;
            Debug.Log("Position of instantiation : "+WavePrefab.transform.position);
            // Apply force and direction to the Wave velocity
            WavePrefab.GetComponent<Rigidbody2D>().velocity = ShootDirection * m_ShootForce;
            Debug.Log("Velocity : "+WavePrefab.GetComponent<Rigidbody2D>().velocity);
            // Delay
            yield return new WaitForSeconds(1);
            // After 1sec, destroy the Wave GameObject
            Destroy(WavePrefab);
            // Enable walk
            m_CanWalk = true;
        }
    }

    void Move()
    {
        if (m_CanWalk)
        {
            float Axis = Input.GetAxisRaw(m_Axis);
            m_RigidB2D.velocity = new Vector2(Axis, 0) * m_MoveSpeed;
        }
        
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q)) && m_FacingRight)
        {
            m_FacingRight = false;
            Flip();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !m_FacingRight)
        {
            m_FacingRight = true;
            Flip();
        }
    }

    void Jump()
    {
        // Jump only if SPACE is pressed down, if vertical velocity =0, and if character is on a grounded or on a platform
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && m_RigidB2D.velocity.y == 0 && m_IsGrounded)
        {
            m_RigidB2D.velocity = (Vector2.up * m_JumpSpeed);
        }
    }

    void Flip()
    {
        // m_FacingRight = !m_FacingRight;

        Vector3 CharacterScale = transform.localScale;
        CharacterScale.x *= -1;
        transform.localScale = CharacterScale;
    }

    void CheckIfGrounded()
    {
        // Calcul a clipping compared to Center groundcheck
        Vector3 DecalPos = new Vector3(0.35f, 0, 0);
        Vector3 DecalNeg = new Vector3(-0.35f, 0, 0);

        m_IsGroundedCenter = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);
        m_IsGroundedRight = Physics2D.Linecast(transform.position + DecalPos, m_GroundCheck.position, m_GroundLayer);
        m_IsGroundedLeft = Physics2D.Linecast(transform.position + DecalNeg, m_GroundCheck.position, m_GroundLayer);

        // --- Display rays on scene
        // Debug.DrawLine(transform.position, m_GroundCheck.position);
        // Debug.DrawLine(transform.position + DecalPos, m_GroundCheck.position + DecalPos);
        // Debug.DrawLine(transform.position + DecalNeg, m_GroundCheck.position + DecalNeg);

        if (m_IsGroundedCenter || m_IsGroundedRight || m_IsGroundedLeft)
        {
            m_IsGrounded = true;
        }

        else
        {
            m_IsGrounded = false; 
        }
    }

    void AddPV()
    {
        m_pv += 1f;
    }


    void DelPV()
    {
        m_pv -= 1f;
    }

    #endregion

    #region Utils
    //void OnGUI()
    //{
    //    GUILayout.Button("x: " + m_RigidB2D.velocity.x + " \n y:" + m_RigidB2D.velocity.y);
    //}
    #endregion

    #region Private properties

    Vector3 m_CurrentPosition;
    Rigidbody2D m_RigidB2D;
    bool m_FacingRight;
    bool m_IsGrounded;
    bool m_IsGroundedCenter;
    bool m_IsGroundedRight;
    bool m_IsGroundedLeft;
    bool m_CanWalk;

    


    
    float m_FloorDistance;
    float m_MaxFloorDistance;
    #endregion


}
