using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.5f;
	public float stamina = 100f;
	private float sprintSpeed = 5.5f;
	private float drainRate = 1f;
	private float rechargeRate = 1f;
	private bool inventoryOpen = false;

	private Vector2 moveInput;
	private SpriteRenderer playerSR;
	private Animator anim;
	private Canvas inventoryCanvas;
	// connects player, inventory, and inventory UI
	public Inventory inventory;
	[SerializeField] private UI_Inventory uiInventory;
	private GameObject craftingRegion;
	
	

    void Start()
    {
		playerSR = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		inventoryCanvas = GameObject.Find("InventoryCanvas").GetComponent<Canvas>();
		craftingRegion = GameObject.Find("CraftingRegion");
		
        inventory = new Inventory(UseItem);
		uiInventory.SetInventory(inventory);
			// initializes inventory and connects it to the UI
		
		
    }
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.P)){ // Open
			craftingRegion.GetComponent<BoxCollider2D>().enabled = !inventoryOpen;
				// disable craftingRegion collider when inventory is closed
			inventoryCanvas.enabled = !inventoryOpen;
				// when canvas is enabled, p closes
				// when canvas is disabled, p opens
			inventoryOpen = !inventoryOpen;
				// adjust reference
			Time.timeScale = inventoryOpen ? 0 : 1;
				// pause underlying game when inventory is open
		}
		
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
		
		if(Input.GetKey(KeyCode.Space))
        {
			//Debug.Log("Sprinting");
			if(stamina > 0)
			{
				speed = sprintSpeed;
				stamina -= Time.deltaTime * drainRate;
            }
		}
		else
		{
			if(stamina < 100f)
			{
                speed = 3.5f;
                stamina += Time.deltaTime * rechargeRate;
            }
			
		}
		//Debug.Log(stamina);

		anim.SetFloat("Speed", Mathf.Abs(moveInput.magnitude));

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
