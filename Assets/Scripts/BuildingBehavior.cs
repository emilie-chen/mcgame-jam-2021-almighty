using UnityEngine;
using System.Collections;

public class BuildingBehavior : MonoBehaviour
{
    void Start()
    {
        GetComponent<DamagableEntity>().DeathHandler += self => ChangeModelToDamaged();
    }

    private void ChangeModelToDamaged()
    {

    }

    void Update()
    {

    }
}
