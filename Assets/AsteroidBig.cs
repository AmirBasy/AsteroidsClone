using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBig : MonoBehaviour
{
    public float velocity = 5;
    public Vector3 direction = Vector3.forward;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot")
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            Die();
        }
    }

    void Move()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
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
