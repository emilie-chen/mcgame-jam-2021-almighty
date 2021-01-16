using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NPCManager : MonoBehaviour
{
    private ISet<GameObject> m_NpcSet;
    public const float NPC_RUNAWAY_RANGE = 20.0f;
    public const float NPC_RUN_DIST = 60.0f;
    public const float STANDARD_NPC_HEIGHT = 30.0f;

    public static NPCManager Instance
    {
        get; private set;
    }

    public void NotifyCrashSite(Vector3 hitPosition)
    {
        //Debug.Log(hitPosition);
        IEnumerable<(NPC, Vector3)> npcsInRange;
        lock (this)
        {
            // find all NPCs within range
            npcsInRange =
               from npc in m_NpcSet
               where Vector3.Distance(npc.transform.position, hitPosition) <= NPC_RUNAWAY_RANGE
               select (npc.GetComponent<NPC>(), npc.transform.position);
            Debug.Log(npcsInRange.Count());
        }
        foreach ((NPC npc, Vector3 npcPos) in npcsInRange)
        {
            Vector3 dirToRun = (npcPos - hitPosition).normalized;
            Debug.DrawRay(npc.transform.position, dirToRun * NPC_RUN_DIST, Color.green, 10.0f);
            Vector3 target = npcPos + dirToRun * NPC_RUN_DIST;
            target.y = STANDARD_NPC_HEIGHT;
            npc.Runaway(target);
            Debug.Log(npc.Target);
        }
    }

    void Start()
    {
        lock (this)
        {
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
            m_NpcSet = new HashSet<GameObject>();
            foreach (GameObject npc in npcs)
            {
                npc.GetComponent<DamagableEntity>().DeathHandler += npc => m_NpcSet.Remove(npc);
                m_NpcSet.Add(npc);
            }
        }
        Instance = this;
    }

    void Update()
    {

    }
}
