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
        if ( Input.GetKey( KeyCode.Escape ) ) 
        {
            CheckPause();
        }
        //Using a support variable and the Time.DeltaTime function, determines how often to spawn a new asteroid
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnRate)
        {
            SpawnAsteroid();
            spawnTime = 0;
        }
    }

    #region Scene Management
    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {

        }
    }

    public void LoseCondition()
    {

    }

    #region Pause
    //TODO: Pause not functioning appropriately, needs ulterior checks and adjustments
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
    #endregion

    //Increases the score by the amount given by the asteroid, then checks if the victory condition is met
    public void GivePoints (int pointsGiven) 
    {
        ActualScore += pointsGiven;
        Debug.Log( string.Format( "Score: {0}", ActualScore ) );
        VictoryCondition();
    }

    #region Asteroid Spawn
    public void SpawnAsteroid()
    {
        //Determines which asteroid to spawn, large or little
        border = Random.Range(0,2);
        GameObject newAsteroid = Asteroids[border];
        Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
        //In which case, sets its life to the correct amount
        switch (border)
        {
            case 0:
                asteroidScript.health = 1;
                break;
            case 1:
                asteroidScript.health = 2;
                asteroidScript.points = 10;
                break;
            default:
                break;
        }
        //Then randomizes on which border it'll spawn
        border = Random.Range(0, 4);
        Transform spawnPoint = Limit[border];
        //Calls the function to establish the exact point in which it'll spawn the object
        Vector3 spawnPosition = DetermineSpawnPosition(spawnPoint, asteroidScript);
        Instantiate(newAsteroid, spawnPosition, Quaternion.identity);
    }
    //Receiving the transform or the selected border
    private Vector3 DetermineSpawnPosition(Transform spawnPoint, Asteroid script)
    {
        Vector3 positionOffset =  Vector3.zero , direction = Vector3.zero, directionOffset = Vector3.zero;
        float angle = Random.Range(minSpawnAngle, maxSpawnAngle);
        /*
        Based on the point of spawn, 
        It sets the direction of the vector that will later be added as a force,
        Adding a little offset too, avoiding spawning directly into the border,
        Thus risking to lose objects that remain outside of the gameplay zone
        */
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
        /*
        At last, sums up the direction and the angle offset and normalizes the vector
        And then the result is passed to the object that will be spawned,
        Giving it the direction vector towards which apply the force 
        */
        script.initialDirection = Vector3.Normalize(direction + directionOffset);
        //Returns the spawn point, adding the curresponding offset from the border
        return spawnPoint.position + positionOffset; 
    }
#endregion

    #region Teleport
    //Changes both x and z values to their respective opposites to spawn the object in the mirrored position
    //TODO: needs to be adjusted, using camera as a reference could be the better way
    public Vector3 Teleport(Transform pos, GameObject obj)
    {
        //Based on the barrier which the object collides with, it teleports on the opposite side of the screen
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
    #endregion
}
