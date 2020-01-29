using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed, lifeTime=1.5f;
    public Vector3 vector3;

    void LifeDecay()
    {
        lifeTime -= Time.deltaTime;
    }

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyAsteroid()
    {
       
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LifeDecay();
        if (lifeTime <= 0) Die();

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "collisionTop") transform.position += new Vector3(0, 0, -18);
        if (collision.gameObject.name == "collisionBottom") transform.position += new Vector3(0, 0, 18);
        if (collision.gameObject.name == "collisionLeft") transform.position += new Vector3(34, 0, 0);
        if (collision.gameObject.name == "collisionRight") transform.position += new Vector3(-34, 0, 0);

        if (collision.gameObject.tag == "Asteroid")
        {
            GameObject asteroid = collision.gameObject;
            asteroid.GetComponent<Asteroids>().Die();
            Die();
        }
    }
}
