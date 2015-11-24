using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

    #region Public properties
    public string m_Axis;
    public float m_MoveSpeed = 10.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_ShootForce = 15.0f;
    #endregion




    #region Main methods
    void Start ()
    {
        m_RigidB2D = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
	}

    void Update()
    {

    }

	void FixedUpdate ()
    {
        m_CurrentPosition = transform.position;

        Shoot();
        Move();
        Jump();
        
    }
    #endregion


    #region Utils
    void Shoot()
    {
        // if RETURN is pressed down
        if (Input.GetKeyDown(KeyCode.Return))
        { 
            // Check the current character direction
            Vector2 ShootDirection = m_FacingRight ? Vector2.right : Vector2.left;
            // Instantiate Destroy Wave prefab
            GameObject DestroyWave = (GameObject)Instantiate(Resources.Load("prefabs/Character/DestroyWavePrefab"));
            // Bullet position at the current character position        
            DestroyWave.transform.position = m_CurrentPosition;
            // Apply force and direction to the Wave velocity
            DestroyWave.GetComponent<Rigidbody2D>().velocity = ShootDirection * m_ShootForce;
        }
    }

    void Move()
    {
        float Axis = Input.GetAxisRaw(m_Axis);
        m_RigidB2D.velocity = new Vector2(Axis, 0) * m_MoveSpeed;

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
        if (Input.GetKeyDown(KeyCode.Space) && m_RigidB2D.velocity.y == 0)
        {
            m_RigidB2D.velocity = (Vector2.up * m_JumpSpeed);
        }
    }


    private void Flip()
    {
        //m_FacingRight = !m_FacingRight;

        Vector3 CharacterScale = transform.localScale;
        CharacterScale.x *= -1;
        transform.localScale = CharacterScale;
    }


    void OnGUI()
    {
        GUILayout.Button("x: " + m_RigidB2D.velocity.x + " \n y:" + m_RigidB2D.velocity.y);
    }
    #endregion



    #region Private properties

    Vector3 m_CurrentPosition;
    Rigidbody2D m_RigidB2D;
    bool m_FacingRight;
    #endregion


}
