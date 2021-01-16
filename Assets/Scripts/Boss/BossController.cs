using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Material[] materials; //USED FOR TESTING

    [SerializeField]
    private INPCState m_CurrentState;

    public MoveState    m_MoveState     = new MoveState();
    public IdleState    m_IdleState     = new IdleState();
    public AttackState  m_AttackState   = new AttackState();

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
