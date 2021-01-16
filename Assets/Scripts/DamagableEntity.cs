using UnityEngine;
using System.Collections;

public class DamagableEntity : MonoBehaviour
{
    public int MaxHealth;
    public int Health;

    public delegate void DeathHandlerFunc(GameObject objectDying);

    public event DeathHandlerFunc DeathHandler;

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

        TryGetComponent<Rigidbody>(out Rigidbody rb);
        rb?.AddForce(kbDir * knockback, ForceMode.Force);


        CheckHealth();
    }

    private void Die()
    {
        DeathHandler?.Invoke(gameObject);
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
}
