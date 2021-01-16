using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Boss attack behaviour
/// </summary>
public class AttackState : INPCState
{
    Material testMat;

#region FSM Methods
    public INPCState EnterState(BossController npc)
    {
        testMat = npc.materials[2];
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        npc.GetComponent<Renderer>().material = testMat;
        return this;
    }

    public INPCState ExitState(INPCState newState, BossController npc)
    {
        return newState.EnterState(npc);
    }

#endregion

#region State Specific

    private bool AttackTarget<T>()
    {
        return true;
    }

#endregion
}
