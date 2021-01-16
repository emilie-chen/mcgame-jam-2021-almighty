using UnityEngine;
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
        Target = NewTarget();
        GetComponent<NavMeshAgent>().speed = 10.0f;
    }

    private static Vector3 NewTarget()
    {
        float x = Random.Range(-500.0f, 500.0f);
        float y = Random.Range(-500.0f, 500.0f);
        return new Vector3(x, 30.0f, y);
    }

    void Update()
    {
        GetComponent<NavMeshAgent>().destination = Target;
    }
}
