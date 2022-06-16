using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public bool isFlipped = false;

    Rigidbody2D m_Rigidbody2D;

    public int attackDamage = 20;
    public int enragedattackDamage = 25;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public Rigidbody2D rb;

    public int enemyMaxHealth = 70;
    public int enemyCurrentHealth;

    public HealthBarEnemy healthBarEnemy;

    private int random;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCurrentHealth = enemyMaxHealth;
        gameObject.tag = "Player";

        healthBarEnemy.SetMaxHealthEnemy(enemyMaxHealth);
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player2Combat>().TakeDamage(attackDamage);
        }

    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player2Combat>().TakeDamage(enragedattackDamage);
        }

    }


    public void takeDMG(int dmg)
    {

        
            enemyCurrentHealth -= dmg;
            healthBarEnemy.SetHealthEnemy(enemyCurrentHealth);
            //Knockback
            animator.SetTrigger("Hurt");
            if (isFlipped == true)
            {
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
            }
            else
            {
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);

            }

            if (enemyCurrentHealth <= 100)
            {
            animator.SetBool("Stage2", true);
        }

            //Play hurt animation
        
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        Debug.Log("Enemy died!");

        //Die animation
        animator.SetTrigger("Died");

        //Disable enemy
        GetComponent<Collider2D>().enabled = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        this.enabled = false;


    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}