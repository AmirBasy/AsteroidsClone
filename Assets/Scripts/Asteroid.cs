using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    GameManager GM;
    public Rigidbody rigbod;
    public float velocity = 1500f;
    public Vector3 initialDirection;
    public int health = 1;
    public int points = 20;

    //Finds reference of the GameManager and gives the object the initial thrust
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        rigbod.AddForce(initialDirection * velocity);
    }

    /*
    Verifies which object it collided with and acts accordingly,
    Taking damage if it's the shot,
    Calling the GameManager's function to teleport if it's one of the limits
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Shot"))
        {
            health--;
            CheckHealth();
        }else if (collision.gameObject.tag == "Limit")
        {
            this.transform.position = GM.Teleport(this.transform, collision.gameObject);
        }
    }

    /*
    Instatiate a number of little asteroids that vary form 2 to 3 (could be modified), 
    And then destroys the large asteroid
    */
    void Split()
    {
        for(int i = 0; i < Random.Range(2, 4); i++ ) 
        {
            Instantiate(GM.Asteroids[0], transform.position, transform.localRotation);
        }
        Die();
    }

    //Destroys the object
    void Die()
    {
        Destroy(this.gameObject);
    }

    /*  
    Verifies the current health of the asteroid, 
    On 1 it splits, 
    On 0 it destroys itself, 
    Telling the GameManager how many points are awarded 
    */
    void CheckHealth()
    {
        if ( health == 1 )
        {
            GM.GivePoints( points );
            Split();
        }else 
        {
            GM.GivePoints( points );
            Die();
        }
    }
}
