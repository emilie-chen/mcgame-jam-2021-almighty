using UnityEngine;
using System.Collections;

public class BuildingBehavior : MonoBehaviour
{
    public GameObject RubblePrefab;

    void Start()
    {
        GetComponent<DamagableEntity>().DeathHandler += self => ChangeModelToDamaged();
    }

    private void ChangeModelToDamaged()
    {
        GameObject rubble = Instantiate(RubblePrefab);
        Vector3 newPos = transform.position;
        newPos.y = 32.3f;
        rubble.transform.position = newPos;
        Destroy(gameObject);
        
    }

    void Update()
    {

    }
}
