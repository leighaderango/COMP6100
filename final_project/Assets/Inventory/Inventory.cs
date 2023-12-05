using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory{

	private List<Item> itemList;
	public event EventHandler OnItemListChanged;
	
	public Inventory(){
		itemList = new List<Item>();

		
		AddItem(new Item {itemType = Item.ItemType.YellowBone, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.GreenGem, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.BlueGreenGem, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.PurpleGem, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.SilverGem, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.GoldMetal, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.SilverMetal, amount = 1});
		AddItem(new Item {itemType = Item.ItemType.Fabric, amount = 1});

	}
	
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
	
	public List<Item> GetItemList(){
		return itemList;
	}
}
