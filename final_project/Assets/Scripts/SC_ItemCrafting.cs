using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ItemCrafting : MonoBehaviour
{
	public RectTransform playerSlotsContainer;
	public RectTransform craftingSlotsContainer;
	public RectTransform resultSlotContainer;
	public Button craftButton;
	public SC_SlotTemplate slotTemplate;
	public SC_SlotTemplate resultSlotTemplate;
	
	[System.Serializable]
	public class SlotContainer{
		
		public Sprite itemSprite; // sprite of the assigned itemSprite
			// must be the same sprite as in itemsArray), or leave null for no item
		public int itemCount; // how many items in this slot 
			// everything equal or under 1 will be interpreted as 1 item
		[HideInInspector]
		public int tableID;
		public SC_SlotTemplate slot;
		
	}

	[System.Serializable]
	public class Item{
		public Sprite itemSprite;
		public bool stackable = false; // can this item be combined together?
		public string craftRecipe; 
			// item keys required to craft this item, separated by comma
	}

	public SlotContainer[] playerSlots;
	SlotContainer[] craftSlots = new SlotContainer[9];
	SlotContainer resultSlot = new SlotContainer();
	public Item[] items; // list of all available items
	
	SlotContainer selectedItemSlot = null;
	
	int craftTableID = -1;
		// ID of where items will be placed one at a time (ex. craft table);
	int resultTableID = -1; 
		// ID of table where we can take items (but not place them);
	
	ColorBlock defaultButtonColors;
	
	void Start(){
		// setup slot element template
		slotTemplate.container.rectTransform.pivot = new Vector2(0, 1);
		slotTemplate.container.rectTransform.anchorMax = slotTemplate.container.rectTransform.anchorMin = new Vector2(0, 1);
		slotTemplate.craftingController = this;
		slotTemplate.gameObject.SetActive(false);
		
		// setup result slot element template
		resultSlotTemplate.container.rectTransform.pivot = new Vector2(0, 1);
		resultSlotTemplate.container.rectTransform.anchorMax = resultSlotTemplate.container.rectTransform.anchorMin = new Vector2(0, 1);
		resultSlotTemplate.craftingController = this;
		resultSlotTemplate.gameObject.SetActive(false);
		
		// attach click event to craft button
		craftButton.onClick.AddListener(PerformCrafting);
		defaultButtonColors = craftButton.colors;
		
		// initialize item crafting slots
		InitializeSlotTable(craftingSlotsContainer, slotTemplate, craftSlots, 5, 0);
		UpdateItems(craftSlots);
		craftTableID = 0;
		
		// initialize item player slots
		InitializeSlotTable(playerSlotsContainer, slotTemplate, playerSlots, 5, 1);
		UpdateItems(playerSlots);
		
		// initialize item result slot
		InitializeSlotTable(resultSlotContainer, resultSlotTemplate, new SlotContainer[] {resultSlot}, 0, 2);
		UpdateItems(new SlotContainer[] {resultSlot});
		resultTableID = 2;
		
		// reset slot element template (used later for hovering element)
		slotTemplate.container.rectTransform.pivot = new Vector2(0.5f, 0.5f);
		slotTemplate.container.raycastTarget = slotTemplate.item.raycastTarget = slotTemplate.count.raycastTarget = false;
	}
	
	void InitializeSlotTable(RectTransform container, SC_SlotTemplate slotTemplateTmp, SlotContainer[] slots, int margin, int tableIDTmp){
		int resetIndex = 0;
		int rowTmp = 0;
		for (int i = 0; i < slots.Length; i++){
			if (slots[i] == null){
				slots[i] = new SlotContainer();
			}
			GameObject newSlot = Instantiate(slotTemplateTmp.gameObject, container.transform);
			slots[i].slot = newSlot.GetComponent<SC_SlotTemplate>();
			slots[i].slot.gameObject.SetActive(true);
			slots[i].tableID = tableIDTmp;
			
			float xTmp = (int)((margin + slots[i].slot.container.rectTransform.sizeDelta.x) * (i - resetIndex));
			if (xTmp + slots[i].slot.container.rectTransform.sizeDelta.x + margin > container.rect.width){
				resetIndex = i;
				rowTmp += 1;
				xTmp = 0;
			}
			
			slots[i].slot.container.rectTransform.anchoredPosition = new Vector2(margin + xTmp, -margin - ((margin + slots[i].slot.container.rectTransform.sizeDelta.y)*rowTmp));
		}
	}
	
	void UpdateItems(SlotContainer[] slots){
		for (int i = 0; i < slots.Length; i++){
			Item slotItem = FindItem(slots[i].itemSprite);
			if (slotItem != null){
				
				if (!slotItem.stackable){
					slots[i].itemCount = 1;
				}
				
				if (slots[i].itemCount > 1){
					slots[i].slot.count.enabled = true;
					slots[i].slot.count.text = slots[i].itemCount.ToString();
				}
				else{
					slots[i].slot.count.enabled = false;
				}
				
				slots[i].slot.item.enabled = true;
				slots[i].slot.item.sprite = slotItem.itemSprite;
			}
			else{
				slots[i].slot.count.enabled = false;
				slots[i].slot.item.enabled = false;
			}
		}
	}
	
	// find item from itemslist using sprite as reference
	Item FindItem(Sprite sprite){
		if (!sprite){
			return null;
		}
		for (int i = 0; i < items.Length; i++){
			if (items[i].itemSprite == sprite){
				return items[i];
			}
		}
		return null;
	}
	
	// find item from itemslist using recipe as reference
	Item FindItem(string recipe){
		if (recipe == ""){
			return null;
		}
		
		for (int i = 0; i < items.Length; i++){
			if (items[i].craftRecipe == recipe){
				return items[i];
			}
		}
		
		return null;
	}
	
	// called from SC_SlotTemplate.cs
	public void ClickEventRecheck(){
		if (selectedItemSlot == null){
			//get clicked slot
			selectedItemSlot = GetClickedSlot();
			if (selectedItemSlot != null){
				if (selectedItemSlot.itemSprite != null){
					selectedItemSlot.slot.count.color = selectedItemSlot.slot.item.color = new Color (1, 1, 1, 0.5f);
				}
				else{
					selectedItemSlot = null;
				}
			}
		} 
		else{
			SlotContainer newClickedSlot = GetClickedSlot();
			if (newClickedSlot != null){
				bool swapPositions = false;
				bool releaseClick = true;
				
				if (newClickedSlot != selectedItemSlot){
					if (newClickedSlot.tableID == selectedItemSlot.tableID){
						// if we clicked on the same table but different slots,
						if (newClickedSlot.itemSprite == selectedItemSlot.itemSprite){
							// check if the new clicked item is the same, then stack, if not, swapPositions
							// unless its a crafting table, then do nothing
							Item slotItem = FindItem(selectedItemSlot.itemSprite);
							if (slotItem.stackable){
								// item is the same and stackable, remove from previous position and add its count to new position
								selectedItemSlot.itemSprite = null;
								newClickedSlot.itemCount += selectedItemSlot.itemCount;
								selectedItemSlot.itemCount = 0;
							}
							else{
								swapPositions = true;
							}
						}
						else{
							swapPositions = true;
						}
					}
					else{
						// moving to different table
						if (resultTableID != newClickedSlot.tableID){
							if (craftTableID != newClickedSlot.tableID){
								if (newClickedSlot.itemSprite == selectedItemSlot.itemSprite){
									Item slotItem = FindItem(selectedItemSlot.itemSprite);
									if (slotItem.stackable){
										selectedItemSlot.itemSprite = null;
										newClickedSlot.itemCount += selectedItemSlot.itemCount;
										selectedItemSlot.itemCount = 0;
									}
									else{
										swapPositions = true;
									}
								}
								else{
									swapPositions = true;
								}
							}
							else{
								if (newClickedSlot.itemSprite == null || newClickedSlot.itemSprite == selectedItemSlot.itemSprite){
									// add one item from selected item slot
									newClickedSlot.itemSprite = selectedItemSlot.itemSprite;
									newClickedSlot.itemCount ++;
									if (selectedItemSlot.itemCount <= 0){
										// we placed the last item
										selectedItemSlot.itemSprite = null;
									}
									else{
										releaseClick = false;
									}
								}
								else{
									swapPositions = true;
								}
							}
						}
					}
				}
				if (swapPositions){
					Sprite previousItemSprite = selectedItemSlot.itemSprite;
					int previousItemCount = selectedItemSlot.itemCount;
					
					selectedItemSlot.itemSprite = newClickedSlot.itemSprite;
					selectedItemSlot.itemCount = newClickedSlot.itemCount;
					
					newClickedSlot.itemSprite = previousItemSprite;
					newClickedSlot.itemCount = previousItemCount;
				}
				
				if (releaseClick){
					selectedItemSlot.slot.count.color = selectedItemSlot.slot.item.color = Color.white;
					selectedItemSlot = null;
				}
				
				// update UI
				UpdateItems(playerSlots);
				UpdateItems(craftSlots);
				UpdateItems(new SlotContainer[] {resultSlot});
			}
		}
	}
	
	// START AT SlotContainer GetClickedSlot()
	SlotContainer GetClickedSlot(){
		for (int i = 0; i < playerSlots.Length; i++){
			if (playerSlots[i].slot.hasClicked){
				playerSlots[i].slot.hasClicked = false;
				return playerSlots[i];
			}
		}
		
		for (int i = 0; i < craftSlots.Length; i++){
			if (craftSlots[i].slot.hasClicked){
				craftSlots[i].slot.hasClicked = false;
				return craftSlots[i];
			}
		}
		
		if (resultSlot.slot.hasClicked){
			resultSlot.slot.hasClicked = false;
			return resultSlot;
		}
		
		return null;
	}
	
	void PerformCrafting(){
		string[] combinedItemRecipe = new string[craftSlots.Length];
		
		craftButton.colors = defaultButtonColors;
		
		for (int i = 0; i < craftSlots.Length; i++){
			Item slotItem = FindItem(craftSlots[i].itemSprite);
			if (slotItem != null){
				combinedItemRecipe[i] = slotItem.itemSprite.name + (craftSlots[i].itemCount > 1 ? "(" + craftSlots[i].itemCount + ")" : "" );
			}
			else {
				combinedItemRecipe[i] = "";
			}
		}
		
		string combinedRecipe = string.Join(",", combinedItemRecipe);
		print(combinedRecipe);
		
		// search if recipe match any of the tiem recipe
		Item craftedItem = FindItem(combinedRecipe);
		if (craftedItem != null){
			// clear craft slots
			for (int i = 0; i < craftSlots.Length; i++){
				craftSlots[i].itemSprite = null;
				craftSlots[i].itemCount = 0;
			}
			resultSlot.itemSprite = craftedItem.itemSprite;
			resultSlot.itemCount = 1;
			
			UpdateItems(craftSlots);
			UpdateItems(new SlotContainer[] {resultSlot});
		}
		else {
			ColorBlock colors = craftButton.colors;
			colors.selectedColor = colors.pressedColor = new Color(0.8f, 0.55f, 0.55f, 1);
			craftButton.colors = colors;
		}
	}
	
	void Update(){
		// slot UI follow mouse position
		if (selectedItemSlot != null){
			if (!slotTemplate.gameObject.activeSelf){
				slotTemplate.gameObject.SetActive(true);
				slotTemplate.container.enabled = false;
				
				//copy selected item values to slot template
				slotTemplate.count.color = selectedItemSlot.slot.count.color;
				slotTemplate.item.sprite = selectedItemSlot.slot.item.sprite;
				slotTemplate.item.color = selectedItemSlot.slot.item.color;
			}
		
			// make template slot follow mouse position
			slotTemplate.container.rectTransform.position = Input.mousePosition;
			//update item count
			slotTemplate.count.text = selectedItemSlot.slot.count.text;
			slotTemplate.count.enabled = selectedItemSlot.slot.count.enabled;
		}
		else {
			if (slotTemplate.gameObject.activeSelf){
				slotTemplate.gameObject.SetActive(false);
			}
		}
	}
}
