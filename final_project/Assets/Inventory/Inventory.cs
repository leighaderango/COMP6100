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
		AddItem(new Item {itemType = Item.ItemType.GreenGem, amount = 2});
		AddItem(new Item {itemType = Item.ItemType.BlueGreenGem, amount = 4});
		AddItem(new Item {itemType = Item.ItemType.PurpleGem, amount = 2});
		AddItem(new Item {itemType = Item.ItemType.SilverGem, amount = 6});
		AddItem(new Item {itemType = Item.ItemType.GoldMetal, amount = 2});
		AddItem(new Item {itemType = Item.ItemType.SilverMetal, amount = 2});
		AddItem(new Item {itemType = Item.ItemType.Fabric, amount = 11});

	}
	
	// adds item to existing inventory
	public void AddItem(Item item){
		if (item.IsStackable()){
			bool itemAlreadyInInventory = false;
			foreach (Item inventoryItem in itemList){
				if (inventoryItem.itemType == item.itemType){
					inventoryItem.amount += item.amount;
					itemAlreadyInInventory = true;
				}
			}
			if (!itemAlreadyInInventory){
				itemList.Add(item);
			}
			
		} else {
			itemList.Add(item);
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty);
	}
	
	public void RemoveItem(Item item){
		if (item.IsStackable()){
			Item itemInInventory = null;
			foreach (Item inventoryItem in itemList){
				if (inventoryItem.itemType == item.itemType){
					inventoryItem.amount -= item.amount;
					itemInInventory = inventoryItem;
				}

			}

			if (itemInInventory.amount == 0){
					itemList.Remove(itemInInventory);
			}
			
		} else {
			itemList.Remove(item);
		}
		OnItemListChanged?.Invoke(this, EventArgs.Empty);
	}
	
	public void UseItem(Item item){
		useItemAction(item);
	}
	
	public List<Item> GetItemList(){
		return itemList;
	}
}
