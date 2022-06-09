using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackBehavior : MonoBehaviour
{
    //public variables
    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackDistance; //min distance for attack
    public float moveSpeed;
    public float timer; //timer between attacks

    //private variables
    private RaycastHit2d hit;
    private GameObject target;
    private Animator anim;
    private float distance; //store distance b/w enemy and player
    private bool attackMode;
    private bool inRange; //check if player is in range
    private bool cooling; //enemy cooldown
    private float intTimer; 

    void Awake()
    {
        intTimer = timer; //stores the initial value of the timer
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
            RaycastDebugger;
        }
        //When player is detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange == false;
        }
        if(inRange == false)
        {
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true; 
        }
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.green);
        }
    }
}
