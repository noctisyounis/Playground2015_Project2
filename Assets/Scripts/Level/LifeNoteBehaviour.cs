using UnityEngine;
using System.Collections;

public class LifeNoteBehaviour : MonoBehaviour {


    #region Public properties
    #endregion

    #region Main methods
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_character = m_player.GetComponent<CharacterBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            // Life note disappear
            Destroy(gameObject);

            // Player wins 1PV
            m_character.AddPV(1);
        }
    }

    
    #endregion

    #region Private properties
    GameObject m_player;
    CharacterBehaviour m_character;
    #endregion
}
