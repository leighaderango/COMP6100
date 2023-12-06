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

	bool roll = false;
	

    void Start()
    {
		playerSR = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
        inventory = new Inventory(UseItem);
		uiInventory.SetInventory(inventory);
		
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

		if(h < 0) {
			playerSR.flipX = true;
		}else if(h > 0) {
			playerSR.flipX = false;
		}	

		anim.SetFloat("Speed", Mathf.Abs(h*speed));

		// Set y axis animation to run here
		
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
		// controls what happens when item is used
	}
}
