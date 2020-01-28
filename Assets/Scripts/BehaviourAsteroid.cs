using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAsteroid : MonoBehaviour
{
    public Camera cam;
    public Rigidbody asteroid;
    public Asteroids ast;
    int border;

    void CrossScreen()
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

    void Move()
    {
        

        if(border == 0)
        {
            int position = Random.Range(-9, 9);
            asteroid.AddForce(Vector3.right * 100 * Time.deltaTime);
        }

        if (border == 1)
        {
            int position = Random.Range(-11, 11);
            asteroid.AddForce(Vector3.forward * -100 * Time.deltaTime);
        }

        if (border == 2)
        {
            int position = Random.Range(-11, 11);
            asteroid.AddForce(Vector3.forward * 100 * Time.deltaTime);
        }

        if (border == 3)
        {
            int position = Random.Range(-9, 9);
            asteroid.AddForce(Vector3.right * -100 * Time.deltaTime);
        }

        
    }


    void Start()
    {
        //ast = FindObjectOfType<Asteroids>();
        cam = Camera.main;// FindObjectOfType<Camera>();
        border = ast.bordo;
    }

    void Update()
    {
        CrossScreen();
        //Move();
    }

}