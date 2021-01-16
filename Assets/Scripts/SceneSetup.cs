using UnityEngine;
using System.Collections;

/// <summary>
/// attach to the terrain
/// </summary>
public class SceneSetup : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = Vector3.down * 9.8f;
    }

}
