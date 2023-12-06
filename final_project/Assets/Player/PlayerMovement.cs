using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 1.0f;
	private Inventory inventory;
	[SerializeField] private UI_Inventory uiInventory;
	

    void Start()
    {
        inventory = new Inventory(UseItem);
		uiInventory.SetInventory(inventory);
		
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2 (h*speed, v*speed);
    }
	

	private void OnTriggerEnter2D(Collider2D collider){
		ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
		if (itemWorld != null){
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
		}
	}
	
	private void UseItem(Item item){
		switch (item.itemType){
		case Item.ItemType.SilverPotion:
			//FlashBlue();
			inventory.RemoveItem(new Item {itemType = Item.ItemType.SilverPotion, amount = 1});
			break;
		case Item.ItemType.GreenPotion:
			//FlashGreen();
			inventory.RemoveItem(new Item {itemType = Item.ItemType.GreenPotion, amount = 1});
			break;
		}
	}
}
