using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxHealth = 1;
    public Animator animator;
    Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


        public void takeDMG(int dmg)
        {
        //Knockback
        animator.SetTrigger("boxHit");
        GetComponent<Collider2D>().enabled = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

}

