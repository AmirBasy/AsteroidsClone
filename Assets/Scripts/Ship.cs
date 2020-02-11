using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigibody;
    public float rotationVelocity=45;
    public float acceleration;
    public GameObject shotReference;
    public GameObject lifePrefab0, lifePrefab1, lifePrefab2;
    int playerLife=3;

    void Rotate(float direction)
    {
        rigibody.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
        //transform.Rotate(Vector3.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Shot()
    {
        GameObject newShot = Instantiate(shotReference, transform.position, transform.localRotation);
    }

    void Accelerate()
    {
        rigibody.AddForce(transform.forward * acceleration * Time.deltaTime);
        //transform.Translate(Vector3.forward * acceleration * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider collision)
    {
     if (collision.gameObject.name == "Limit_UP") transform.position += new Vector3(0, 0, -24);
     if (collision.gameObject.name == "Limit_DX") transform.position += new Vector3(-45, 0, 0);
     if (collision.gameObject.name == "Limit_DOWN") transform.position += new Vector3(0, 0, 24);
     if (collision.gameObject.name == "Limit_SX") transform.position += new Vector3(45, 0, 0);
     if (collision.gameObject.name == "Limit_TOP") transform.position += new Vector3(45, 0, 24);
     
    if (collision.gameObject.tag == "Asteroid")
        {
            playerLife -= 1;
            if (playerLife>=2)
            {
                Destroy(lifePrefab0.gameObject);
            }
            if (playerLife>=1)
            {
                Destroy(lifePrefab1.gameObject);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.tag == "Asteroid")
    {
            playerLife -= 1;
        if (playerLife>=2)
        {
            Destroy(lifePrefab0.gameObject);
        }
        if (playerLife>=1)
        {
            Destroy(lifePrefab1.gameObject);
        }
    }
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();
    }
}
