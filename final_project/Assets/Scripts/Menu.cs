using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Canvas creditCanvas;
	public Canvas bookCanvas;
	
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
	
	public void showBook(){
		bookCanvas.GetComponent<Canvas>().enabled = true;
	}
	
	public void hideBook(){
		bookCanvas.GetComponent<Canvas>().enabled = false;
	}
	
}
