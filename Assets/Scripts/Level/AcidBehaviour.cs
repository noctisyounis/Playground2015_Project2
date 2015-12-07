using UnityEngine;
using System.Collections;

public class AcidBehaviour : MonoBehaviour
{

    #region Public properties
    #endregion

    #region Main methods
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_character = m_player.GetComponent<CharacterBehaviour>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Player loose 1PV
            m_character.AddPV(-1);
        }
    }
    #endregion

    #region Private properties
    GameObject m_player;
    CharacterBehaviour m_character;
    #endregion

}
