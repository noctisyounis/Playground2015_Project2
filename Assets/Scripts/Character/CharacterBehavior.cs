using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

    #region Public properties
    public string m_Axis;
    public float m_MoveSpeed = 10.0f;
    public float m_JumpSpeed = 10.0f;
    #endregion


    #region Main methods
    void Start ()
    {
        m_RigidB2D = GetComponent<Rigidbody2D>();
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
    // TODO : input DIRECTION ! (= player direction; left or right)
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {        
            GameObject BulletPrefab = (GameObject)Instantiate(Resources.Load("prefabs/Character/PrefabBullet"));
            BulletPrefab.transform.position = m_CurrentPosition;
        }
    }

    void Move()
    {
        float Axis = Input.GetAxisRaw(m_Axis);
        m_RigidB2D.velocity = new Vector2(Axis, 0) * m_MoveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_RigidB2D.velocity.y == 0)
        {
            m_RigidB2D.velocity = (Vector2.up * m_JumpSpeed);
        }
    }

    void OnGUI()
    {
        GUILayout.Button("x: " + m_RigidB2D.velocity.x + " \n y:" + m_RigidB2D.velocity.y);
    }
    #endregion



    #region Private properties

    Vector3 m_CurrentPosition;
    Rigidbody2D m_RigidB2D;

    #endregion


}
