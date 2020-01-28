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

        Vector3 targetPosition = Vector3.zero;

        if (border == 0) targetPosition= new Vector3(-12.5f, 0, Random.Range(-9, 9));
        if (border == 1) targetPosition= new Vector3(Random.Range(-11, 11) , 0, 9);
        if (border == 2) targetPosition= new Vector3(Random.Range(-11, 11), 0, -9);
        if (border == 3) targetPosition= new Vector3(12.5f, 0, Random.Range(-9, 9));

        float lunghezza = targetPosition.magnitude;
        Vector3 vettoreNormalizzato = targetPosition.normalized;

        GameObject newAsteroid = Instantiate(AsteroidPrefab, targetPosition, transform.rotation);
        newAsteroid.GetComponent<BehaviourAsteroid>().ast = this;
        newAsteroid.GetComponent<Rigidbody>().velocity = -targetPosition.normalized * 5;

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
