using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Boss movement behaviour
/// </summary>
public class MoveState : INPCState
{
    Material testMat;

#region FSMMethods
    public INPCState EnterState(BossController npc)
    {
        testMat = npc.materials[1];
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

#region StateSpecific

    private void MoveToDest()
    {

    }

#endregion
}
