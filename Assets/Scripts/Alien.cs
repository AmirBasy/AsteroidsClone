using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour 
{
    public int scoreToGive = 1000;
    public float speed = 3;
    public float rateOfFire = 1;
    public GameObject shotReference;

    Vector3 direction;
    float timerShot;

    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CrossScreen();

        Move();
        CheckShot();
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
                //add score
                gameManager.AddScore(scoreToGive);

                //destroy shot
                Destroy(other.gameObject);

                //destroy alien
                Die();
            }
        }

        //if hit player, add damage
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.AddDamage();
        }
    }

    public void CreateAlien()
    {
        //set position outside of the screen
        transform.position = gameManager.RandomPosition();

        //set size of the alien
        float size = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(size, size, size);

        //set random direction
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void CheckShot()
    {
        //check timer than shot
        if (timerShot < Time.time)
        {
            timerShot = Time.time + rateOfFire;

            Shot();
        }
    }

    void Shot()
    {
        //spawn shot
        GameObject newShot = Instantiate(shotReference);

        //and set it
        Vector3 position = transform.position - transform.up;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        bool isPlayerShot = false;

        newShot.GetComponent<Shot>().CreateShot(position, direction, isPlayerShot);
    }

    void CrossScreen()
    {
        //if out of the screen, teleport to the other side
        transform.position = gameManager.CrossScreen(transform.position, 1, 0);
    }

    void Die()
    {
        //reset in gameManager
        gameManager.canSpawnAlien = true;

        Destroy(gameObject);
    }

    public void Accelerate()
    {
        throw new System.NotImplementedException();
    }

    public void Rotate()
    {
        throw new System.NotImplementedException();
    }
}
