using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f;
    public int attackDamage = 30;
    private int maxHealth = 100;
    private int currentHealth;
    private float cooldown = 1f; //seconds
    private float lastAttackedAt = -9999f;

    public Animator anim;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    string attackType;
	public Slider healthBar;
	public Canvas endGameCanvas;
	private GameObject leftAnimation;
	private GameObject rightAnimation;


    void Start(){
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
		
		rightAnimation = GameObject.Find("RightAnimation");
		leftAnimation = GameObject.Find("LeftAnimation");
    }


    void Update(){
        if (Input.GetButtonDown("Attack1")){
            if (Time.time > lastAttackedAt + cooldown){
                attackType = "1";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Attack2")){
            if (Time.time > lastAttackedAt + cooldown){
                attackType = "2";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Attack3")){
            if (Time.time > lastAttackedAt + cooldown){
                attackType = "3";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Block")){
            attackType = "_block";
            Attack(attackType);
        }
    }

    void Attack(string type){

        // Play attack animation
        string setAttack = "Attack" + type;
        anim.SetTrigger(setAttack);

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage the enemies
        foreach (Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

	// executes when attacked by enemy
    public void TakeDamage(int damage){
        
		currentHealth -= damage;
        anim.SetTrigger("Damaged");

        if (currentHealth <= 0){
            Die();
        }
		
		healthBar.value = currentHealth; // update health bar value
    }

    void Die(){
		
		// play death animation
        anim.SetTrigger("Death");
		
		// show and animate end game canvas
		endGameCanvas.GetComponent<Canvas>().enabled = true;
		leftAnimation.GetComponent<Animator>().SetBool("FlameActive", true);
		rightAnimation.GetComponent<Animator>().SetBool("FlameActive", true);
		
		// stop game underneath
		Time.timeScale = 0;
       
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
	
   
}