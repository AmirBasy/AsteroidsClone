using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Ship ActualShip;
    public GameObject ship;
    public int ActualScore = 0;
    public int activeAsteroids = 0;
    public int asteroidsToSpawn = 4;
    float randX = 0f, randZ = 0f;
    public GameObject asteroid;
    private GameObject[] asteroidList;
    public bool alive = true;
    public float timer = 6f;
    public bool shipCollider = true;
    



    public void Awake()
    {
        //ActualShip = FindObjectOfType<Ship>();
        
    }

    public void ScoreIncrease()
    {
        ActualScore += 10;
    }

    public void AsteroidCount()
    {
        asteroidList = GameObject.FindGameObjectsWithTag("Asteroid");
        activeAsteroids = asteroidList.Length;
    }

    public void AsteroidSpawn()
    {
        for (activeAsteroids = 0; activeAsteroids < asteroidsToSpawn; activeAsteroids++)
        {
            randX = 0;
            randZ = 0;
            while ((randX <= 8 && randX >= -8) && (randZ <= 7 && randZ >= -7))
            {
                randX = Random.Range(-15f, 15f);
                randZ = Random.Range(-7.5f, 7.5f);
            }
            Instantiate(asteroid, new Vector3(randX, 0, randZ), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360), 0)));
        }
        asteroidsToSpawn += 2;
    }

    public void LoseCondition()
    {
        if (ActualShip.life == 0) GoToEndMenu(3);
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu(int a)
    {
        SceneManager.LoadScene(a);
    }

    public void ShipDie()
    {
        if (alive == false && timer > 0f)
        {
            ship.SetActive(alive);
            ship.transform.position = new Vector3(0, 0, 0);
            ActualShip.life -= 1;
            timer = 0f;
            alive = true;


        }
    }
    public void ShipRespawn()
    {
        if (alive == true && timer > 2f && timer <= 5f) {
            ship.SetActive(alive);
            shipCollider = false;
            if (ship.GetComponent<Renderer>().enabled == true) ship.GetComponent<Renderer>().enabled = false;
            else ship.GetComponent<Renderer>().enabled = true;
        }
        if (alive == true && timer > 5f)
        {
            shipCollider = true;
            ship.GetComponent<Renderer>().enabled = true;
        }
    }
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShipDie();
        ShipRespawn();
        AsteroidCount();
        if (activeAsteroids <= 0)
        {
            AsteroidSpawn();
        }
        LoseCondition();
        timer += Time.deltaTime;
    }
}
