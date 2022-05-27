using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(30, 30); //this changes speed

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        float inputX = UnityEngine.Input.GetAxis("Horizontal"); //Axis is Imported from the unity engine. 
        float inputY = UnityEngine.Input.GetAxis("Vertical"); //Axis is Imported from the unity engine. 

        animator.SetFloat("Speed", Mathf.Abs(inputX));

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

    }
}
