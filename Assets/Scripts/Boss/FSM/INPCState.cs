using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCState
{
    // Enter and initialize desired state
    INPCState EnterState(BossController npc);

    // Update core state logic
    INPCState UpdateState(BossController npc);

    // Exit towards new state
    INPCState ExitState(INPCState newState, BossController npc);

}
