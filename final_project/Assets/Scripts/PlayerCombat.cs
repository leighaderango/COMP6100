using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 30;
    public LayerMask enemyLayers;

    string attackType;
    void Start(){
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack1")) {
            attackType = "1";
            Attack(attackType);
        }

        if(Input.GetButtonDown("Attack2")) {
            attackType = "2";
            Attack(attackType);
        }

        if(Input.GetButtonDown("Attack3")) {
            attackType = "3";
            Attack(attackType);
        }

        if(Input.GetButtonDown("Block")) {
            attackType = "_block";
            Attack(attackType);
        }
    }

    void Attack(string type) {
        // Play attack animation
        string setAttack = "Attack" + type;  
        anim.SetTrigger(setAttack);

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage the enemies
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
