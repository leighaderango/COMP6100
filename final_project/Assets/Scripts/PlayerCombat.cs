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

    void Start()
    {
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
		
	
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack1"))
        {
            if (Time.time > lastAttackedAt + cooldown)
            {
                attackType = "1";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Attack2"))
        {
            if (Time.time > lastAttackedAt + cooldown)
            {
                attackType = "2";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Attack3"))
        {
            if (Time.time > lastAttackedAt + cooldown)
            {
                attackType = "3";
                Attack(attackType);
                lastAttackedAt = Time.time;
            }
        }

        if (Input.GetButtonDown("Block"))
        {
            attackType = "_block";
            Attack(attackType);
        }
    }

    void Attack(string type)
    {

        // Play attack animation
        string setAttack = "Attack" + type;
        anim.SetTrigger(setAttack);

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage the enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Damaged");
        Debug.Log("Player Health => " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
		
		healthBar.value = currentHealth;
    }

    void Die()
    {
        anim.SetTrigger("Death");
        // add losing game ending stuff here

    }
	
	private void SetHealth(int hp)
    {
        healthBar.value = hp;
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