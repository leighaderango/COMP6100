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
		anim = gameObject.GetComponent<Animator>();
	}


	public void OnDrop(PointerEventData eventData){
		
		if (!craftDict.ContainsKey(eventData.pointerDrag.tag)){
		
			if (eventData.pointerDrag.transform.GetChild(1).GetComponent<TMP_Text>().text != ""){
				craftDict.Add(eventData.pointerDrag.tag.ToString(), int.Parse(eventData.pointerDrag.transform.GetChild(1).GetComponent<TMP_Text>().text));
			} else{
				craftDict.Add(eventData.pointerDrag.tag.ToString(), 1);
			}
		}
		
	}
	
	public void Craft(){
		
		bool isRecipe = true;
		string createdItem = null;
		List<string> keysToRemove = new List<string>(); 
		anim.SetBool("FlameActive", true);
		
		
		// check if each item in craftDict is still in the crafting region
		if (craftDict.Count != 0){
			foreach (KeyValuePair<string, int> crafter in craftDict){
				crafterTag = GameObject.FindWithTag(crafter.Key).transform.position;
				objectCollider = gameObject.GetComponent<BoxCollider2D>();

				if (!objectCollider.OverlapPoint(crafterTag)){
					keysToRemove.Add(crafter.Key);
				}
			}
		}
		
		if (keysToRemove.Count != 0){
			foreach (string key in keysToRemove){
				craftDict.Remove(key);
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
			if (isRecipe == true){
				createdItem = item.Key;
				break;
			}
			
			isRecipe = true;
		}
			
		
				
		if (createdItem != null){
			
			inventory.AddItem(new Item {itemType = Item.GetItemType(createdItem), amount = 1});
			
			foreach (KeyValuePair <string, int> ingredient in recipes[createdItem.ToString()]){
				string usedItem = ingredient.Key;
				int usedAmount = ingredient.Value;
				inventory.RemoveItem(new Item {itemType = Item.GetItemType(usedItem), amount = usedAmount});
			}
			
			ui_inventory.RefreshInventoryItems();
			
		} 
		
		createdItem = null;
		craftDict.Clear();
		keysToRemove.Clear();
		anim.SetBool("FlameActive", false);
		
	}
	
	
}
