using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int scoreTogive = 10;

    GameManager gameManager;
    Camera cam;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CrossScreen();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            //destroy shot
            Destroy(other.gameObject);

            //add score
            gameManager.AddScore(scoreTogive);

            //split or destroy
            if (transform.localScale.sqrMagnitude > 10)
            {
                SplitAsteroid();
            }
            else
            {
                Die();
            }
        }
        //if hit player, add damage
        else if(other.gameObject.CompareTag("Player"))
        {
            gameManager.AddDamage();
        }
    }

    void CrossScreen()
    {
        //from world point to viewport point
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        //if out of the screen, teleport to the other side
        if (screenPoint.x > 1.1f)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(-0.1f, screenPoint.y, screenPoint.z));    //right to left
        }
        else if (screenPoint.x < -0.1f)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(1.1f, screenPoint.y, screenPoint.z));    //left to right
        }
        else if (screenPoint.y > 1.1f)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, -0.1f, screenPoint.z));    //up to down
        }
        else if (screenPoint.y < -0.1f)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 1.1f, screenPoint.z));    //down to up
        }
    }

    void SplitAsteroid()
    {
        //get asteroids spawn to right and left from the directon of this one
        Vector3 forward = rb.velocity.normalized;
        Vector3 right = Vector3.Cross(forward, Vector3.up);
        Vector3 left = -right;

        //create two half asteroids
        CreateHalfAsteroid(right);
        CreateHalfAsteroid(left);

        //destroy this asteroid
        Die();
    }

    //call in GameManager
    public void CreateAsteroid()
    {
        //add asteroid to the list
        gameManager.asteroids.Add(this);

        //set position outside of the screen
        transform.position = RandomPosition();

        //set size of the asteroid
        float size = Random.Range(1f, 3f);
        transform.localScale = new Vector3(size, size, size);

        //add force to rigidbody in random direction
        Vector3 direction = Random.onUnitSphere;
        float force = Random.Range(250, 300);
        rb.AddForce(direction * force);
    }

    Vector3 RandomPosition()
    {
        //return random position outside of the screen

        //-1 == down, 0 = in screen, 1 = up
        int y = Random.Range(-1, 2);

        Vector3 screenPosition = Vector3.zero;

        if (y == 1)
        {
            //up, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = 1.5f;
        }
        else if (y == -1)
        {
            //down, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = -1.5f;
        }
        else
        {
            //random y
            screenPosition.y = Random.Range(0f, 1f);

            //0 == left, 1 == right
            screenPosition.x = Random.Range(0, 2) == 0 ? -1.5f : 1.5f;

        }

        //return to world point and set y to 0
        Vector3 worldPosition = cam.ViewportToWorldPoint(screenPosition);
        worldPosition.y = 0;

        return worldPosition;
    }

    void CreateHalfAsteroid(Vector3 position)
    {
        //add asteroid to the list
        gameManager.asteroids.Add(this);

        //create half asteroids
        GameObject halfAsteroid = Instantiate(gameObject);

        //set positions
        halfAsteroid.transform.position = transform.position + position;

        //set scales
        halfAsteroid.transform.localScale = transform.localScale / 2;

        //add force to rigidbody
        //Vector3 direction = Random.onUnitSphere;
        float force = Random.Range(150, 200);
        halfAsteroid.GetComponent<Rigidbody>().AddForce(position * force);
    }

    void Die()
    {
        //remove from list and destroy asteroid
        gameManager.asteroids.Remove(this);

        Destroy(gameObject);
    }
}
