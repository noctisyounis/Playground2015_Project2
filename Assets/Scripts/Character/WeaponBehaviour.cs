using UnityEngine;
using System.Collections;


    public class WeaponBehaviour : MonoBehaviour
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
            if (!other.CompareTag("Player"))
            {
                // TODO : Fade Out
                Destroy(gameObject);
            }
        }

        #endregion

        #region Private Properties

        #endregion



    }

