using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Vector3 Target;

    void Start()
    {
        Target = transform.position;
    }

    void Update()
    {
        Target = GameObject.FindWithTag("Player").transform.position;
        GetComponent<NavMeshAgent>().destination = Target;
    }
}
