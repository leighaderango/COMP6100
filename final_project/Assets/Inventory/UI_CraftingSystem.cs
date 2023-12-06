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
		itemContainer = transform.Find("ItemContainer");
		Transform gridContainer = transform.Find("GridContainer");
		
		for (int x = 0; x < CraftingSystem.GRID_SIZE; x++){
			for (int y = 0; y < CraftingSystem.GRID_SIZE; y++){
				slotTransformArray[x, y] = gridContainer.Find("Grid" + x + y);
			}
		}
		
		outputSlotTransform = transform.Find("ResultContainer");
		
	CreateItem(0,0, new Item {itemType = Item.ItemType.GreenPotion});
	CreateItemOutput(new Item {itemType = Item.ItemType.Sword_1});
		//CreateItem(0,1, new Item {itemType = Item.ItemType.Fabric});
	}


	private void CreateItem(int x, int y, Item item){
		Transform itemTransform = Instantiate(pfItem, itemContainer);
		RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
		itemRectTransform.anchoredPosition = slotTransformArray[x,y].GetComponent<RectTransform>().anchoredPosition;
		itemRectTransform.GetComponent<ItemWorld>().SetItem(item);
	}
	
	private void CreateItemOutput(Item item){
		Transform itemTransform = Instantiate(pfItem, itemContainer);
		RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
		itemRectTransform.anchoredPosition = outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
		itemTransform.GetComponent<ItemWorld>().SetItem(item);
	}
}
