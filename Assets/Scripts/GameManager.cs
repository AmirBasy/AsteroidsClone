using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int ActualScore = 0;
    public int ScoreToWin = 100;
    public GameObject asteroidReference;
    public float minTimeAsteroid = 5f;
    public float maxTimeAsteroid = 8f;

     [HideInInspector] public Ship ActualShip;
    [HideInInspector] public List<Asteroid> asteroids = new List<Asteroid>();

    UiManager uiManager;
    float timer;

    public void Awake()
    {
        ActualShip = FindObjectOfType<Ship>();

        uiManager = FindObjectOfType<UiManager>();
    }

    private void Update()
    {
        //spawn asteroids
        SpawnAsteroid();

        //check end game
        VictoryCondition();
        LoseCondition();
    }

    void SpawnAsteroid()
    {
        //sometimes
        if (timer < Time.time && asteroids.Count < 10)
        {
            timer = Time.time + Random.Range(minTimeAsteroid, maxTimeAsteroid);

            //spawn asteroid
            GameObject go = Instantiate(asteroidReference);
            go.GetComponent<Asteroid>().CreateAsteroid();
        }
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void Lose()
    {
        SceneManager.LoadScene("LoseScene");
    }

    void VictoryCondition()
    {
        //score >= win
        if (ActualScore >= ScoreToWin)
        {
            Win();
        }
    }

    void LoseCondition()
    {
        //life <= 0
        if (ActualShip.Life <= 0)
        {
            Lose();
        }
    }

    public void AddScore(int score)
    {
        //add score and update UI
        ActualScore += score;

        uiManager.UpdateUI();
    }

    public void AddDamage()
    {
        if (ActualShip.enabled)
        {
            //toglie una vita alla nave e aggiorna UI
            ActualShip.Life -= 1;

            uiManager.SetCurrentShipLife();

            //disable ship and respawn after few seconds
            StartCoroutine(RespawnShip());
        }
    }

    IEnumerator RespawnShip()
    {
        //disable ship and rigidbody
        ActualShip.enabled = false;
        Rigidbody shipRb = ActualShip.GetComponent<Rigidbody>();
        shipRb.velocity = Vector3.zero;
        shipRb.angularVelocity = Vector3.zero;

        //wait
        yield return new WaitForSeconds(1f);

        //and respawn
        ActualShip.transform.position = Vector3.zero;
        ActualShip.enabled = true;
    }
}
