using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float range;
    private void Update()
    {

        foreach(playerCombat enemy in playerCombat.GetEnemyList())
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) < range)
            {
                animator.SetBool("inRange", true);
            }
            
        }
    }
}
