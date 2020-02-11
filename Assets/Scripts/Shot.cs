using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity = 50;
    public Vector3 direction = Vector3.forward;


    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit")
        {
            Die();
        }
        if (other.gameObject.tag == "Asteroid")
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
        if (collision.gameObject.tag == "Asteroid")
        {
            Die();
        }
    }



    void Move()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    void SplitAsteroid()
    {

    }

    void DestroyAsteroid()
    {

    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
