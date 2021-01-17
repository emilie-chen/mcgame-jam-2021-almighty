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
    public float MaxNpcCount
    {
        get; private set;
    }

    public static NPCManager Instance
    {
        get; private set;
    }

    public void NotifyCrashSite(Vector3 hitPosition)
    {
        //Debug.Log(hitPosition);
        IEnumerable<(NPC, Vector3, float)> npcsInRange;
        lock (this)
        {
            // find all NPCs within range
            npcsInRange =
               from npc in m_NpcSet
               where Vector3.Distance(npc.transform.position, hitPosition) <= NPC_RUNAWAY_RANGE
               select (npc.GetComponent<NPC>(), npc.transform.position, Vector3.Distance(npc.transform.position, hitPosition));
            Debug.Log(npcsInRange.Count());
        }
        foreach ((NPC npc, Vector3 npcPos, float distance) in npcsInRange)
        {
            Vector3 dirToRun = (npcPos - hitPosition).normalized;
            Debug.DrawRay(npc.transform.position, dirToRun * NPC_RUN_DIST, Color.green, 10.0f);
            Vector3 target = npcPos + dirToRun * NPC_RUN_DIST;
            target.y = STANDARD_NPC_HEIGHT;
            npc.DoDamage((int)((1.0f - distance / NPC_RUNAWAY_RANGE) * 10.0f));
            npc.Runaway(target);
            Debug.Log(npc.Target);
        }
    }

    private void UpdateNpcIndices()
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
            // report the new number to the game manager
            GameObject.Find("Game UI").GetComponent<GameManager>().populationPercentage = m_NpcSet.Count / (float)MaxNpcCount * 100.0f;
        }
    }

    void Start()
    {
        UpdateNpcIndices();
        MaxNpcCount = m_NpcSet.Count;
        Instance = this;
        InvokeRepeating(nameof(UpdateNpcIndices), 1.0f, 1.0f);
    }

    void Update()
    {

    }
}
