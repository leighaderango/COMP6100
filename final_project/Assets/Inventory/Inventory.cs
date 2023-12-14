using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory{

	private List<Item> itemList;
	public event EventHandler OnItemListChanged;
	private Action<Item> useItemAction;
	
	public Inventory(Action<Item> useItemAction){
		this.useItemAction = useItemAction;
		
		// initialize empty list of Items
		itemList = new List<Item>();

		// gives player a base inventory (DELETE LATER)
		AddItem(new Item {itemType = Item.ItemType.YellowBone, amount = 3});
		//AddItem(new Item {itemType = Item.ItemType.GreenGem, amount = 2});
		//AddItem(new Item {itemType = Item.ItemType.BlueGreenGem, amount = 4});
		//AddItem(new Item {itemType = Item.ItemType.PurpleGem, amount = 2});
		//AddItem(new Item {itemType = Item.ItemType.SilverGem, amount = 6});
		AddItem(new Item {itemType = Item.ItemType.GoldMetal, amount = 2});
		//AddItem(new Item {itemType = Item.ItemType.SilverMetal, amount = 2});
		//AddItem(new Item {itemType = Item.ItemType.Fabric, amount = 11});

	}
	
	// adds item to existing inventory
	public void AddItem(Item item){
		if (item.IsStackable()){ 
			bool itemAlreadyInInventory = false;
			foreach (Item inventoryItem in itemList){
				if (inventoryItem.itemType == item.itemType){ // match item to be added to an inventory item
					inventoryItem.amount += item.amount; // increase amount
					itemAlreadyInInventory = true; 
				}
			}
			if (!itemAlreadyInInventory){
				// if item did not match to an inventory item (not posessed yet)
				itemList.Add(item); 
					// add the item to the list of items in inventory
			}
			
		} else {
			itemList.Add(item); // if item can't be stacked, just add another element to itemList
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty); // if item list has been changed, reset inventory
	}
	
	// same logic as adding item to inventory, but reversed
	public void RemoveItem(Item item){ 
		if (item.IsStackable()){
			Item itemInInventory = null;
			foreach (Item inventoryItem in itemList){
				if (inventoryItem.itemType == item.itemType){
					inventoryItem.amount -= item.amount;
					itemInInventory = inventoryItem;
				}

			}

			if (itemInInventory.amount == 0){ // if there are no items of this kind left, 
					itemList.Remove(itemInInventory); // remove the item from the inventory completely
			}
			
		} else {
			itemList.Remove(item);
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty);
	}
	
	// not utilized, but could be used if items had ability to be used in the game world
	public void UseItem(Item item){
		useItemAction(item);
	}
	
	// public function to access items in the inventory
	public List<Item> GetItemList(){
		return itemList;
	}
}
