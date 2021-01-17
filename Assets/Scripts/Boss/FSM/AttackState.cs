using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Boss attack behaviour
/// </summary>
public class AttackState : INPCState
{
    private float m_Timer;
    private float m_FireTimer;
    private const float MAX_TIME = 3;
    private const float FIRE_RATE = 0.3f;

    private SendFireBall m_FireSender;

#region FSM Methods
    public INPCState EnterState(BossController npc)
    {
        m_FireSender = npc.GetComponentInChildren<SendFireBall>();
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        m_Timer += Time.deltaTime;
        m_FireTimer += Time.deltaTime;

        m_FireSender.SetTarget(PredictedPosition(npc.GetPlayer().transform.position, npc.transform.position, npc.GetPlayer().GetComponent<Rigidbody>().velocity, m_FireSender.GetProjectileSpeed()));

        if (m_FireTimer >= FIRE_RATE)
        {
            m_FireSender.SpawnFlame();
            m_FireTimer -= FIRE_RATE;
        }

        if (m_Timer >= MAX_TIME)
            return ExitState(npc.m_MoveState, npc);

        return this;
    }

    public INPCState ExitState(INPCState newState, BossController npc)
    {
        return newState.EnterState(npc);
    }

#endregion

#region State Specific

    //Overall attack logic
    private void AttackTarget(PlayerBehavior target)
    {
        
    }

    private Vector3 PredictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 displacement = targetPosition - shooterPosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            Debug.Log("Position prediction is not feasible.");
            return targetPosition;
        }
        //also Sine Formula
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }

    #endregion
}
