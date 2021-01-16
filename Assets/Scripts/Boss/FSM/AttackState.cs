using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Boss attack behaviour
/// </summary>
public class AttackState : INPCState
{

#region FSM Methods
    public INPCState EnterState(BossController npc)
    {
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
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

    //Used for projectile targeting
    //private Vector3 SetTarget()
    //{
    //
    //}

#endregion
}
