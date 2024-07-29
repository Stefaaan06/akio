using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip hitSound;
    public AudioClip shootSound;
    
    public cameraShake cameraShake;
    public AudioSource audioSource;

    private float volumeDeviation = 0.1f;
    private float pitchDeviation = 0.1f;

    private void PlaySound(AudioClip clip)
    {
        audioSource.volume = 1.0f + Random.Range(-volumeDeviation, volumeDeviation);
        audioSource.pitch = 1.0f + Random.Range(-pitchDeviation, pitchDeviation);
        audioSource.PlayOneShot(clip);
    }

    public void playDashSound()
    {
        PlaySound(dashSound);
    }
    
    public void playJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void playDamageSound()
    {
        cameraShake.ShakeOnce(.10f, .5f);
        PlaySound(damageSound);
    }

    public void playHitSound()
    {
        PlaySound(hitSound);
    }

    public void playShootSound()
    {
        cameraShake.ShakeOnce(.6f);
        PlaySound(shootSound);
    }
}