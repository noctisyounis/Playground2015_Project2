using UnityEngine;
using System.Collections;
using Assets.Scripts.Character.enums;

public class WeaponBehavior : MonoBehaviour
{
    #region Public Properties 
    

    #endregion



    #region Main methods
    void Start ()
    {
        m_AmmoType = GetComponent<CharacterBehavior>().m_SelectedAmmo;        
    }

    void FixedUpdate()
    {
        // Wave auto-destruction default period (if never colliding or triggering)
        Destroy(gameObject, 1);
    } 
    #endregion


    #region Utils



    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO : Rewrite and put a tag on Character

        if (m_AmmoType == Ammo.DestroyWave)
        {
            // If wave collision with a destructible target, destroy the target and the wave too
            if (other.gameObject.CompareTag("Destructible"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            // If wave collision with a "Ground"-tagged of "Pushable"-tagged gameObject, destroy just the wave
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pushable"))
            {
                Destroy(gameObject);
            }
    }

        if (m_AmmoType == Ammo.PushWave)
        {
            if (other.gameObject.CompareTag("Pushable"))
            {
                Destroy(gameObject);
                // TODO : Push();
                Debug.Log("Pushed");
            }

            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Destructible"))
            {
                Destroy(gameObject);
            }
        }
    }


    #endregion

    #region Private Properties
    Ammo m_AmmoType;
    #endregion


}
