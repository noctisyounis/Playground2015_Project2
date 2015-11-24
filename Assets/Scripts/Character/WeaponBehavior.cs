using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour
{
    #region Properties
    public float m_Speed = 3.0f;
    Rigidbody2D m_RigidB2D;
    #endregion


    #region Main methods
    void Start ()
    {
        m_RigidB2D = GetComponent<Rigidbody2D>();
        m_RigidB2D.velocity = Vector2.right * m_Speed;
	}

    void FixedUpdate()
    {
        // Bullet destruction default period (if never colliding)
        Destroy(gameObject, 1);
    } 
    #endregion


    #region Utils

    // TODO  : destroy bullet after few seconds (in case of never collision entering)


    void OnTriggerEnter2D(Collider2D other)
    {
        // If collision on a target, destroy it too
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }   
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Destroy the bullet if entering with "Ground"-tagged gameObject
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    #endregion


}
