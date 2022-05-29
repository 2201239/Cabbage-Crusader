using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHP = 100;
    int currentHP;
    Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public void takeDMG(int dmg)
    {
        currentHP -= dmg;

        //Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHP <= 0)
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


}
