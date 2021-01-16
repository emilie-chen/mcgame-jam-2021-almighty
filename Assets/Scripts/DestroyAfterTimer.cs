using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    public float timer;
    public float scaleDownFactor;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", timer);
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, scaleDownFactor);
    }

    private void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
