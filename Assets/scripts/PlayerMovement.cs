using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float slideSpeed = 12f;
    public float slideDuration = 0.5f;
    private bool isDashing = false;
    private bool isSliding = false;
    private bool canDoubleJump = false;

    [Header("References")]
    public Rigidbody2D rb;
    public Collider2D coll;

    //groundCheck
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    private float moveInput;
    private bool jumpInput;
    private bool dashInput;
    private bool slideInput;
    
    private bool canDash = true;
    

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
        slideInput = Input.GetKeyDown(KeyCode.LeftControl);
        
        
        if (isGrounded)
        {
            //ADD COOLDOWN AT SOME POINT
            canDash = true;
        }
        if (jumpInput)
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        if (dashInput && !isDashing && canDash)
        {
            StartCoroutine(Dash());
            canDash = false;
        }

        if (slideInput && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        if (!isDashing && !isSliding)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(moveInput * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    IEnumerator Slide()
    {
        isSliding = true;
        float originalSpeed = moveSpeed;
        moveSpeed = slideSpeed;
        yield return new WaitForSeconds(slideDuration);
        moveSpeed = originalSpeed;
        isSliding = false;
    }
}
