using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyJumping : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(EnableAnimator), Random.Range(0.0f, 1.0f));
    }

    private void EnableAnimator() => GetComponent<Animator>().enabled = true;

    void Update()
    {
        
    }
}
