using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Canvas creditCanvas;
	
	void Start(){
		//creditCanvas = .Find("CreditCanvas");
	}
	
	public void OnPlayButton(){
		SceneManager.LoadScene(1);
	}

	public void OnQuitButton(){
		Application.Quit();
	}
	
	public void showCredit(){
		creditCanvas.GetComponent<Canvas>().enabled = true;
	}
	
	public void hideCredit(){
		creditCanvas.GetComponent<Canvas>().enabled = false;
	}
	
}
