using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerBehavior : MonoBehaviour
{
    public int MaxHealth;
    public int Health;

    private void Start()
    {
        MaxHealth = 100;
        Health = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.TryGetComponent<DamagingEntityProps>(out DamagingEntityProps damagingEntityProps);
        if (damagingEntityProps == null)
            return;

        if (!damagingEntityProps.IsEnabled)
            return;

        damagingEntityProps.IsEnabled = false;

        int damage = damagingEntityProps.Damage;
        int knockback = damagingEntityProps.Knockback;

        Destroy(collision.collider.gameObject);

        Health -= damage;

        Vector3 kbDir = collision.contacts[0].normal;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(kbDir * knockback, ForceMode.Force);


        CheckHealth();
    }

    private void Die()
    {
        Debug.LogWarning("Player died");
    }

    private void CheckHealth()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if (Health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Terrain"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 3, transform.position, ForceMode.VelocityChange);
            }
        }
    }
}
