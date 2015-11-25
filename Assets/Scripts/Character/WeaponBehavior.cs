using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour
{
    #region Public Properties    
    
    #endregion


    #region Main methods
    void Start ()
    {

           
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

        // Destroy the bullet if entering with "Ground"-tagged gameObject
        if (other.gameObject.CompareTag("Ground"))
        {
            // Rewrite this = no collision with the character itself !
            Destroy(gameObject);
        }
    }


    #endregion

    #region Private Properties
    #endregion


}
