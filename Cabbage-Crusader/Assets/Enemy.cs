using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHP = 100;
    public Transform player;
    public bool isFlipped = false;

    int currentHP;
    Rigidbody2D m_Rigidbody2D;

    public int attackDamage = 20;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public Rigidbody2D rb;

    private int random;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        gameObject.tag = "Player";
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<playerCombat>().TakeDamage(attackDamage);
        }
    }
    
    public void takeDMG(int dmg)
    {
        random = Random.Range(1, 4); //random num between 1 and 3 / 33% chance

        if (random == 1)
        {
            // animator.SetTrigger("Block");
            Debug.Log("Attack blocked");

        }
        else
        {
            currentHP -= dmg;
            //Knockback

            //Play hurt animation
            animator.SetTrigger("Hurt");
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

   

    void Die()
    {
        Debug.Log("Enemy died!");

        //Die animation
        animator.SetBool("IsDead", true);


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
