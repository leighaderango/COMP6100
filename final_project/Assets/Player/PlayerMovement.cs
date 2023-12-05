using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 1.0f;
<<<<<<< Updated upstream
	private Inventory inventory;
	[SerializeField] private UI_Inventory uiInventory;
	

    void Start()
    {
        inventory = new Inventory();
		uiInventory.SetInventory(inventory);
		
		ItemWorld.SpawnItemWorld(new Vector3(5,0), new Item {itemType = Item.ItemType.YellowBone, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(-5,0), new Item {itemType = Item.ItemType.GreenGem, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(0,0), new Item {itemType = Item.ItemType.BlueGreenGem, amount = 1});
    }

=======
    public Animator anim;

    private SpriteRenderer playerSpriteRenderer;

    bool isAttacking = false;
    //bool isRolling = false;
    bool isBlocking = false;

    void Start() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){

        // Left click = attack1
        if (Input.GetButtonDown("Attack1")){
            isAttacking = true;
            anim.SetBool("isAttacking1", true);
        }
        // Q = attack2
        if(Input.GetButtonDown("Attack2")){
            isAttacking = true;
            anim.SetBool("isAttacking2", true);
        }
        // E = attack3
        if(Input.GetButtonDown("Attack3")){
            isAttacking = true;
            anim.SetBool("isAttacking3", true);
        }
        // Right Click = block
        if(Input.GetButtonDown("Block")){
            isBlocking = true;
            anim.SetBool("isBlocking", true);
        }
        //if(Input.GetButtonDown("R")){
        //
        //}
    }

>>>>>>> Stashed changes
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h < 0) {
            playerSpriteRenderer.flipX = true;
        }else if(h > 0) {
            playerSpriteRenderer.flipX = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(h));

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
