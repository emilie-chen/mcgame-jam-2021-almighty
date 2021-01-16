using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlames : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject fireball;
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnFlame() {
        Instantiate(smoke, spawnPoint.transform.position, Quaternion.identity);
        Instantiate(fireball, spawnPoint.transform.position, transform.rotation);
    }
}
