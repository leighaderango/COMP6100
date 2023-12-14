using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCollider : MonoBehaviour
{
	public Canvas winGameCanvas;
	
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
		winGameCanvas.GetComponent<Canvas>().enabled = true;
        Debug.Log("Game Over");
		Time.timeScale = 0;
    }
}
