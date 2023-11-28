using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player") {
            SceneManager.LoadScene("Level");
            Debug.Log("Entered new room");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
