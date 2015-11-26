using UnityEngine;
using System.Collections;
using UnityEngine.UI; // required when using UI elements in scripts

public class ManagerScript : MonoBehaviour 
{
	//
	//private GameObject objet;
	public GameObject[] m_popupPauseArray; 	// les composants de la fenetre popUp Pause
	public GameObject[] m_popupFailArray;	// les composants de la fenetre popUp en cas d'echeque
	public GameObject[] m_popupFinishArray; // les composants de la fenetre popUp "Niveau Terminé"	


	//private int n = 0;
	private bool msgPopUp=false;  // cette variable permet de bloquer l'évolution du code quand une fenetre popUP est active
	private enum TYPEPOPUP	{NOPOPUP,PAUSE,FAIL,BAD,MEDIUM,GOOD,MINT};   
	private TYPEPOPUP typeFramePopUp;  


	void Start () 
	{
		DisablePopUpFrame ();		
		typeFramePopUp = TYPEPOPUP.NOPOPUP;
		msgPopUp=false;
			
	}
	
	void Update()
	{ 
		if (msgPopUp == false) {
			if (Input.GetKeyDown ("space")) {	
				typeFramePopUp = TYPEPOPUP.PAUSE;
				

			} else if (Input.GetKeyDown ("f")) {	
				typeFramePopUp = TYPEPOPUP.FAIL;
				
			} else if (Input.GetKeyDown ("b")) {
				typeFramePopUp = TYPEPOPUP.BAD;
				
			} else if (Input.GetKeyDown ("m")) {
				typeFramePopUp = TYPEPOPUP.MEDIUM;
				
			} else if (Input.GetKeyDown ("g")) {
				typeFramePopUp = TYPEPOPUP.GOOD;
				
			} else if (Input.GetKeyDown ("e")) {
				typeFramePopUp = TYPEPOPUP.MINT;
				
			}
							


		switch (typeFramePopUp) 
		{ 
				
			case TYPEPOPUP.PAUSE:
				foreach (GameObject framePopUp in m_popupPauseArray) 
				{
					if (framePopUp.name!="ButtonRejouerFinishMint")   
					{	framePopUp.SetActive (true);
						

					}

				}

				msgPopUp = true;
				break;

			case TYPEPOPUP.FAIL:
				foreach (GameObject framePopUp in m_popupFailArray) 
				{	
					framePopUp.SetActive (true);
				}
				msgPopUp = true;
				break;

			case TYPEPOPUP.BAD:
				foreach (GameObject framePopUp in m_popupFinishArray) 
				{	
					if (framePopUp.name!="ButtonRejouerFinishMint")
					{	framePopUp.SetActive (true);
						
						
					}

					if (framePopUp.name=="ButtonNiveauSuivant")
					{	
						framePopUp.GetComponent<Button>().interactable=false; 

					}
				}
				msgPopUp = true;
				break;
			case TYPEPOPUP.MEDIUM:
				foreach (GameObject framePopUp in m_popupFinishArray) 
				{	
					if (framePopUp.name!="ButtonRejouerFinishMint")
					{	framePopUp.SetActive (true);
											
					}

				}
				msgPopUp = true;
				break;
			case TYPEPOPUP.GOOD:
				foreach (GameObject framePopUp in m_popupFinishArray) 
				{	
					if (framePopUp.name!="ButtonRejouerFinishMint")
					{	framePopUp.SetActive (true);
						
					}

				}
				msgPopUp = true;
				break;

			case TYPEPOPUP.MINT:
				foreach (GameObject framePopUp in m_popupFinishArray) {	

					if (framePopUp.name!="ButtonCompleter")
					{	framePopUp.SetActive (true);

						//framePopUp.GetComponent<Text>().text="Rejouer";
					}

				}
				msgPopUp = true;
				break;
		
			default:
				typeFramePopUp = TYPEPOPUP.NOPOPUP;
				break;

			}
			typeFramePopUp = TYPEPOPUP.NOPOPUP;

			// the rest of the program has to be rounded by if msgPopUp different to true. This to avoid the background to run
		}

		if (msgPopUp == false) 
		{
			Debug.Log (" Ici le reste du code Update");
		}

	}


	public void DisablePopUpFrame()
	{	// cette fonction desactive toute les fenetre popUP actives
	
		foreach (GameObject framePopUp in m_popupPauseArray) 
		{	
			framePopUp.SetActive(false);
		}


		foreach (GameObject framePopUp in m_popupFailArray) {	
			framePopUp.SetActive (false);
		}

		foreach (GameObject framePopUp in m_popupFinishArray) 
		{	

			if (framePopUp.name=="ButtonNiveauSuivant")
			{	
				framePopUp.GetComponent<Button>().interactable=true; 
				
			}
			framePopUp.SetActive(false);


		}


		msgPopUp = false;

	}
	void affiche()
	{   
	//	Instantiate (m_imageFond, new Vector3 (0, 1, 0), Quaternion.identity);
	//	transform.parent.gameObject.AddComponent<PopUp> ();
	}



}
