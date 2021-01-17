using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//BRIEF
//
// This is the main boss class, here we manage health and gameplay conditions as well as state update
//

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class BossController : MonoBehaviour
{
    [SerializeField]
    private INPCState   m_CurrentState;

    public MoveState    m_MoveState     = new MoveState();
    public IdleState    m_IdleState     = new IdleState();
    public AttackState  m_AttackState   = new AttackState();
    public DeathState   m_DeathState    = new DeathState();

    public NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    //protected DamagableEntity m_hp;

    /// <summary>
    /// Used to give invulnerability time to boss
    /// </summary>
    private const float IMMUNE_TIME = 0.5f;
    private bool m_IsImmune = false;

    Transform  m_Target;

    private void OnEnable()
    {
        m_Target = FindObjectOfType<PlayerBehavior>().transform; //This should be done the other way, with player registering itself
        m_CurrentState = m_MoveState.EnterState(npc: this);
        //m_hp = GetComponent<DamagableEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentState = m_CurrentState.UpdateState(npc: this);
        //Debug.Log(m_CurrentState);
    }

    public float Hp
    {
        get => GetComponent<DamagableEntity>().Health;
        set => GetComponent<DamagableEntity>().Health = (int)value;
    }

    public float MaxHp => GetComponent<DamagableEntity>().MaxHealth;

    /// <summary>
    /// Used to trigger death state and logic
    /// </summary>
    private void OnNoMorehp()
    {
        m_CurrentState.ExitState(newState: m_DeathState, npc: this);
    }

    public void OnDamageRecieved(float dmg)
    {
        Hp -= dmg;
        m_IsImmune = true;

        if (Hp <= 0)
            OnNoMorehp();

        Invoke(nameof(RemoveImmunity), IMMUNE_TIME);
    }


    public void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.TryGetComponent<DamagingEntityProps>(out DamagingEntityProps damagingEntityProps);
        if (damagingEntityProps == null)
            return;

        if (!damagingEntityProps.IsEnabled)
            return;

        damagingEntityProps.IsEnabled = false;

        int knockback = damagingEntityProps.Knockback;

        Destroy(collision.collider.gameObject);

        Vector3 kbDir = collision.contacts[0].normal;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(kbDir * knockback, ForceMode.Force);
        Debug.Log(m_IsImmune ? "Immune" : "Non immune");
        if (!m_IsImmune)
            OnDamageRecieved(damagingEntityProps.Damage);
    }

    /// <summary>
    /// Immunity after hit event
    /// </summary>
    private void RemoveImmunity()
    {
        m_IsImmune = false;
    }

    public Transform GetPlayer()
    {
        return m_Target;
    }

    public void SetState(INPCState newState)
    {
        m_CurrentState = newState;
    }
}
