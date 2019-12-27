using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandle : MonoBehaviour
{
    public AsteroidData asteroid;
    void Start()
    {
        if(string.IsNullOrEmpty(asteroid.id))
        {
            asteroid.id = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongTimeString() + Random.Range(0, int.MaxValue).ToString(); 

            
        }

        
    }

 
}
