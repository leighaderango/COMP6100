using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DisplayControlsCollider : MonoBehaviour
{
	private GameObject player;
	private Canvas controlsCanvas;
	
	void Start(){
		
		player = GameObject.Find("Player");
		controlsCanvas = GameObject.Find("CraftingMenuCanvas").GetComponent<Canvas>();
		
	}
	
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {

		controlsCanvas.GetComponent<Canvas>().enabled = true;
		Time.timeScale = 0;
		
        // Set controls canvas to true.
        //Debug.Log("Show control canvas");
    }

}