using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ship ActualShip;
    public GameObject ship;
    public int ActualScore = 0;
    public int ScoreToWin = 100;
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
        ActualShip = FindObjectOfType<Ship>();
    }

    private void Update()
    {
        VictoryCondition();
        AsteroidCount();
        if (activeAsteroids <= 0)
        {
            AsteroidSpawn();
        }
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


    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {
        SceneManager.LoadScene(4);
        }
    }
   
    public void QuitGame()
    {
    Debug.Log("QUIT!");
    Application.Quit();
    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
