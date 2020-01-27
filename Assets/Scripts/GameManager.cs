using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ship ActualShip;
    public int ActualScore;
    public int ScoreToWin;
    public Transform prefab;
    public UiManager UI;
    GameObject [] Asteroid;
    public GameObject AsteroidPrefab;


    public void Awake()
    {
        ActualShip = FindObjectOfType<Ship>();
        Asteroid = new GameObject[8];
        SpawnAsteroid();
    }

    private void Update()
    {
        VictoryCondition();
        LoseCondition();
    }
    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void EndMenu()
    {
        SceneManager.LoadScene(0);
    }
       

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {
            for (int i = 0; i < Object.FindObjectsOfType<Asteroids>().Length; i++)
            {
                Destroy(Object.FindObjectsOfType<Asteroids>()[i].gameObject);
            }

            Object.FindObjectOfType<Ship>().gameObject.transform.position = new Vector3(0, 0, 0);
            SpawnAsteroid();
            ActualScore = 0;
            EndMenu();
        }
    }

    public void LoseCondition()
    {
        if (ActualShip.Life <= 0)
        {
            
            for (int i = 0; i < Object.FindObjectsOfType<Asteroids>().Length; i++)
            {
                Destroy(Object.FindObjectsOfType<Asteroids>()[i].gameObject);
            }
                EndMenu();
        }
    }


    public void SpawnAsteroid()
    {
        for(int i = 0; i < 8; i++)
        {
            Asteroid[i] = GameObject.Instantiate(AsteroidPrefab);
            Asteroid[i].gameObject.transform.position = new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10,10));
           
        }

    }
}
