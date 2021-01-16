using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public GameObject smoke;
    public GameObject bigSmoke;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawnsmoke), 0.05f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * speed));
    }

    private void Spawnsmoke() {
        Instantiate(smoke, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(bigSmoke,transform.position,transform.rotation);
        Destroy(this);
    }
}
