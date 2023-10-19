using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    public CameraSlide cameraSlide;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Player") 
        {
            if (transform.position.x < -0.01f) 
            {
                StartCoroutine(cameraSlide.SlideCamera(1, 0));
            } else if (transform.position.x > 0.01f) {
                StartCoroutine(cameraSlide.SlideCamera(-1, 0));
            }

            cameraSlide.isChecking = false;
            col.transform.position = new Vector3(col.GetComponent<PlayerController>().startingPos.x, col.GetComponent<PlayerController>().startingPos.y, 0);
            cameraSlide.isChecking = true;
        }
    }
}
