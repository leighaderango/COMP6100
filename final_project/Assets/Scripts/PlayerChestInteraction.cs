using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class interactionChest : MonoBehaviour
{
    
	private float searchRange = 1;
	private int numItems = 0;
	private Collider2D[] chestColliders;
	
	// connect to player inventory
	public UI_Inventory ui_inventory;
	private Inventory playerInventory;
	
	// list of items able to be added to inventory (only base items, not craftables)
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
	// used for UI animation to show player what was in the chest
	private int randomIndex;
	private string item;
	private Transform inventoryTextContainer;
	private Transform inventoryTemplateTransform;
	

	void Start(){
		playerInventory = ui_inventory.GetInventory();
		
		inventoryTextContainer = GameObject.Find("InventoryTextContainer").GetComponent<RectTransform>();
		inventoryTemplateTransform = inventoryTextContainer.Find("InventoryTextTemplate");
		
	}


    void Update() {
		
		Collider2D[] chestColliders = Physics2D.OverlapCircleAll(transform.position, searchRange);
			//  recognize chests in search range
	
        if(Input.GetButtonDown("Interact") & chestColliders.Length != 0){
			// if open button is pressed and there are chests in the search area
            foreach(Collider2D c in chestColliders){
				if (c.tag == "BlueChest" ||  c.tag == "GoldChest" || c.tag == "RedChest"){
				// check that the collider is a chest 
					if (c.GetComponent<Chest>().isOpen == false){ // if chest is closed, 
						c.GetComponent<Chest>().isOpen = true; // open it
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
						// add items based on chest color
					}
				}
				
			
            }
		
			chestColliders = null;
			// reset list of chestColliders to constantly monitor player's location
        }

    }
	
	
	// adds items from opened chest to player inventory
	private void AddItemsFromChest(int numItems){
		
		// delete previous templates from items added to avoid unnecessary game objects
		foreach(Transform previousItem in inventoryTextContainer){
			if (previousItem == inventoryTemplateTransform) continue;
			Destroy(previousItem.gameObject);
		}
		
		int y = 0;
		float textHeightSize = 40f;
		
		for (int i = 0; i < numItems; i++){ // for each item
			randomIndex = UnityEngine.Random.Range(0, generatableItems.Count - 1);
			item = generatableItems[randomIndex];
			// randomly choose item to add

			playerInventory.AddItem(new Item{itemType = Item.GetItemType(item), amount = 1});
			// add it to inventory
			

			// play text animation for player to see what was added
			RectTransform textRectTransform = Instantiate(inventoryTemplateTransform, inventoryTextContainer).GetComponent<RectTransform>();
				// Instantiate text template in text container
			textRectTransform.gameObject.SetActive(true);
			textRectTransform.anchoredPosition = new Vector2(inventoryTextContainer.position.x, y * textHeightSize);
				// play player visual at appropriate spot (on top of previous item added visual)
			TextMeshProUGUI amountText = textRectTransform.Find("AddInventoryText").GetComponent<TextMeshProUGUI>();
			amountText.SetText("+1 " + item);
			
			y += 1;
		}
		
		
	}


	
	    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, searchRange);
    }

    
}
