using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : MonoBehaviour
{
    [SerializeField]
    private INPCState m_CurrentState;

    public MoveState    m_MoveState     = new MoveState();
    public IdleState    m_IdleState     = new IdleState();
    public AttackState  m_AttackState   = new AttackState();

    public NavMeshAgent m_NaveMeshAgent;

    private void OnEnable()
    {
        m_CurrentState = m_IdleState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentState = m_CurrentState.UpdateState(this);
    }
}
