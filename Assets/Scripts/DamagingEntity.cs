using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingEntity : MonoBehaviour
{
    private DamagingEntityProps m_Props;

    void Start()
    {
        m_Props = GetComponent<DamagingEntityProps>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(JsonUtility.ToJson(collision));
        // register this as an impact and let the NPCs run away from it
        NPCManager.Instance.NotifyCrashSite(collision.GetContact(0).point);
    }

    void Update()
    {
        
    }
}
