using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
    [HideInInspector] public Vector3 startingPos;

    private void Start() 
    {
        startingPos = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (IsGrounded()) 
        {
            animator.SetBool("isJumping", false);

            if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f) 
            {
                animator.SetBool("isRunning", true);
            } else {
                animator.SetBool("isRunning", false);
            }
        } else {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}