using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip[] jumpSoundEffects;
    public AudioClip[] rebirthSoundEffects;
    public AudioClip[] deathSoundEffects;

    public AudioSource audioSource;

    public void PlayJumpSound() {
        int rand = Random.Range(0, jumpSoundEffects.Length);
        audioSource.clip = jumpSoundEffects[rand];
        audioSource.Play();
    }

    public void PlayRebirthSound() {
        int rand = Random.Range(0, rebirthSoundEffects.Length);
        audioSource.clip = rebirthSoundEffects[rand];
        audioSource.Play();
    }

    public void PlayDeathSound() {
        int rand = Random.Range(0, deathSoundEffects.Length);
        audioSource.clip = deathSoundEffects[rand];
        audioSource.Play();
    }
}
