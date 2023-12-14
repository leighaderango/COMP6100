using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
	private Inventory inventory;
		// connect inventory to UI
	private Transform itemSlotContainer;
	private Transform itemSlotTemplateTransform;
		// used to show inventory items in the UI
	private string itemName;
	
	
	private void Awake(){
		itemSlotContainer = transform.Find("ItemSlotContainer");
		itemSlotTemplateTransform = itemSlotContainer.Find("ItemSlotTemplate");
	}
	
	
	// connects this script and the UI object to the inventory
	public void SetInventory(Inventory inventory){
		this.inventory = inventory;
		inventory.OnItemListChanged += Inventory_OnItemListChanged;
		RefreshInventoryItems();
		Debug.Log("set inventory run");
	}
	
	// returns player inventory
	public Inventory GetInventory(){
		return inventory;
	}
	
	// recognizes event when inventory list is changed and refreshes inventory UI
	private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
		RefreshInventoryItems();
	}

	public void RefreshInventoryItems(){
		// destroy template copies from previous inventory update
		foreach (Transform child in itemSlotContainer){
			if (child == itemSlotTemplateTransform) continue;
			Destroy(child.gameObject);
		}
		
		int x = 0;
		int y = 0;
		float itemSlotCellSize = 80f;
		
		// cycle through the items in the current inventory
		foreach (Item item in inventory.GetItemList()){
			RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplateTransform, itemSlotContainer).GetComponent<RectTransform>();
				//instantiate item slot template in the container
			itemSlotRectTransform.gameObject.SetActive(true);
				// make it active (originally set as inactive so the template image doesn't show up)
			itemSlotRectTransform.anchoredPosition = new Vector2(x*itemSlotCellSize, y * itemSlotCellSize);
				//position the templates in a grid

			Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
			image.sprite = item.GetSprite();
				// find the sprite for the item type of the current item by calling to Item script
				
			itemSlotRectTransform.gameObject.tag = item.itemType.ToString();
				// add tag to the item slot object to be used for crafting
			
			
			// if amount text is more than 1, show amount
			// if amount text is blank, set text to 0 so that it will get removed from the inventory (useful when crafting)
			TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
			if (item.amount > 1){
				uiText.SetText(item.amount.ToString());
			} else if (item.amount == 1){
				uiText.SetText("");
			} else{
				uiText.SetText("0");
			}
			
			// update variables for item grid alignment
			x += 1;
			if (x > 2){
				x = 0;
				y -= 1;
			}
			
		}
	}
}
