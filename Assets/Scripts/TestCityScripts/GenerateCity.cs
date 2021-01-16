using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class GenerateCity : MonoBehaviour
{
 
    void Start()
    {
        GameObject buildingTemplate = GameObject.FindGameObjectWithTag("Building");
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        GameObject newBuilding = Instantiate(buildingTemplate);
                        newBuilding.transform.position = buildingTemplate.transform.position + new Vector3(i * 100 + x * 12, 0, j * 100 + y * 12);
                    }
                }
            }
        }
        NavMeshBuilder.BuildNavMesh();
    }

    void Update()
    {

    }
}
