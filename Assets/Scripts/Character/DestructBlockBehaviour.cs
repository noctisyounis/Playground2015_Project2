using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

namespace Assets.Scripts.Character
{
    class DestructBlockBehaviour : MonoBehaviour
    {
        void Start()
        {

        }

        void FixedUpdate()
        {
            
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("DestroyWave"))
            {
                Destroy(gameObject);
            }

            else
            {
                // TODO : Shake();
                Debug.Log("Shaked");
            }
        }

        #region Private properties

        #endregion


    }
}
