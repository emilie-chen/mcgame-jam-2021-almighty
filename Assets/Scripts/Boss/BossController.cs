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
    private float   m_ImmuneTime    = 0.5f;
    private bool    m_IsImmune      = false;

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
        Immunity();
    }

    public float Gethp()
    {
        return GetComponent<DamagableEntity>().Health;
    }

    //private void Sethp(float newhp)
    //{
    //    m_hp.Health = (int)newhp;
    //}

    /// <summary>
    /// Used to trigger death state and logic
    /// </summary>
    private void OnNoMorehp()
    {
        m_CurrentState.ExitState(newState: m_DeathState, npc: this);
    }

    public void OnDamageRecieved(float dmg)
    {
        //Sethp(m_hp.Health - dmg);
        m_IsImmune = true;

        if (Gethp() <= 0)
            OnNoMorehp();
    }


    private void OnCollisionEnter(Collision collision)
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

        if (!m_IsImmune)
            OnDamageRecieved(damagingEntityProps.Damage);
    }

    /// <summary>
    /// Immunity after hit event
    /// </summary>
    private void Immunity()
    {
        if (m_IsImmune)
        {
            float timer = 0;
            timer += Time.deltaTime;

            if (timer >= m_ImmuneTime)
                m_IsImmune = false;
        }
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
