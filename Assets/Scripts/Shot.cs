using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity = 1000;
    public Vector3 direction;
    Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit")
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

       Die();
  
    }

    void Move()
    {

        //transform.Translate(direction * velocity * Time.deltaTime);
        rb.AddRelativeForce(direction * velocity * Time.deltaTime,ForceMode.Acceleration);
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
