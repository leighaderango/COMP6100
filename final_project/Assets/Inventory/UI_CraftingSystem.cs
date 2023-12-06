using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_CraftingSystem : MonoBehaviour{

	[SerializeField] private Transform pfItem;
	
	private Transform[,] slotTransformArray;
	private Transform outputSlotTransform;
	private Transform itemContainer;
	
	private void Awake(){
		
		slotTransformArray = new Transform[CraftingSystem.GRID_SIZE, CraftingSystem.GRID_SIZE];
		Transform gridContainer = transform.Find("GridContainer");
		itemContainer = gridContainer.Find("ItemContainer");
		
		
		for (int x = 0; x < CraftingSystem.GRID_SIZE; x++){
			for (int y = 0; y < CraftingSystem.GRID_SIZE; y++){
				slotTransformArray[x, y] = gridContainer.Find("Grid" + x + y);
				UI_CraftingItemSlot craftingItemSlot = slotTransformArray[x,y].GetComponent<UI_CraftingItemSlot>();
				craftingItemSlot.SetXY(x, y);
				craftingItemSlot.OnItemDropped += UI_CraftingSystem_OnItemDropped;
			}
		}
		
		outputSlotTransform = transform.Find("ResultContainer");
		
		//CreateItem(0,0, new Item {itemType = Item.ItemType.GreenPotion});
		//CreateItemOutput(new Item {itemType = Item.ItemType.Sword_1});
		//CreateItem(0,1, new Item {itemType = Item.ItemType.Fabric});
	}
	
	private void UI_CraftingSystem_OnItemDropped(object sender, UI_CraftingItemSlot.OnItemDroppedEventArgs e){
		Debug.Log(e.item + " " + e.x + " " + e.y);
	}


	public void CreateItem(int x, int y, Item item){
		Transform itemTransform = Instantiate(pfItem, itemContainer, true);
		RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
		itemRectTransform.anchoredPosition = slotTransformArray[x,y].GetComponent<RectTransform>().anchoredPosition;
		itemRectTransform.GetComponent<ItemWorld>().SetItem(item);
	}
	
	private void CreateItemOutput(Item item){
		Transform itemTransform = Instantiate(pfItem, itemContainer, true);
		RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
		itemRectTransform.anchoredPosition = outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
		itemTransform.GetComponent<ItemWorld>().SetItem(item);
	}
}
