using UnityEngine;
using System.Collections;

public class AudioClipController : MonoBehaviour {


	void Start () {
        m_SpeakerSong = GetComponent<AudioSource>();
        m_SpeakerSong.volume = 0f;
	}
	

	void FixedUpdate () {

 
	}


    void OnMouseDown()
    {
        m_SpeakerSong.volume += 0.1f;
    }


    #region Private properties

    AudioSource m_SpeakerSong;

    #endregion
}
