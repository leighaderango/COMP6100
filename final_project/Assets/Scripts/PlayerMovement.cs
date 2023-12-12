using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f;
	private Vector2 moveInput;
	private SpriteRenderer playerSR;
	private Animator anim;
	
	// connects player, inventory, and inventory UI
	private Inventory inventory;
	[SerializeField] private UI_Inventory uiInventory;

    void Start()
    {
		playerSR = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		
        inventory = new Inventory(UseItem);
		uiInventory.SetInventory(inventory);
			// initializes inventory and connects it to the UI
		
    }

    void FixedUpdate()
    {
		moveInput.x = Input.GetAxisRaw("Horizontal");
	   	moveInput.y = Input.GetAxisRaw("Vertical");
	
		if(moveInput.x < 0) {
			playerSR.flipX = true;
		}else if(moveInput.x > 0) {
			playerSR.flipX = false;
		}

		anim.SetFloat("Speed", Mathf.Abs(moveInput.x));

		// Set y axis animation to run here
		GetComponent<Rigidbody2D>().velocity = moveInput * speed;
        //GetComponent<Rigidbody2D>().velocity = new Vector2 (h*speed, v*speed);
    }

	private void OnTriggerEnter2D(Collider2D collider){
		ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
		if (itemWorld != null){
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
		}
	}

	private void UseItem(Item item){
		//KEEP THIS HERE the inventory doesn't work without it
	}

}
