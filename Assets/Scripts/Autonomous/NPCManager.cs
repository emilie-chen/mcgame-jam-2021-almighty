using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    private GameObject[] m_NpcList;

    void Start()
    {
        m_NpcList = GameObject.FindGameObjectsWithTag("NPC");
    }

    void Update()
    {

    }
}
