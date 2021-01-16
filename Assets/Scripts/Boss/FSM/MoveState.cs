using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Boss movement behaviour
/// </summary>
public class MoveState : INPCState
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

#region StateSpecific

    //Used to Move to set destination
    private void MoveToDest()
    {

    }

    //Use to set destination
    //private Vector3 SetDestination()
    //{
    //    return 
    //}

#endregion
}
