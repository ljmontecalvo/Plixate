using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float delay;
    public float disanceToSlide;
    [HideInInspector] public bool isChecking = true;

    private void Update() {
        if (isChecking) {
            if (player.transform.position.x > transform.position.x + disanceToSlide) {
                StartCoroutine(SlideCamera(1, transform.position.x + 9));  
            } else if (player.transform.position.x < transform.position.x - disanceToSlide) {
                StartCoroutine(SlideCamera(-1, transform.position.x - 9));
            }
        }
    }

    public IEnumerator SlideCamera(int direction, float newPos) {   
        isChecking = false;
        if (direction > 0) {
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
        isChecking = true;
    }
}
