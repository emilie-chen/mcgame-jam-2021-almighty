using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : INPCState
{
    public INPCState EnterState(BossController npc)
    {
        Debug.Log("Victory");
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        return this;
    }

    //In case of Death state, Exit will be handled differently
    public INPCState ExitState(INPCState newState, BossController npc)
    {
        return newState.EnterState(npc);
    }
}
