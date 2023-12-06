using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem {

	private Item[,] itemArray;
	public const int GRID_SIZE = 3;

	public CraftingSystem(){
		itemArray = new Item[GRID_SIZE, GRID_SIZE];
	}
	
	private bool IsEmpty(int x, int y){
		return itemArray[x, y] == null;
	}
	
	public Item GetItem(int x, int y){
		return itemArray[x, y];
	}
	
	public void SetItem(Item item, int x, int y){
		itemArray[x, y] = item;
	}
	
	public void IncreaseItemAmount(int x, int y){
		GetItem(x, y).amount++;
	}
	
	public void DecreaseItemAmount(int x, int y){
		GetItem(x, y).amount--;
	}
	
	private void RemoveItem(int x, int y){
		SetItem(null, x, y);
	}
	
	private bool TryAddItem(Item item, int x, int y){
		if (IsEmpty(x, y)){
			SetItem(item, x, y);
			return true;
		} else{
			if (item.itemType == GetItem(x, y).itemType){
				IncreaseItemAmount(x, y);
				return true;
			} else{
				return false;
			}
		}
	}
}
