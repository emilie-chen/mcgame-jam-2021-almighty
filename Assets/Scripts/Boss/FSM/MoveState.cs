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
    private float m_MaxRadius = 150.0f;


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
        if (!IsLineOfSight(npc))
        {
            if ( sqrLen > GetSqrDist(m_MaxRadius) )
                npc.m_NavMeshAgent.SetDestination(m_Destination.position);
        }
        //else
        //{
        //    Vector3 targetPosition = offset.normalized * -m_MinRadius;
        //    npc.m_NavMeshAgent.SetDestination(targetPosition);
        //    npc.transform.rotation.SetLookRotation(m_Destination.position);
        //}

        if (IsLineOfSight(npc) && sqrLen < GetSqrDist(m_MaxRadius) && offset.sqrMagnitude > GetSqrDist(m_MinRadius))
            ExitState(npc.m_AttackState, npc);

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
        if(Physics.Raycast(npc.transform.position, npc.transform.position - m_Destination.transform.position, out hit, 100))
        {
            Debug.DrawRay(npc.transform.position, (m_Destination.position - npc.transform.position) * 100, Color.green);
            return true;
        }
        return false;
    }

    #endregion

    float GetSqrDist(float val)
    {
        return val * val;
    }
}
