using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
	private Inventory inventory;
	private Transform itemSlotContainer;
	private Transform itemSlotTemplate;
	
	private void Awake(){
		itemSlotContainer = transform.Find("ItemSlotContainer");
		itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
	}
	
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
		
		foreach (Item item in inventory.GetItemList()){
			// instantiate item slot
			RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
			// set as active
			itemSlotRectTransform.gameObject.SetActive(true);
			// position the slots into a grid
			itemSlotRectTransform.anchoredPosition = new Vector2(x*itemSlotCellSize, y * itemSlotCellSize);
			Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
			image.sprite = item.GetSprite();
			
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
