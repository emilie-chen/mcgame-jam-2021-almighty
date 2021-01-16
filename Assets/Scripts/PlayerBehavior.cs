using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.CompareTag("Terrain"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * 3, transform.position, ForceMode.VelocityChange);
            }
        }
    }
}
