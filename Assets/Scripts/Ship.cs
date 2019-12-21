using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigibody;
    public GameObject shotReference;
    public float rotationVelocity = 1800;
    public float acceleration = 6000;
    public int Life = 3;

    Camera cam;

    void Rotate(float direction)
    {
        rigibody.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
        //transform.Rotate(Vector3.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Shot()
    {
        //spawn shot and set direction
        GameObject newShot = Instantiate(shotReference);
        newShot.transform.position = transform.position + transform.forward;

        newShot.GetComponent<Shot>().direction = transform.forward;
    }

    void Accelerate()
    {
        rigibody.AddForce(transform.forward * acceleration * Time.deltaTime);
        //transform.Translate(Vector3.forward * acceleration * Time.deltaTime);
    }

    void CrossScreen()
    {
        //from world point to viewport point
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        //if out of the screen, teleport to the other side
        if (screenPoint.x > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0, screenPoint.y, screenPoint.z));    //right to left
        }
        else if (screenPoint.x < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(1, screenPoint.y, screenPoint.z));    //left to right
        }
        else if (screenPoint.y > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 0, screenPoint.z));    //up to down
        }
        else if (screenPoint.y < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 1, screenPoint.z));    //down to up
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();

        CrossScreen();
    }
}
