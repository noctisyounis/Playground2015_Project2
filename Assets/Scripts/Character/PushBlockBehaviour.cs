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
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PushWave"))
            {
                // transform.Translate(1.0f,0,0);
                m_rgbd2d.velocity = Vector2.right *  5 ;
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
