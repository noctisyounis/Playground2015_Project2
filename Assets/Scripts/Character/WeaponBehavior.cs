using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

namespace Assets.Scripts.Character
{
    public class WeaponBehavior : MonoBehaviour
    {
        #region Main methods
        void Start()
        {

        }

        void FixedUpdate()
        {
            // Wave auto-destruction default period (if never colliding or triggering)
            Destroy(gameObject, 1);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Character"))
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Private Properties

        #endregion



    }
}
