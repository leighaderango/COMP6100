using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public int maxHealth = 100;
	private int currentHealth;
	private int damage = 10;
	private float cooldown = 3f; // attack rebound time in seconds
	private float lastAttackedAt = -9999f;
	
	private float speed = 0.8f;
	public float chaseRange = 7f; // the range from enemy when it will chase the player
	public float attackRange = 1f; // the range from enemy when it will attack the player
	
	
	public Animator anim; // animator of the enemy character
	private BoxCollider2D enemy_collider; // collider element of the enemy character
	public LayerMask PlayerLayer; // limits what items will be considered in range (only player, not other objects)
	private Collider2D[] player; // player's collider (if in chase range)
	private Collider2D[] attackPlayer; // player's collider (if in attack range)
	private GameObject playerObject; 
	

    void Start()
    {
		player = null;
        currentHealth = maxHealth;
		enemy_collider = gameObject.GetComponent<BoxCollider2D>();
		playerObject = GameObject.FindWithTag("Player");
    }
	
	void Update(){
		player = null; // reset player array to constantly monitor movement
		float step = speed * Time.deltaTime;
		
		// find if the player is within chase range using current position of the enemy
		player = Physics2D.OverlapCircleAll(transform.position, chaseRange, PlayerLayer);

		if (player.Length != 0 & (currentHealth > 0.25*maxHealth)){ // if player is within chase range
			transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, step);
			anim.SetFloat("Speed", speed);
			// enemy move towards player
		} else if (player.Length != 0 & (currentHealth <= 0.25*maxHealth)){
			transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, -step);
			anim.SetFloat("Speed", speed);
			// if health is low, move away
		} else {
			anim.SetFloat("Speed", 0f);
			// if not in range, stay in place
		}
		
		// detect if player is in range of attack
		attackPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, PlayerLayer);
		
		if (attackPlayer.Length != 0){
			if (Time.time > lastAttackedAt + cooldown){ 
				Attack(attackPlayer);
				lastAttackedAt = Time.time;
				// if player is in range and this enemy has not attacked in last 3 seconds
				// attack player
			}
		} 
		
		
	}
	
	
	// attack player
	public void Attack(Collider2D[] hitPlayer){

        // Play enemy's attack animation 
        anim.SetTrigger("Attack");

		// Damage player
		if (hitPlayer.Length != 0){
			foreach(Collider2D hitplayer in hitPlayer){ 
				hitplayer.GetComponent<PlayerCombat>().TakeDamage(damage);
				
			}
		}
		
    }
	
	// executes when attacked
    public void TakeDamage(int damage) {
		
        currentHealth -= damage;

        if(currentHealth <= 0) {
            Die();
        } else{
			 anim.SetTrigger("Damaged");  // if attacked but not dead, play animation
		}
		Debug.Log("EnemyHealth" + currentHealth);
    }

	// executes when enemy health reaches 0
    void Die() {

        //play death animation
        anim.SetTrigger("Dead");
		
		chaseRange = 0f;
		enemy_collider.enabled = false;
		// Make the enemy stop moving and disable collider
		

    }
	
	void OnDrawGizmosSelected()
    {
        // Display the chase range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
