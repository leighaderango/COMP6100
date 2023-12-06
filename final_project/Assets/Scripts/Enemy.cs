using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        anim.SetTrigger("Damaged");
        Debug.Log("Skeleton Health => " + currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Skeleton Died");
        //Die animation
        anim.SetTrigger("isDead");

        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        

    }

}
