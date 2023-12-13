using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionChest : MonoBehaviour
{
    
    public int ChestsInRange = 0;
	public float searchRange = 5;
    [SerializeField] private List<GameObject> chestsInArea;
	private Collider2D[] chestColliders;
	
	public UI_Inventory ui_inventory;
	private Inventory playerInventory;
	private int numItems = 0;
	private List<string> generatableItems = new List<string>(){
		"YellowBone", 	
		"GreenGem",
		"BlueGreenGem",
		"PurpleGem",
		"SilverGem", 
		"GoldMetal", 	
		"SilverMetal", 	
		"Fabric", 	
		"SilverPotion", 
		"GreenPotion"};
	private int randomIndex;
	private string item;
	

	void Start(){
		playerInventory = ui_inventory.GetInventory();
		Debug.Log(playerInventory);
	}


    void Update() {
		
		Collider2D[] chestColliders = Physics2D.OverlapCircleAll(transform.position, searchRange);
	
        if(Input.GetButtonDown("Interact") & chestColliders.Length != 0) 
        {
            
            foreach(Collider2D c in chestColliders)
            {
			if (c.tag == "BlueChest" ||  c.tag == "GoldChest" || c.tag == "RedChest"){
					c.GetComponent<Chest>().isOpen = true;
					if (c.tag == "BlueChest"){
						numItems = 1;
						AddItemsFromChest(numItems);
					}
					if (c.tag == "RedChest"){
						numItems = 2;
						AddItemsFromChest(numItems);
					}
					if (c.tag == "GoldChest"){
						numItems = 3;
						AddItemsFromChest(numItems);
					}
				}
            }
		
			chestColliders = null;
            ChestsInRange = 0;
        }

    }
	
	private void AddItemsFromChest(int numItems){
		for (int i = 0; i <= numItems; i++){
			randomIndex = Random.Range(0, generatableItems.Count - 1);
			item = generatableItems[randomIndex];
			Debug.Log(item);

			playerInventory.AddItem(new Item{itemType = Item.GetItemType(item), amount = 1});
			Debug.Log("added " + item);
			
		
			// add to inventory
			// play player visual
		}
	}

    // private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Entered = " + other.name);

        // if(other.name == "chest_red" || other.name == "chest_blue" || other.name == "chest_gold") {
            // ChestsInRange++;
            // chestsInArea.Add(other.gameObject);
        // }
    // }

    // private void OnTriggerExit2D(Collider2D other) {
        // Debug.Log("Exited = " + other.name);

         // if(other.name == "chest_red" || other.name == "chest_blue" || other.name == "chest_gold") {
            // ChestsInRange--;
            // chestsInArea.Remove(other.gameObject);
        // }
    // }
	
	    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, searchRange);
    }

    
}
