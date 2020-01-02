using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Ship ship;
    public float speed;
    public Vector3 direction;
   
    //void Rotate()
    //{  this.transform.rotation = ship.transform.rotation ; }
     
    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime );
    }
  
    void Start()
    {
        ship = FindObjectOfType<Ship>();
        direction = ship.transform.forward;
        
    }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    void SplitAsteroid()
    {

    }

    void DestroyAsteroid()
    {

    }

    void Die()
    {

    }
}
