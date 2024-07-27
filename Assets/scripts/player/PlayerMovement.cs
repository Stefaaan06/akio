using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashDuration;
    public float slideSpeed;
    public float slideDuration;
    private bool isDashing = false;
    private bool isSliding = false;
    private bool canDoubleJump = false;

    [Header("References")]
    public Rigidbody2D rb;
    public Collider2D coll;
    public ParticleSystem dash;
    
    // Ground check
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
            ApplyMovement();
            ApplyCounterMovement();
        }
    }

    void ApplyMovement()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(moveInput * moveSpeed, 0), ForceMode2D.Force);
        }
    }

    void ApplyCounterMovement()
    {
        Vector2 velocity = rb.velocity;

        if (isGrounded)
        {
            if (Mathf.Abs(velocity.x) > 0.01f && Mathf.Abs(moveInput) < 0.05f)
            {
                rb.AddForce(new Vector2(-velocity.x * moveSpeed * 0.5f, 0), ForceMode2D.Force);
            }
        }
        else
        {
            if (Mathf.Abs(velocity.x) > 0.01f && Mathf.Abs(moveInput) < 0.02f)
            {
                rb.AddForce(new Vector2(-velocity.x * moveSpeed * 0.05f, 0), ForceMode2D.Force);
            }
        }

 
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        dash.Play();
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