using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (m_Destination == null)
            SetDestination(npc.GetPlayer().gameObject.transform);

        npc.m_NavMeshAgent.speed = m_MoveSpeed;
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        Vector3 offset = npc.transform.position - m_Destination.position;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen < GetSqrDist(m_MaxRadius) && sqrLen > GetSqrDist(m_MinRadius))
            npc.m_NavMeshAgent.isStopped = true;
        else
            npc.m_NavMeshAgent.isStopped = false;

        npc.m_NavMeshAgent.SetDestination(StayInRange(npc));

        npc.m_NavMeshAgent.SetDestination(Avoidance(npc));

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
    private Transform SetDestination(Transform newDest)
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

    #endregion

    float GetSqrDist(float val)
    {
        return val * val;
    }
}
