using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    private ISet<GameObject> m_NpcSet;

    void Start()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        m_NpcSet = new HashSet<GameObject>();
        //m_NpcSet.
    }

    void Update()
    {

    }
}
