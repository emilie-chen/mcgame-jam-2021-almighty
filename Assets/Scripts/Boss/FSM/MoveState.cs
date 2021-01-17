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
    Vector3 m_DestPos;
    private float m_MoveSpeed = 20;

    /// <summary>
    /// Minimum range from player
    /// </summary>
    [SerializeField]
    private const float MIN_RADIUS = 15.0f;

    /// <summary>
    /// Maximum range from player
    /// </summary>
    [SerializeField]
    private const float MAX_RADIUS = 30.0f;

    private float m_MoveTime;
    private float m_Timer;

#region FSMMethods
    public INPCState EnterState(BossController npc)
    {
        SetDest(npc.GetPlayer().gameObject.transform);

        m_DestPos = new Vector3();
        m_DestPos.x = m_Destination.position.x + Random.Range(-MAX_RADIUS, MAX_RADIUS);
        m_DestPos.z = m_Destination.position.z + Random.Range(-MAX_RADIUS, MAX_RADIUS);

        m_MoveTime = Random.Range(1, 5);

        npc.m_NavMeshAgent.speed = m_MoveSpeed;
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        m_Timer += Time.deltaTime;
        Vector3 offset = npc.transform.position - m_Destination.position;
        float sqrLen = offset.sqrMagnitude;

        
        npc.m_NavMeshAgent.SetDestination(m_DestPos);

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

        if (IsLineOfSight(npc) && sqrLen < GetSqrDist(MAX_RADIUS) && offset.sqrMagnitude > GetSqrDist(MIN_RADIUS) && m_Timer >= m_MoveTime)
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
        float t = centerOffset.magnitude / MAX_RADIUS;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }
        return centerOffset * t * t;
    }

    private Vector3 Avoidance(BossController npc)
    {
        Vector3 avoidance = Vector3.zero;
        if (Vector3.SqrMagnitude(m_Destination.position - npc.transform.position) < GetSqrDist(MIN_RADIUS))
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
