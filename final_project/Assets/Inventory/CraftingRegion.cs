using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Threading;

public class CraftingRegion : MonoBehaviour, IDropHandler {

	public UI_Inventory ui_inventory;
	private Inventory inventory;
	private Collider2D objectCollider;
	public static Dictionary<string, int> craftDict = new Dictionary<string, int>();
	private Item pfItemWorld;
	private Vector2 crafterTag;
	private Animator anim;
	private PlayerCombat player;
	
	// nested dictionary of crafting recipes
	private Dictionary<string, Dictionary<string, int>> recipes = new Dictionary<string, Dictionary<string, int>>{
		
		{"Sword_2", new Dictionary<string, int> {{"SilverMetal", 2}, 
													{"YellowBone", 2},
													{"BlueGreenGem", 1}}},
		{"Armor_Purple", new Dictionary<string, int> {{"Fabric", 2},
													{"PurpleGem", 2}}},
		{"Sword_1", new Dictionary<string, int> {{"GoldMetal", 2},
													{"YellowBone", 1}}},
		{"Armor_Green", new Dictionary<string, int> {{"Fabric", 4}, 
													{"GreenGem", 2}}},											
		{"GreenPotion", new Dictionary<string, int> {{"SilverGem", 2},
													{"BlueGreenGem", 2}}},
		{"SilverPotion", new Dictionary<string, int> {{"SilverGem", 3}}} 
													
	};

		
	private void Start(){
		
		inventory = ui_inventory.GetInventory();
			// connect to player inventory
		anim = gameObject.GetComponent<Animator>();
			// access animator for cauldron fire
		player = GameObject.Find("Player").GetComponent<PlayerCombat>();
	}


	public void OnDrop(PointerEventData eventData){
		// when item is dropped in crafting region
		
		if (!craftDict.ContainsKey(eventData.pointerDrag.tag)){
			// if it has not already been added to the dictionary of items in the region
		
			if (eventData.pointerDrag.transform.GetChild(1).GetComponent<TMP_Text>().text != ""){
				craftDict.Add(eventData.pointerDrag.tag.ToString(), int.Parse(eventData.pointerDrag.transform.GetChild(1).GetComponent<TMP_Text>().text));
			} else{
				craftDict.Add(eventData.pointerDrag.tag.ToString(), 1);
			}
			// add it as a key, with the amount as the value
		}
		
	}
	
	public void Craft(){ // executes when craft button is pushed
		
		bool isRecipe = true; // if items in crafting region make up a recipe
		string createdItem = null; // item being created
		List<string> keysToRemove = new List<string>(); // list of items removed from crafting region
		anim.SetBool("FlameActive", true); // animation
		
		
		// check if each item in craftDict is still in the crafting region
		if (craftDict.Count != 0){
			foreach (KeyValuePair<string, int> crafter in craftDict){
				crafterTag = GameObject.FindWithTag(crafter.Key).transform.position;
				objectCollider = gameObject.GetComponent<BoxCollider2D>();

				if (!objectCollider.OverlapPoint(crafterTag)){
					keysToRemove.Add(crafter.Key); // if not, add it to list of items to be removed
				}
			}
		}
		
		if (keysToRemove.Count != 0){ // if any items have been removed from the crafting region,
			foreach (string key in keysToRemove){
				craftDict.Remove(key); // remove them from consideration for recipes
			}
		}
		
		
		
		//iterate through recipes to check if items in crafting region make anything
		foreach (KeyValuePair <string, Dictionary<string, int>> item in recipes){ 
		// for each recipe 
			foreach (KeyValuePair <string, int> ingredient in item.Value){
				// for each ingredient in the recipe
				if (isRecipe == true){ // if the last ingredient was in the crafting region
					if (craftDict.ContainsKey(ingredient.Key.ToString())){
					// if the current ingredient is in the crafting region
						if (ingredient.Value <= craftDict[ingredient.Key]){
						// and has enough amount
							continue;
							//check next ingredient in recipe
						} else {
							isRecipe = false;
						}
					} else {
						isRecipe = false;
					}
					
				} else{ // if any ingredient is not in the crafting region,  move on to next recipe
					isRecipe = false;
				}
				
			}
			if (isRecipe == true){ // if the items in the crafting region match a recipe
				createdItem = item.Key; // set the item to be created and stop checking recipes
				break;
			}
			
			isRecipe = true;
		}
			
		
				
		if (createdItem != null){ // if the items in the crafing region form a recipe

			
			inventory.AddItem(new Item {itemType = Item.GetItemType(createdItem), amount = 1}); 
				// add the created item to the inventory
			
				// and remove all of its ingredients from the inventory amounts
			foreach (KeyValuePair <string, int> ingredient in recipes[createdItem.ToString()]){
				string usedItem = ingredient.Key;
				int usedAmount = ingredient.Value;
				inventory.RemoveItem(new Item {itemType = Item.GetItemType(usedItem), amount = usedAmount});
			}
			
			ui_inventory.RefreshInventoryItems();
			
			if (createdItem == "Sword_1"){
				player.attackDamage = 25;
			}else if (createdItem == "Sword_2"){
				player.attackDamage = 30;
			}else if (createdItem == "Armor_Green"){
				player.maxHealth = 125;
				player.currentHealth = 125;
			}else if (createdItem == "Armor_Purple"){
				player.maxHealth = 150;
				player.currentHealth = 150;
			}else if (createdItem == "SilverPotion"){
				if (player.currentHealth <= 80){
					player.currentHealth += 20;	
				} else{
					player.currentHealth = 100;
				}
			}else if (createdItem == "GreenPotion"){
				if (player.currentHealth <= 70){
					player.currentHealth += 30;
				} else {
					player.currentHealth = 100;
				}			
			} 
			 		
			
		} 
		
		// reset objects used for monitoring
		createdItem = null;
		craftDict.Clear();
		keysToRemove.Clear();
		anim.SetBool("FlameActive", false);
		
	}
	
	
}
