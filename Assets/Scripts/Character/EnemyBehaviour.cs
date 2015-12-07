using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {


    #region Public properties
    
    #endregion

    #region Main methods
    void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_character = m_player.GetComponent<CharacterBehaviour>();
        m_characterPosition = m_character.transform.position;
    }	

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player loose 1PV
            m_character.AddPV(-1);

            Vector2 centerColl = collision.collider.bounds.center;

            float CollPosX = collision.transform.position.x;
            float CollPosY = collision.transform.position.y;

            ContactPoint2D[] Contacts = collision.contacts;
            foreach (ContactPoint2D cp in Contacts)
            {
                Debug.Log("Contact point : "+cp.point.x);
                Debug.Log("Character position on X : "+ m_characterPosition.x);
                Debug.Log("Magnitude :" + cp.point.magnitude);

                if (cp.point.x > m_characterPosition.x)
                {
                    // Collision is on the right side
                    m_characterPosition = new Vector2(CollPosX + 1f, CollPosY);
                }

                else
                {
                    // Collision is on the left side
                    m_characterPosition = new Vector2(CollPosX - 1f, CollPosY);
                }

                if (cp.point.y > centerColl.y)
                {
                    // Collision is on the top side
                    m_characterPosition = new Vector2(CollPosX, CollPosY + 1f);
                }

                else
                {
                    // Collision is on the bottom side
                    m_characterPosition = new Vector2(CollPosX, CollPosY - 1f);
                }

            }
        }
    }


    #endregion

    #region Private properties
    GameObject m_player;
    CharacterBehaviour m_character;
    Vector2 m_characterPosition;
    #endregion
}
