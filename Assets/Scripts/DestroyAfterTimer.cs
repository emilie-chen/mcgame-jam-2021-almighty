using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", timer);
    }

    private void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
