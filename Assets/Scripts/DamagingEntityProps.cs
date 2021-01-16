using UnityEngine;
using System.Collections;
using System.Threading;

public class DamagingEntityProps : MonoBehaviour
{
    public int Damage;
    public int Knockback;
    private bool Enabled = true;

    private object m_Lock;

    private void Start()
    {
        m_Lock = new object();
    }

    public object ObtainLock()
    {
        return m_Lock;
    }

    public bool IsEnabled
    {
        get
        {
            lock (m_Lock)
            {
                return Enabled;
            }
        }
        set
        {
            lock (m_Lock)
            {
                Enabled = value;
            }
        }
    }
}
