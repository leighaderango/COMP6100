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
	private Transform itemSlotTemplate;
		// used to show inventory items in the UI
	
	private void Awake(){
		itemSlotContainer = transform.Find("ItemSlotContainer");
		itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
	}
	
	// connects this script and the UI object to the inventory
	public void SetInventory(Inventory inventory){
		this.inventory = inventory;
		inventory.OnItemListChanged += Inventory_OnItemListChanged;
		RefreshInventoryItems();
	}
	
	private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
		RefreshInventoryItems();
	}

	private void RefreshInventoryItems(){
		foreach (Transform child in itemSlotContainer){
			if (child == itemSlotTemplate) continue;
			Destroy(child.gameObject);
		}
		
		int x = 0;
		int y = 0;
		float itemSlotCellSize = 60f;
		
		// cycles through the items in the current inventory
		foreach (Item item in inventory.GetItemList()){
			RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
				//instantiates item slot template in the container
			itemSlotRectTransform.gameObject.SetActive(true);
				// makes it active (originally set as inactive so the template image doesn't show up)
			itemSlotRectTransform.anchoredPosition = new Vector2(x*itemSlotCellSize, y * itemSlotCellSize);
				//positions the templates in a grid
			Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
			image.sprite = item.GetSprite();
				// finds the sprite for the item type of the current item by calling to Item script
			
			TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
			if (item.amount > 1){
				uiText.SetText(item.amount.ToString());
			} else{
				uiText.SetText("");
			}
			
			x += 1;
			if (x > 2){
				x = 0;
				y -= 1;
			}
			
		}
	}
}
