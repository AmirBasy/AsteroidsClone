using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsUp : MonoBehaviour
{
    public float alienSpawn;
    public float time;
    public Asteroid asteriod;
   
  
    void SpawningFromU()
    {
        Instantiate(asteriod, new Vector3(Random.Range(-15f, 15f), 0, 18f), Quaternion.identity); //TODO, da fixare lo spawn dell'asteroide(in tutti i 4 Aspwner)
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {
      
        time = time+ Time.deltaTime;
        if (time > 9) { SpawningFromU(); }
        if (time > 9.001) { time = 0; }
       
    }
}
