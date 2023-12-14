using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpen;
    public int numOfItemsToSpawn;
    public Animator anim;

    List<Item> items;

    List<string> chestContains = new List<string>();
    List<string> item_name = new List<string>(){
        "YellowBone",
        "GreenGem",
        "BlueGreenGem",
        "PurpleGem",
        "SilverGem",
        "GoldMetal",
        "SilverMetal",
        "Fabric"};

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
            //Debug.Log("Chest opened");
            // Open chest animation.
            anim.SetBool("isOpen", true);
            // Show UI with Items added to inventory.
            // Inventory.AddItem(item)
            // Turn off script so Player cannot interact with it.
            gameObject.GetComponent<Chest>().enabled = false;
        }
    }

    private void fillChest()
    {
        int rnd = Random.Range(0, item_name.Count);
        for(int i = 0; i < numOfItemsToSpawn; i++) {
            chestContains.Add(item_name[rnd]);
        }
    }

    private void dumpItemsInventory()
    {

    }

}
