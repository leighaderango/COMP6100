using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionChest : MonoBehaviour
{
    
    public int ChestsInRange = 0;
	public float searchRange = 5;
    [SerializeField] private List<GameObject> chestsInArea;
	private Collider2D[] chestColliders;
	
	public UI_Inventory ui_inventory;
	private Inventory playerInventory;
	private int numItems = 0;
	private List<string> generatableItems = new List<string>(){
		"YellowBone", 	
		"GreenGem",
		"BlueGreenGem",
		"PurpleGem",
		"SilverGem", 
		"GoldMetal", 	
		"SilverMetal", 	
		"Fabric", 	
		"SilverPotion", 
		"GreenPotion"};
	private int randomIndex;
	private string item;
	private Transform inventoryTextContainer;
	private Transform inventoryTemplateTransform;
	

	void Start(){
		playerInventory = ui_inventory.GetInventory();
		Debug.Log(playerInventory);
		
		inventoryTextContainer = GameObject.Find("InventoryTextContainer").GetComponent<RectTransform>();
		inventoryTemplateTransform = inventoryTextContainer.Find("InventoryTextTemplate");
		
	}


    void Update() {
		
		Collider2D[] chestColliders = Physics2D.OverlapCircleAll(transform.position, searchRange);
	
        if(Input.GetButtonDown("Interact") & chestColliders.Length != 0) 
        {
            //int chestNum = 0;
			
            foreach(Collider2D c in chestColliders){
				
					if (c.tag == "BlueChest" ||  c.tag == "GoldChest" || c.tag == "RedChest"){
						if (c.GetComponent<Chest>().isOpen == false){
							c.GetComponent<Chest>().isOpen = true;
							if (c.tag == "BlueChest"){
								numItems = 1;
								AddItemsFromChest(numItems);
							}
							if (c.tag == "RedChest"){
								numItems = 2;
								AddItemsFromChest(numItems);
							}
							if (c.tag == "GoldChest"){
								numItems = 3;
								AddItemsFromChest(numItems);
							}
						}
					}
				
			
            }
		
			chestColliders = null;
            ChestsInRange = 0;
        }

    }
	
	private void AddItemsFromChest(int numItems){
		
		int y = 0;
		float textHeightSize = 40f;
		
		for (int i = 0; i < numItems; i++){
			randomIndex = Random.Range(0, generatableItems.Count - 1);
			item = generatableItems[randomIndex];
			Debug.Log(item);

			playerInventory.AddItem(new Item{itemType = Item.GetItemType(item), amount = 1});
			Debug.Log("added " + item);
			

			// Instantiate text template in text container
			RectTransform textRectTransform = Instantiate(inventoryTemplateTransform, inventoryTextContainer).GetComponent<RectTransform>();
			textRectTransform.gameObject.SetActive(true);
			// play player visual
			textRectTransform.anchoredPosition = new Vector2(inventoryTextContainer.position.x, y * textHeightSize);
			
			TextMeshProUGUI amountText = textRectTransform.Find("AddInventoryText").GetComponent<TextMeshProUGUI>();
			amountText.SetText("+1 " + item);
			
			y += 1;
			// Instantiate text object above Health Bar GUI, play animation
		}
		
		
	}


	
	    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, searchRange);
    }

    
}
