using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageToBossScript : MonoBehaviour
{

    private BossController boss;

    private void Start()
    {
        boss = GameObject.Find("boss").GetComponent<BossController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        boss.OnCollisionEnter(collision);
    }
}
