using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour{
	
   public float moveSpeed;
   public Animator animator;
   private Vector2 moveInput;
   
   
   
   void FixedUpdate(){
	   moveInput.x = Input.GetAxisRaw("Horizontal");
	   moveInput.y = Input.GetAxisRaw("Vertical");
	   
	   moveInput.Normalize();
	   GetComponent<Rigidbody2D>().velocity = moveInput * moveSpeed;
	   
	   animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
   }
}

