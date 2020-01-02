using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsL : MonoBehaviour
{
    public float time;
    public Asteroid asteriod;

    void SpawningFromL()
    {
        Instantiate(asteriod, new Vector3(-33, 0, Random.Range(-10f, 10f)), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        time = 2;
    }

    // Update is called once per frame
    void Update()
    {

        time = time + Time.deltaTime;
        if (time > 7) { SpawningFromL(); }
        if (time > 7.001) { time = 0; }

    }
}
