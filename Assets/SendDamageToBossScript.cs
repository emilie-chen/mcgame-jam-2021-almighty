using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageToBossScript : MonoBehaviour
{

    public DamagableEntity boss;

    private void Start()
    {
        boss = GameObject.Find("boss").GetComponent<DamagableEntity>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("COLLISION");
        boss.ReceiveChildCollision(collision);
    }
}
