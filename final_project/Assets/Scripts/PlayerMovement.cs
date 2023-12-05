using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 1.0f;
	private SpriteRenderer playerSR;
	private Animator anim;
	private Inventory inventory;
	[SerializeField] private UI_Inventory uiInventory;
	

    void Start()
    {
		playerSR = GetComponent<SpriteRenderer>();
        inventory = new Inventory();
		uiInventory.SetInventory(inventory);
		
		ItemWorld.SpawnItemWorld(new Vector3(5,0), new Item {itemType = Item.ItemType.YellowBone, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(-5,0), new Item {itemType = Item.ItemType.GreenGem, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(0,0), new Item {itemType = Item.ItemType.BlueGreenGem, amount = 1});
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

		anim.SetFloat("Speed", Mathf.Abs(speed));

		if(h < 0) {
			playerSR.flipX = true;
		}else if(h > 0) {
			playerSR.flipY = false;
		}

        GetComponent<Rigidbody2D>().velocity = new Vector2 (h*speed, v*speed);
    }
	

	private void OnTriggerEnter2D(Collider2D collider){
		ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
		if (itemWorld != null){
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
		}
	}
}
