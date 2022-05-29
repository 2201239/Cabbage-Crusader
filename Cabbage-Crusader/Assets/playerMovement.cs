using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(30, 30); //this changes speed
    private bool m_FacingRight = true;
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpAmount = 10;


    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;





        // Update is called once per frame
        void Update()
    {
        float inputX = UnityEngine.Input.GetAxis("Horizontal"); //Axis is Imported from the unity engine. 

        animator.SetFloat("Speed", Mathf.Abs(inputX));

        Vector3 movement = new Vector3(speed.x * inputX, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        //This is a groundcheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);

        // If the input is moving the player right and the player is facing left...
        if (inputX > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (inputX < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        //Jump
        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
        {
             if (isGrounded) //Checks is p;ayer is grounded 
            {
                rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            }
        }

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false); //starts jump animation

        }
        else
        {
            animator.SetBool("IsJumping", true); //starts jump animation

        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
