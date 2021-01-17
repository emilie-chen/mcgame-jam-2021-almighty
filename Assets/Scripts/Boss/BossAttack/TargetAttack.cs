using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttack : FireBall
{
    public float speed;
    public GameObject smoke;
    public GameObject bigSmoke;
    public Rigidbody rb;
    public bool bounce;

    private void Start()
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
        else
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * speed));

        }
    }

    private void Spawnsmoke()
    {
        Instantiate(smoke, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(bigSmoke, transform.position, transform.rotation);

        if (collision.gameObject.tag == "Building")
        {
            bounce = true;
        }
        else
        {
            Destroy(this);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
