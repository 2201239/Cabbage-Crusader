using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDMG = 10;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public int health = 100;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
            {
                PlayerAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void PlayerAttack()
    {
        //Play animation
        animator.SetTrigger("Attack");
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDMG(attackDMG);
        }
    }
    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;

        //Play hurt animation
        animator.SetTrigger("PlayerHurt");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

