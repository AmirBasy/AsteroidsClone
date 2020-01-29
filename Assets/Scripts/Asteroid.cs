using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody rbod;
    GameManager Gm;
    public float velocity = 1500f;
    public Vector3 initialDirection;
    public int health = 1;

    void Start()
    {
        Gm = FindObjectOfType<GameManager>();
        rbod.AddForce(initialDirection * velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Shot"))
        {
            health--;
            CheckHealth();
        }else if (collision.gameObject.tag == "Limit")
        {
            this.transform.position = Gm.Teleport(this.transform, collision.gameObject);
        }
    }

    void Split()
    {
        Instantiate(Gm.Asteroids[0], transform.position, transform.localRotation);
        Instantiate(Gm.Asteroids[0], transform.position, transform.localRotation);
        Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void CheckHealth()
    {
        switch (health)
        {
            case 1:
                Split();
                break;
            case 0:
                Die();
                break;
            default:
                break;
        }
    }
}
