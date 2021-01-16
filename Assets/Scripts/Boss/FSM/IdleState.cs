using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : INPCState
{

#region FSMMethods
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
}
