using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAsteroid : MonoBehaviour
{

    public Camera cam;
    public Rigidbody asteroid;
    Asteroids ast;
    int border;


    void CrossScreen()                                              //funzione che permette di sapere se l'asteroide si trova all'interno della camera
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        if (screenPoint.x > 1)
            Destroy(this.gameObject);

        else if (screenPoint.x < 0)
            Destroy(this.gameObject);

        else if (screenPoint.y > 1)
            Destroy(this.gameObject);

        else if (screenPoint.y < 0)
            Destroy(this.gameObject);

    }

    void move()                                                     //funzione per far muovere l'asteroide
    {
        border = ast.bordo;

        if(border == 0)
        {
            int position = Random.Range(-9, 9);
            asteroid.AddForce(Vector3.right * 10000 * Time.deltaTime);
        }

        if (border == 1)
        {
            int position = Random.Range(-11, 11);
            asteroid.AddForce(Vector3.forward * -10000 * Time.deltaTime);
        }

        if (border == 2)
        {
            int position = Random.Range(-11, 11);
            asteroid.AddForce(Vector3.forward * 10000 * Time.deltaTime);
        }

        if (border == 3)
        {
            int position = Random.Range(-9, 9);
            asteroid.AddForce(Vector3.right * -10000 * Time.deltaTime);
        }

        
    }


    private void Awake()
    {
        ast = FindObjectOfType<Asteroids>();
        cam = FindObjectOfType<Camera>();
    }

    void Start()
    {   
        move();
    }

    void Update()
    {
        CrossScreen();          
    }

}