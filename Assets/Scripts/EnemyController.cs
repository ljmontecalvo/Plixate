using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 patrolPoint1;
    public Vector3 patrolPoint2;

    public float speed;
    public float waitDelay;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    private bool goingLeft = true;
    private bool goingRight = false;
    private bool isMoving = true;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (goingLeft && isMoving) {
            GoLeft();
        }       
        else if (goingRight && isMoving) {
            GoRight();
        }

        CheckForFlip();
        Animate();
    }

    public void GoLeft() {
        if (transform.position.x >= patrolPoint1.x) {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        } else {
            goingLeft = false;
            goingRight = true;
            isMoving = false;
            StartCoroutine(Delay(waitDelay));
        }
    }

    public void GoRight() {
        if (transform.position.x <= patrolPoint2.x) {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y);
        } else {
            goingLeft = true;
            goingRight = false;
            isMoving = false;
            StartCoroutine(Delay(waitDelay));
        }
    }

    public void CheckForFlip() {
        if (rb.velocity.x > 0.01f) {
            sp.flipX = false;
        } else if (rb.velocity.x < 0.01f) {
            sp.flipX = true;
        }
    }

    public void Animate() {
        if (rb.velocity.y < -0.01f) {
            anim.SetBool("isFalling", true);
        } else {
            anim.SetBool("isFalling", false);
            if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f) {
                anim.SetBool("isRunning", true);
            } else {
                anim.SetBool("isRunning", false);
            }
        }
    }

    private IEnumerator Delay(float delay) {
        yield return new WaitForSeconds(delay);
        isMoving = true;
    }
}
