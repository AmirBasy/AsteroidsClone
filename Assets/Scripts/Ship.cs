using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float rotationSpeed;
    public float acceleration;
    public GameObject shot;
    public Rigidbody rigidBody;
    public int life;
    public GameObject manager;
    public Manager managerClass;

    void Rotate(int verso)
    {
        rigidBody.AddTorque(Vector3.up * verso * rotationSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.up * verso * rotationSpeed * Time.deltaTime);
    }

    void Shot()
    {
        Instantiate(shot, transform.position + transform.forward * 0.5f, transform.rotation);
    }

    void Accelerate()
    {
        rigidBody.AddForce(transform.forward * acceleration * Time.deltaTime);
        //transform.Translate(Vector3.forward * acceleration * Time.deltaTime);
    }

    void Die()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();
    }
    private void OnTriggerEnter(Collider collision)    {
        if (collision.gameObject.name == "collisionTop") transform.position += new Vector3(0, 0, -18);
        if (collision.gameObject.name == "collisionBottom") transform.position += new Vector3(0, 0, 18);
        if (collision.gameObject.name == "collisionLeft") transform.position += new Vector3(34, 0, 0);
        if (collision.gameObject.name == "collisionRight") transform.position += new Vector3(-34, 0, 0);
        if (collision.gameObject.tag == "Asteroid" && managerClass.shipCollider == true)
        {
            managerClass.alive=false;
        }
    }
}
