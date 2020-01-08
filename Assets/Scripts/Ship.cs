using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigibody;
    public float rotationVelocity=45;
    public float acceleration;
    public GameObject shotReference;
    public int Life=3;
    public Camera cam;


    void Rotate(float direction)
    {
        rigibody.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Shot()
    {
        GameObject newShot = Instantiate(shotReference, transform.position, transform.localRotation);
    }

    void Accelerate()
    {
        rigibody.AddForce(transform.forward * acceleration * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Asteroid")
        {
            transform.position = Vector3.zero;
            Destroy(collision.gameObject);
            Life--;
        }
    }

    void CrossScreen()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);                                                             //funzione che permette di sapere se la nave si trova all'interno della camera

        if (screenPoint.x > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.x < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(1, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.y > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 0, screenPoint.z));
        }
        else if (screenPoint.y < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 1, screenPoint.z));
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);


    }


    
    void Start()
    {
        cam=FindObjectOfType<Camera>();      
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();
        CrossScreen();
    }
}
