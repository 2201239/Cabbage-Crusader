using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolgie_Idle : StateMachineBehaviour
{
    public float enemySpeed = 3f;
    public float enemyAttackRange = 4f;
    Transform player;
    Rigidbody2D rb;
    Enemy1 enemy;
    private int random;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy1>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        enemy.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, enemySpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        random = Random.Range(1, 6); //random num between 1 and 5

        if (Vector2.Distance(player.position, rb.position) <= enemyAttackRange)
        {
            if (random == 1)
            {

            }
            else
            {
                animator.SetTrigger("Attack");
            }
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
