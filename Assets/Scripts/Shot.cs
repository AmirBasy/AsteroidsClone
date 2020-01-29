using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity;
    public Vector3 direction = Vector3.forward;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Limit")
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
    
    void Update()
    {
        Move();
    }
}
