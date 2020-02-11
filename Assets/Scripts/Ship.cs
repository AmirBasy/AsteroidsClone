using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigibody;
    public float rotationVelocity=45;
    public float acceleration;
    public GameObject shotReference;
    public int Life;

    private void Awake()
    {
    }
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

    void Die()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
     if (collision.gameObject.name == "Limit_UP") transform.position += new Vector3(0, 0, -24);
     if (collision.gameObject.name == "Limit_DX") transform.position += new Vector3(-45, 0, 0);
     if (collision.gameObject.name == "Limit_DOWN") transform.position += new Vector3(0, 0, 24);
     if (collision.gameObject.name == "Limit_SX") transform.position += new Vector3(45, 0, 0);
     if (collision.gameObject.name == "Limit_TOP") transform.position += new Vector3(45, 0, 24);
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
