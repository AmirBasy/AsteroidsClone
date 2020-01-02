using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsDown : MonoBehaviour
{
    public float time;
    public Asteroid asteriod;

    void SpawningFromD()
    {
        Instantiate(asteriod, new Vector3(Random.Range(-15f, 15f), 0, -20f), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
      
        time = 1;
    }
    
    // Update is called once per frame
    void Update()
    {

        time = time + Time.deltaTime;
        if (time > 5) { SpawningFromD(); }
        if (time > 5.001) { time = 0; }

    }
}
