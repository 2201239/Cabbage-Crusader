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

    // Update is called once per frame
    void Update()
    {
        float inputX = UnityEngine.Input.GetAxis("Horizontal"); //Axis is Imported from the unity engine. 

        animator.SetFloat("Speed", Mathf.Abs(inputX));

        Vector3 movement = new Vector3(speed.x * inputX, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

 

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
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
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
