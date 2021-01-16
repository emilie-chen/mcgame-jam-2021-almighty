using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



public class PlayerBehavior : MonoBehaviour
{
    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        // check normal
        Vector3 normal = collision.contacts[0].normal;
        if (Vector3.Angle(normal, Vector3.up) > 45.0f)
        {
            return;
        }    
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 3, transform.position, ForceMode.VelocityChange);
        }
    }
}
