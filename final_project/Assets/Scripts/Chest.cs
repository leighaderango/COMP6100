using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen;
    public int numOfItemsToSpawn;
    public Animator anim;

    List<Item> items;
    // Update is called once per frame.
    void Start()
    {
        isOpen = false;
        numOfItemsToSpawn = Random.Range(1,8);
        anim = gameObject.GetComponent<Animator>();
        //fillChest(); 
    }

    // Check if player has opened chest.
    void Update()
    {
        if(isOpen == true){
            Debug.Log("Chest opened");
            // Open chest animation.
            anim.SetBool("isOpen", true);
            // Show UI with Items added to inventory.

            // Turn off script so Player cannot interact with it.
            gameObject.GetComponent<Chest>().enabled = false;
        }
    }

    private void fillChest()
    {
        for(int i = 0; i < numOfItemsToSpawn; i++) {
            
        }
        
    }

    // private void addForceToItems() 
    // {   
    //     foreach(GameObject item in item_prefabs)
    //     {
    //         GameObject obj = (GameObject)Instantiate(item, transform.position, Quaternion.identity);
    //         float x = Random.Range(0.0f, 1.0f);
    //         float y = Random.Range(0.0f, 1.0f);
    //         obj.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    //     }
    // }
}
