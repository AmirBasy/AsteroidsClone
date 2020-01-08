using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public int bordo;
    float timer=0;
    

    int SpawnAsteroid()
    {
        int border = Random.Range(0, 4);

        if (border == 0)
        {
            int position = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(AsteroidPrefab, new Vector3(-12.5f, 0, position), transform.rotation);
        }

        if (border == 1)
        {
            int position = Random.Range(-11, 11);
            GameObject newAsteroid = Instantiate(AsteroidPrefab, new Vector3(position, 0, 9), transform.rotation);
        }

        if (border == 2)
        {
            int position = Random.Range(-11, 11);
            GameObject newAsteroid = Instantiate(AsteroidPrefab, new Vector3(position, 0, -9), transform.rotation);
        }

        if (border == 3)
        {
            int position = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(AsteroidPrefab, new Vector3(12.5f, 0, position), transform.rotation);
        }

        return border;

    }


    void Start()
    {
    }

    void Update()
    {
        timer = timer + Time.deltaTime;

        if(timer > 1)
        {
            bordo =SpawnAsteroid();
            timer = 0;
        }
       
    }
}
