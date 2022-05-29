using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

    }

    void Attack()
    {
        //Play animation
        animator.SetTrigger("Attack");
        //Detect enemies in range
        //Damage enemies
    }


}
