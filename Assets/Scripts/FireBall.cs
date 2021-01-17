using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public GameObject smoke;
    public GameObject bigSmoke;
    public GameObject tinySmoke;

    public Rigidbody rb;
    public bool bounce;
    public bool castTinySmokeInstead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Spawnsmoke), 0.05f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bounce)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + (-transform.forward * speed));
        }
        else {
            GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * speed));

        }
    }

    private void Spawnsmoke() {
        if (!castTinySmokeInstead)
        {
            Instantiate(smoke, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(tinySmoke, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!castTinySmokeInstead)
        {
            Instantiate(bigSmoke, transform.position, transform.rotation);
        }
        else {
            Instantiate(tinySmoke, transform.position, transform.rotation);
        }

        if (collision.gameObject.tag == "Building")
        {
            bounce = true;
        }
        else {
            Destroy(this);
        }
    }
}
