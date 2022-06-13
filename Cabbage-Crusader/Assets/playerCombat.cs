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

    //health code
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
   

    //player is an enemy for bandits
    public static List<playerCombat> enemyList = new List<playerCombat>();

    public static List<playerCombat> GetEnemyList()
    {
        return enemyList;
    }

    private void Start()
    {
        enemyList.Add(this);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int damage = 20;
         
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


    public void TakeDamage(int damage)
    {
        damage = 20;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        //Play hurt animation
        animator.SetTrigger("PlayerHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");
        enemyList.Remove(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

