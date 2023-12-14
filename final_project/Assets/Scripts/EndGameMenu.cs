using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
	public Canvas endGameCanvas;
	
	public void onPlayAgainButton(){
		SceneManager.LoadScene("TheDungeon");
	}
	
	public void onMainMenuButton(){
		SceneManager.LoadScene("HomeScreen");
	}
}
