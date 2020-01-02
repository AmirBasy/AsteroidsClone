using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsR : MonoBehaviour
{
    public float alienSpawn;
    public float time;
    public Asteroid asteriod;
    public Alien alien;


    void SpawningFromR()
    {
        Instantiate(asteriod, new Vector3(33, 0, Random.Range(-10f, 10f)), Quaternion.identity);
    }

    void SpawningAlienFromU()
    {
        Instantiate(alien, new Vector3(7f, 0, 18f), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        alienSpawn = 0;
        time = 8;
    }

    // Update is called once per frame
    void Update()
    {

        time = time + Time.deltaTime;
        if (time > 10) { SpawningFromR(); }
        if (time > 10.001) { time = 0; }
        alienSpawn = alienSpawn + Time.deltaTime;
        if (alienSpawn > 15) { SpawningAlienFromU(); }
        if (alienSpawn > 15.001) { alienSpawn = 0; }

    }
}
