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
    public bool isDashing = false;
    private bool isSliding = false;

    [Header("Wall Running/Jumping")]
    public float wallJumpForce;
    private bool isTouchingWall = false;

    [Header("References")]
    public Rigidbody2D rb;
    public Collider2D coll;
    public ParticleSystem dash;
    public PlayerSoundManager soundManager;

    // Ground check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    // Coyote Time
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float moveInput;
    private bool jumpInput;
    private bool dashInput;
    private bool slideInput;

    private bool canDash = true;

    public PlayerUIManager playerUIManager;

    public GameObject eye;
    public GameObject eye1;
    public GameObject eye2;

    void Update()
    {
        if(playerUIManager.paused)
        {
            return;
        }
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
        bool downDashInput = dashInput && Input.GetKey(KeyCode.S);
        bool upDashInput = dashInput && Input.GetKey(KeyCode.W); // Check for upward dash input

        if (isGrounded)
        {
            canDash = true;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpInput && (isGrounded || coyoteTimeCounter > 0f))
        {
            Jump();
        }
        else if (jumpInput && isTouchingWall)
        {
            WallJump();
        }

        if (dashInput && !isDashing && canDash)
        {
            if (downDashInput)
            {
                StartCoroutine(DownwardDash());
            }
            else if (upDashInput)
            {
                StartCoroutine(UpwardDash());
            }
            else
            {
                StartCoroutine(Dash());
            }
            canDash = false;
        }

        if (moveInput > 0)
        {
            eye.transform.position = eye1.transform.position;
        }
        else if (moveInput < 0)
        {
            eye.transform.position = eye2.transform.position;
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
        if (Mathf.Abs(rb.linearVelocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(moveInput * moveSpeed, 0), ForceMode2D.Force);
        }
    }

    void ApplyCounterMovement()
    {
        Vector2 velocity = rb.linearVelocity;

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
                rb.AddForce(new Vector2(-velocity.x * moveSpeed * 0.02f, 0), ForceMode2D.Force);
            }
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        soundManager.playJumpSound();
    }

    void WallJump()
    {
        rb.linearVelocity = new Vector2(-moveInput * wallJumpForce, wallJumpForce);
        isTouchingWall = false;
        soundManager.playJumpSound();
    }

    IEnumerator Dash()
    {
        soundManager.playDashSound();
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        dash.Play();
        rb.linearVelocity = new Vector2(moveInput * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    IEnumerator DownwardDash()
    {
        soundManager.playDashSound();
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        dash.Play();
        rb.linearVelocity = new Vector2(0, -dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }
    
    IEnumerator UpwardDash()
    {
        soundManager.playDashSound();
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        dash.Play();
        rb.linearVelocity = new Vector2(0, dashSpeed); // Set velocity for upward dash
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wallrunn"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wallrunn"))
        {
            isTouchingWall = false;
        }
    }
}