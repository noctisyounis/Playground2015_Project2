using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

namespace Assets.Scripts.Character
{

    class PushBlockBehaviour : MonoBehaviour
    {
        #region Main methods

        void Start()
        {
            m_rgbd2d = GetComponent<Rigidbody2D>();
            m_rgbd2d.isKinematic = true;      
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PushWave"))
            {
                m_rgbd2d.isKinematic = false;

                float OtherVeloX = other.GetComponent<Rigidbody2D>().velocity.x;

                if (OtherVeloX > 0)
                {
                    Vector2 Force = Vector2.right * 5;
                    m_rgbd2d.AddForce(Force,ForceMode2D.Impulse);
                    // m_rgbd2d.velocity = Vector2.right * 3;
                }

                if (OtherVeloX < 0)
                {
                    m_rgbd2d.velocity = Vector2.left * 3;
                }
            }

            else
            {
                // Shake();
                Debug.Log("Shaked");
            }

        }




        #endregion


        #region Private properties
        Rigidbody2D m_rgbd2d;
        #endregion

    }
}
