﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class NPC : MonoBehaviour
{
    public Vector3 Target;

    public bool IsInPanicMode => GetComponent<NavMeshAgent>().speed == 20.0f;

    void Start()
    {
        Target = transform.position;
        GetComponent<DamagableEntity>().DeathHandler += Destroy;
        GetComponent<NavMeshAgent>().speed = 10.0f;

        InvokeRepeating(nameof(ResetState), 1.0f, 20.0f);
    }

    public void DoDamage(int damage)
    {
        GetComponent<DamagableEntity>().Health -= damage;
    }

    public void Runaway(Vector3 target)
    {
        GetComponent<NavMeshAgent>().speed = 20.0f;
        Target = target;
        Invoke(nameof(ResetStateAfterRunaway), 10.0f);
    }

    public void ResetStateAfterRunaway()
    {
        GetComponent<NavMeshAgent>().speed = 10.0f;
        ResetState();
    }

    private void ResetState()
    {
        if (!IsInPanicMode)
        {
            Target = NewTarget();
            GetComponent<NavMeshAgent>().speed = 10.0f;
        }
    }

    private static Vector3 NewTarget()
    {
        float x = Random.Range(-175, 110);
        float y = Random.Range(-175, 110);

        return new Vector3(x, 0, y);
    }

    void Update()
    {
        GetComponent<NavMeshAgent>().destination = Target;
    }
}
