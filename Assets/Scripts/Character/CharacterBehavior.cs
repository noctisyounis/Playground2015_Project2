using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

public class CharacterBehavior : MonoBehaviour
{

    #region Public properties
    public string m_Axis;
    public float m_MoveSpeed = 10.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_ShootForce = 15.0f;

    public Transform m_GroundCheck;
    public LayerMask m_GroundLayer;

    public Ammo m_SelectedAmmo;
    #endregion



    //      TODO :
    //      Stop more longer walking when a non-stop shooting
    //      Stop walking when jumping (just before), then jump
    //      Stop sneaking when shooting/jumping
    //      Fall gestion
    //      Directionnal flip-bug
    //      Debug jump+shoot with RETURN



    #region Main methods
    void Start ()
    {
        m_RigidB2D = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
        m_CanWalk = true;
        m_SelectedAmmo = Ammo.DestroyWave;
    }



    void FixedUpdate ()
    {  
        m_IsGrounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);

        // Display a visual ray on scene
        Debug.DrawLine(transform.position, m_GroundCheck.position);

        m_CurrentPosition = transform.position;

        Move();
        Jump();
        SwitchAmmo();

        StopCoroutine(Shoot(m_SelectedAmmo));
        StartCoroutine(Shoot(m_SelectedAmmo));
    }


    void SwitchAmmo()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (m_SelectedAmmo == Ammo.DestroyWave)
            {
                m_SelectedAmmo = Ammo.PushWave;
                Debug.Log("Switched to Push Wave");
            }

            else
            {
                m_SelectedAmmo = Ammo.DestroyWave;
                Debug.Log("Switched to Destroy Wave");
            }
        }
    }


    IEnumerator Shoot(Ammo AmmoType)
    {
        // if RETURN is pressed down
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // Disable walk
            m_CanWalk = false;
            // Check the current character direction
            Vector2 ShootDirection = m_FacingRight ? Vector2.right : Vector2.left;
            // Adapt prefab type and intantiate the wave
            string PrefabName = (AmmoType == Ammo.DestroyWave) ? "DestroyWavePrefab" : "PushWavePrefab";
            GameObject WavePrefab = (GameObject)Instantiate(Resources.Load("prefabs/Character/"+PrefabName));
            // Bullet position at the current character position        
            WavePrefab.transform.position = m_CurrentPosition;
            // Apply force and direction to the Wave velocity
            WavePrefab.GetComponent<Rigidbody2D>().velocity = ShootDirection * m_ShootForce;
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

        // Without using velocity
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector2.right * Time.deltaTime);
        //}
        
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


    #endregion

    #region Utils
    void OnGUI()
    {
        GUILayout.Button("x: " + m_RigidB2D.velocity.x + " \n y:" + m_RigidB2D.velocity.y);
    }
    #endregion

    #region Private properties

    Vector3 m_CurrentPosition;
    Rigidbody2D m_RigidB2D;
    bool m_FacingRight;
    bool m_IsGrounded;
    bool m_CanWalk;
    
    #endregion


}
