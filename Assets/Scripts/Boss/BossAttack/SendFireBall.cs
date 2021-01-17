using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFireBall : SpawnFlames
{
    private Vector3 m_Target;
    Quaternion m_Dir;

    public Vector3 SetTarget(Vector3 newTarget)
    {
        m_Target = newTarget;
        return m_Target;
    }

    public float GetTargetDistSqr()
    {
        float dist;
        Vector3 offset = m_Target - transform.position;
        dist = offset.sqrMagnitude;

        return dist;
    }

    private Quaternion SetDir(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
        return rot;
    }

    public override void SpawnFlame()
    {
        Instantiate(smoke, spawnPoint.transform.position, SetDir(m_Target));
        Instantiate(fireball, spawnPoint.transform.position, SetDir(m_Target));
    }

    public float GetProjectileSpeed()
    {
        return fireball.GetComponent<FireBall>().speed;
    }
}
