using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manage Boss movement behaviour
/// </summary>
public class MoveState : INPCState
{
    Transform m_Destination;
    private float m_MoveSpeed = 20;

    /// <summary>
    /// Minimum range from player
    /// </summary>
    [SerializeField]
    private float m_MinRadius = 15.0f;

    /// <summary>
    /// Maximum range from player
    /// </summary>
    [SerializeField]
    private float m_MaxRadius = 30.0f;


#region FSMMethods
    public INPCState EnterState(BossController npc)
    {
        SetDest(npc.GetPlayer().gameObject.transform);

        npc.m_NavMeshAgent.speed = m_MoveSpeed;
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        Vector3 offset = npc.transform.position - m_Destination.position;
        float sqrLen = offset.sqrMagnitude;

        if (StayInRange(npc) != Vector3.zero)
            npc.m_NavMeshAgent.SetDestination(StayInRange(npc));

        if (Avoidance(npc) != Vector3.zero)
            npc.m_NavMeshAgent.SetDestination(Avoidance(npc));

        NavMeshPath path = new NavMeshPath();
        npc.m_NavMeshAgent.CalculatePath(m_Destination.position, path);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            npc.m_NavMeshAgent.SetDestination(m_Destination.position);
        }

        //SAFEGUARD
        if (sqrLen > 10000)
            npc.m_NavMeshAgent.Warp(RandomPos());

        if (IsLineOfSight(npc) && sqrLen < GetSqrDist(m_MaxRadius) && offset.sqrMagnitude > GetSqrDist(m_MinRadius))
            return ExitState(npc.m_AttackState, npc);
        
        return this;
    }

    public INPCState ExitState(INPCState newState, BossController npc)
    {
        return newState.EnterState(npc);
    }
    
#endregion

#region StateSpecific

    //Use to set destination
    private Transform SetDest(Transform newDest)
    {
        m_Destination = newDest;
        return m_Destination;
    }

    //Use to check if agent has LOS with player
    private bool IsLineOfSight(BossController npc)
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = npc.transform.position;
        
        ray.direction = m_Destination.position - npc.transform.position;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<PlayerBehavior>())
                return true;
            return false;
        }
        return false;
    }
    
    private Vector3 StayInRange(BossController npc)
    {
        Vector3 centerOffset = m_Destination.position - npc.transform.position;
        float t = centerOffset.magnitude / m_MaxRadius;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }
        return centerOffset * t * t;
    }

    private Vector3 Avoidance(BossController npc)
    {
        Vector3 avoidance = Vector3.zero;
        if (Vector3.SqrMagnitude(m_Destination.position - npc.transform.position) < GetSqrDist(m_MinRadius))
        {
            avoidance += npc.transform.position - m_Destination.position;
        }

        return avoidance;
    }

    Vector3 RandomPos()
    {
        Vector3 dest = new Vector3();
        dest.x = Random.Range(0, 100);
        dest.z = Random.Range(0, 100);
        return dest;
    }

    #endregion

    float GetSqrDist(float val)
    {
        return val * val;
    }
}
