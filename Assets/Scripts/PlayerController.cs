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
    public GameObject soundEffects;

    private void Start() 
    {
        startingPos = new Vector3(transform.position.x, transform.position.y, 0);

        soundEffects = GameObject.FindGameObjectWithTag("Sound Effects");
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            soundEffects.GetComponent<SoundEffects>().PlayJumpSound();
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

    // Player Death Handler Functions
    public void ResetCharacterCallback1() { // Init death sequence.
        GetComponent<SpriteRenderer>().enabled = false;

        if (Vector3.Distance(transform.position, Vector3.zero) < 10f) { // Skip to next function if camera hasn't moved.
            ResetCharacterCallback2();
        }

        transform.position = startingPos; // Reset player's position.
    }

    public void ResetCharacterCallback2() { // Called after response from CameraSlide.
        GetComponent<SpriteRenderer>().enabled = true;
        soundEffects.GetComponent<SoundEffects>().PlayRebirthSound();
        animator.Rebind();
        animator.Update(0f);
    }
}