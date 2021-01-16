using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public Transform player;

    public AudioSource audioSrc;
    public AudioClip[] audios;

    public bool randomPitch;
    public float minPitch;
    public float maxPitch;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(int index) {

        audioSrc.volume = Mathf.Clamp(1 / (Mathf.Abs(transform.position.x - player.position.x)), 0.2f, 1f);

        audioSrc.panStereo = (transform.position.x - player.position.x) / 10;

        if (randomPitch) {
            audioSrc.pitch = Random.Range(minPitch,maxPitch);
        }
        audioSrc.PlayOneShot(audios[index]);
    }
}
