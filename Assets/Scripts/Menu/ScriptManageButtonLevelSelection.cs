using UnityEngine;
using System.Collections;
using UnityEngine.UI; 


public class ScriptManageButtonLevelSelection : MonoBehaviour {

	public  GameObject[] m_playObjectArray;
	public  GameObject[] m_pauseObjectArray;
	public  GameObject[] m_ButtonNieauArray;
	
	
	
	private GameObject[] controlPlayObject;
	private int sizeArray;
	public int niveauReussi;
	
	
	void Start ()
	{
		niveauReussi = 0;
		sizeArray=m_pauseObjectArray.Length;
		controlPlayObject = new GameObject[sizeArray];
		for (int i = 0; i < sizeArray; i++ )
		{
			m_playObjectArray[i].SetActive(true);
			m_pauseObjectArray[i].SetActive(false);
			controlPlayObject[i] = null;
			//  controlPauseObject[i] = null;
		}
		
		
		InitialiseButtonNiveau ();
		
		
	}
	
	void InitialiseButtonNiveau()
	{
		for (int i = 0; i < sizeArray; i++ )
		{
			m_ButtonNieauArray[i].GetComponent<Button>().interactable=false ;
		}
		
		m_ButtonNieauArray[0].GetComponent<Button>().interactable=true ;
		
	}
	
	
	void EnableLevelButton()
	{
		switch(niveauReussi)
		{
		case 1:	m_ButtonNieauArray [niveauReussi-1].GetComponent<Button> ().interactable = true;
			break;
			
		case 2:	m_ButtonNieauArray [niveauReussi-1].GetComponent<Button> ().interactable = true;
			break;
			
		case 3:	m_ButtonNieauArray [niveauReussi-1].GetComponent<Button> ().interactable = true;
			break;
			
		case 4:	m_ButtonNieauArray [niveauReussi-1].GetComponent<Button> ().interactable = true;
			break;
			
		}
		niveauReussi=0;
		
	}
	void Update()
	{
		if (niveauReussi > 0 && niveauReussi<=sizeArray) 
		{ 
			EnableLevelButton();
			
		}
		
	}
	
	
	void StopCurrentMusic()
	{
		for (int i = 0; i < sizeArray; i++)
		{
			if (controlPlayObject[i] != null)
			{
				m_playObjectArray[i].SetActive(true);
				m_pauseObjectArray[i].SetActive(false);
				controlPlayObject[i] = null;
				
				// fonction arreter la musique
			}
			
			
			
		}
	}
	
	public void ChangePlayToPauseButton(int n)
	{	
		StopCurrentMusic();  
		if ((n > 0)  &&  (n <= sizeArray ))
		{   m_playObjectArray[n-1].SetActive(false);
			m_pauseObjectArray[n-1].SetActive(true);
			controlPlayObject[n - 1] = m_playObjectArray[n - 1];
			// fonction lancer la musique		
		}
		
		
		
	}
	
	
	public void ChangePauseToPlayButton(int n)
	{	
		StopCurrentMusic();  
		if ((n > 0)  &&  (n <= sizeArray ))
		{   m_playObjectArray[n-1].SetActive(true);
			m_pauseObjectArray[n-1].SetActive(false);
			controlPlayObject[n - 1] =null;
			
		}
		
		
	}

	public void OpenLevel(string levelName )
	{
		Application.LoadLevel(levelName);
	}

	
	
}
