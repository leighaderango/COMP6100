using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour{
   public float moveSpeed;
   public Rigidbody2D rb;
   private Vector2 moveInput;
   
   void Update(){
	   moveInput.x = Input.GetAxisRaw("Horizontal");
	   moveInput.y = Input.GetAxisRaw("Vertical");
	   
	   moveInput.Normalize();
	   rb.velocity = moveInput * moveSpeed;
   }
}

