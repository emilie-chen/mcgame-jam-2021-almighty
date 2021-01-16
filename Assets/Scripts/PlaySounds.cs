using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip[] audios;

    public bool hasRandomPitch;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    public void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void playSound(int index) {
        if (hasRandomPitch) {
            audioSrc.pitch = Random.Range(minPitch,maxPitch);
        }
        audioSrc.PlayOneShot(audios[index]);
    }
}
