using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
	private GameObject player;
	private Canvas controlsCanvas;
	
	void Start(){
		
		player = GameObject.Find("Player");
		controlsCanvas = GameObject.Find("CraftingMenuCanvas").GetComponent<Canvas>();
		
	}
	
	public void OnContinueButton(){
		controlsCanvas.GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
	}
}
