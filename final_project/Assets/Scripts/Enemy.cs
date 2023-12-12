using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public int maxHealth = 100;
	private int currentHealth;
	private int damage = 10;
	private float cooldown = 1f; //seconds
	private float lastAttackedAt = -9999f;
	
	private float speed = 0.8f;
	public float chaseRange = 0f; // the range from enemey when it will chase the player
	public float attackRange = 0.5f;
	
	
	public Animator anim;
	public LayerMask PlayerLayer;
	private Collider2D[] player; // player's collider (if in chase range)
	private Collider2D[] hitPlayer;
	private Collider2D[] attackPlayer;
	private GameObject playerObject; // player
	

    void Start()
    {
		player = null;
		hitPlayer = null;
        currentHealth = maxHealth;
		playerObject = GameObject.Find("Player");
    }
	
	void Update(){
		player = null; // reset player array
		float step = speed * Time.deltaTime;
		
		// find if the player is within chase range using current position of the enemy
		player = Physics2D.OverlapCircleAll(transform.position, chaseRange, PlayerLayer);

		if (player.Length != 0 & (currentHealth > 0.25*maxHealth)){ // if player is within chase range
			transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, step);
			anim.SetFloat("Speed", speed);
			// move towards player
		} else if (player.Length != 0 & (currentHealth <= 0.25*maxHealth)){
			transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, -step);
			anim.SetFloat("Speed", speed);
			// if health is low, move away
		} else {
			anim.SetFloat("Speed", 0f);
		}
		
		attackPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, PlayerLayer);
		
		if (attackPlayer.Length != 0){
			if (Time.time > lastAttackedAt + cooldown){
				Attack();
				lastAttackedAt = Time.time;
			}
		}
		
		
	}
	
	public void Attack(){

        // Play attack animation 
        anim.SetTrigger("Attack");

        //Detect if player is in range of attack
        hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, PlayerLayer);
		
		//Damage player
		if (hitPlayer.Length != 0){
			foreach(Collider2D player in hitPlayer){ 
				player.GetComponent<PlayerCombat>().TakeDamage(damage);
			}
		}
		
    }
	

    public void TakeDamage(int damage) {
        currentHealth -= damage;
		
        Debug.Log("Enemy Health => " + currentHealth);

        if(currentHealth <= 0) {
            Die();
        } else{
			 anim.SetTrigger("Damaged");
		}
    }

    void Die() {
        Debug.Log("Enemy Died");
        //Die animation
        anim.SetTrigger("Dead");

		// Make the enemy stop moving
		chaseRange = 0f;

    }
	
	    void OnDrawGizmosSelected()
    {
        // Display the chase range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
