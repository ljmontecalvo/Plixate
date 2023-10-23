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
            GameObject.FindGameObjectWithTag("Sound Effects").GetComponent<SoundEffects>().PlayDeathSound();
            player.GetComponent<PlayerController>().ResetCharacterCallback1(); // Starts player death sequence.
            if (transform.position.x < -0.01f) 
            {
                StartCoroutine(cameraSlide.SlideCamera(1, 0, true));
            } else if (transform.position.x > 0.01f) {
                StartCoroutine(cameraSlide.SlideCamera(-1, 0, true));
            }
        }
    }
}
