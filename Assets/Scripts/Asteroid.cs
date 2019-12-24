using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int scoreToGive = 10;
    public int scoreDestroyed = 100;

    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        CrossScreen();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            Shot shot = other.GetComponentInParent<Shot>();

            //only if is a player shot (not alien)
            if (shot.playerShot)
            {

                //check size, than split or destroy
                if (transform.localScale.sqrMagnitude > 10)
                {
                    gameManager.AddScore(scoreToGive);

                    Vector3 shotDirection = shot.direction;
                    SplitAsteroid(shotDirection);
                }
                else
                {
                    gameManager.AddScore(scoreDestroyed);

                    Die();
                }

                //destroy shot
                Destroy(other.gameObject);
            }
        }
        
        //if hit player, add damage
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.AddDamage();
        }
    }

    void CrossScreen()
    {
        //if out of the screen, teleport to the other side
        transform.position = gameManager.CrossScreen(transform.position, 1.1f, -0.1f);
    }

    void SplitAsteroid(Vector3 shotDirection)
    {
        //get asteroids spawn to right and left from the directon of the shot
        Vector3 forward = shotDirection;
        Vector3 right = Vector3.Cross(forward, Vector3.up);
        Vector3 left = -right;

        //create two half asteroids
        CreateHalfAsteroid(right);
        CreateHalfAsteroid(left);

        //destroy this asteroid
        Die();
    }

    //call in GameManager, because is not a function for half asteroids, so it can't be in awake
    public void CreateAsteroid()
    {
        //add asteroid to the list
        gameManager.asteroids.Add(this.gameObject);

        //set position outside of the screen
        transform.position = gameManager.RandomPosition();

        //set size of the asteroid
        float size = 5;
        transform.localScale = new Vector3(size, size, size);

        //add force to rigidbody in random direction
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        float force = Random.Range(250, 300);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * force);
    }

    void CreateHalfAsteroid(Vector3 position)
    {
        //create half asteroids
        GameObject halfAsteroid = Instantiate(gameObject);

        //add asteroid to the list
        gameManager.asteroids.Add(halfAsteroid);

        //set positions
        halfAsteroid.transform.position = transform.position + position;

        //set scales
        halfAsteroid.transform.localScale = transform.localScale / 2;

        //add force to rigidbody
        Vector3 direction = position;
        float force = Random.Range(300, 500);
        Rigidbody rb = halfAsteroid.GetComponent<Rigidbody>();
        rb.AddForce(direction * force);
    }

    void Die()
    {
        //remove from list and destroy asteroid
        gameManager.asteroids.Remove(this.gameObject);

        Destroy(gameObject);
    }
}
