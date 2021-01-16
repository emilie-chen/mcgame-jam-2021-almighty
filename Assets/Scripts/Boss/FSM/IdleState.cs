using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : INPCState
{
    //TESTING
    //-----------
    Material testMat;
    float timer;
    //-----------

#region FSMMethods
    public INPCState EnterState(BossController npc)
    {
        testMat = npc.materials[0];
        timer = 0;
        return this;
    }

    public INPCState UpdateState(BossController npc)
    {
        npc.GetComponent<Renderer>().material = testMat;
        timer += Time.deltaTime;
        
        //TESTING
        //--------------------
        if (timer > 5)
        {
            return ExitState(npc.m_MoveState, npc);
        }
        //--------------------
        return this;
    }

    public INPCState ExitState(INPCState newState, BossController npc)
    {
        return newState.EnterState(npc);
    }

#endregion
}
