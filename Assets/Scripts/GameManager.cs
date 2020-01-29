using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ship ActualShip;
    public List<Transform> Limit;
    public List<GameObject> Asteroids;
    public float spawnRate = 2f, spawnTime = 0f, margin, minSpawnAngle = -1.5f, maxSpawnAngle = 1.5f;
    public int ActualScore, ScoreToWin, border;
    public bool Pause = false;
    Transform offset;

    void Awake()
    {
        ActualShip = FindObjectOfType<Ship>();
    }
    
    void Update()
    {
        VictoryCondition();
        LoseCondition();
        if (Input.GetKey(KeyCode.Escape))
        {
            CheckPause();
        }
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnRate)
        {
            SpawnAsteroid();
            spawnTime = 0;
        }
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {

        }
    }

    public void LoseCondition()
    {
        if (ActualShip.life <= 1)
        {

        }
    }
    public void CheckPause()
    {
        if (Pause)
        {
            Pause = false;
            PauseGame();
        }
        else
        {
            Pause = true;
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (Pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void SpawnAsteroid()
    {
        border = Random.Range(0,2);
        GameObject newAsteroid = Asteroids[border];
        Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
        switch (border)
        {
            case 0:
                asteroidScript.health = 1;
                break;
            case 1:
                asteroidScript.health = 2;
                break;
            default:
                break;
        }
        border = Random.Range(0, 4);
        Transform spawnPoint = Limit[border];
        Vector3 spawnPosition = DetermineSpawnPosition(spawnPoint, asteroidScript);
        Instantiate(newAsteroid, spawnPosition, Quaternion.identity);
    }
    private Vector3 DetermineSpawnPosition(Transform spawnPoint, Asteroid script)
    {
        Vector3 positionOffset =  Vector3.zero , direction = Vector3.zero, directionOffset = Vector3.zero;
        float angle = Random.Range(minSpawnAngle, maxSpawnAngle);
        switch (border)
        {
            case 0:
                positionOffset = new Vector3(0, 0, -margin);
                direction = Vector3.back;
                directionOffset = new Vector3(angle, 0, 0);
                break;
            case 1:
                positionOffset = new Vector3(0, 0, margin);
                direction = Vector3.forward;
                directionOffset = new Vector3(angle, 0, 0);
                break;
            case 2:
                positionOffset = new Vector3(margin, 0, 0);
                direction = Vector3.right;
                directionOffset = new Vector3(0, 0, angle);
                break;
            case 3:
                positionOffset = new Vector3(-margin, 0, 0);
                direction = Vector3.left;
                directionOffset = new Vector3(0, 0, angle);
                break;
            default:
                break;
        }
        script.initialDirection = Vector3.Normalize(direction + directionOffset);
        return spawnPoint.position + positionOffset; 
    }
    //changes both x and z values to their respective opposites to spawn the object in the mirrored position
    public Vector3 Teleport(Transform pos, GameObject obj)
    {
        Vector3 tp = pos.position;
        switch (obj.name)
        {
            case "Limit_Up":
                tp.z = -20;
                break;
            case "Limit_Down":
                tp.z = 20;
                break;
            case "Limit_Sx":
                tp.x = 25;
                break;
            case "Limit_Dx":
                tp.x = -25;
                break;
            default:
                break;
        }
        return tp;
    }
}
