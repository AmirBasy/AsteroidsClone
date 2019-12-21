using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity = 50;
    public Vector3 direction = Vector3.forward;

    Camera cam;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit")
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit")
        {
            Die();
        }
    }
    */

    void Move()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void OutScreen()
    {
        //from world point to viewport point
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        //if out of the screen, destroy
        if (screenPoint.x > 1 || screenPoint.x < 0 || screenPoint.y > 1 || screenPoint.y < 0)
        {
            Die();
        }
    }

    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //move and check if out of the screen
        Move();

        OutScreen();
    }
}
