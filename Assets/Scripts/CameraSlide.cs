using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float delay;
    public float disanceToSlide;

    private bool cameraLock = false;

    private void Start() {
        speed /= 1000;
        delay /= 1000000;
    }

    private void Update() {
        if (!cameraLock) { // Check if camera is allowed to check for player position.
            CheckBoundaries();
        }
    }

    private void CheckBoundaries() {
        if (player.transform.position.x > transform.position.x + disanceToSlide) { // Check if player is at buffer distance for camera slide.
            StartCoroutine(SlideCamera(1, transform.position.x + 9, false));  // Move right.
        } else if (player.transform.position.x < transform.position.x - disanceToSlide) {
            StartCoroutine(SlideCamera(-1, transform.position.x - 9, false)); // Move left.
        }
    }

    public IEnumerator SlideCamera(int direction, float newPos, bool playerReset) { // Slides Camera: +1 right and -1 left, target position for camera, should this be handled as player death?
        cameraLock = true;
        if (direction > 0) { // Slide to given position.
            while (transform.position.x <= newPos) {
                transform.position += new Vector3(direction * speed, 0, 0);
                yield return new WaitForSeconds(delay);
            }
        } else {
            while (transform.position.x >= newPos) {
                transform.position += new Vector3(direction * speed, 0, 0);
                yield return new WaitForSeconds(delay);
            }
        }
        if (playerReset) {
            player.GetComponent<PlayerController>().ResetCharacterCallback2(); // Sends response back to PlayerController script.
        }
        cameraLock = false;
    }
}
