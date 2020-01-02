using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{

    public DATA.Asteroid[] _asteroids;
    public DATA.AsteroidFragment[] _debris;

    public int _asteroidsCount;
    public int _debrisCount;

    public bool _enemySpawned = false;

    [Header("References")]
    public GameObject _playerPrefab;
    public GameObject _asteroidPrefab;
    public GameObject _enemyPrefab;

    public Camera gamecamera;

    [Header("Game Rules")]
    public int CurrentPoints;
    private int PointsToNextLevel;
    public Vector2 _screenLimit;

    private Vector3 _adjustedPostion;

    public void Awake()
    {

        

    }

    public void Start()
    {
        


        if(!SaveLoadManager.SaveGameExists())
        {
            BuildLevel();
            _playerPrefab.transform.position += new Vector3(50, 0, 0);
            SaveLoadManager.SaveData(this);
        }
        else
        {
            SaveLoadManager.LoadData(this);
        }

        //BuildLevel();

    }

    public void Update()
    {
        //<---Exausted Lifes?
        //<---control player position
        _adjustedPostion = _playerPrefab.transform.position;

        if(_playerPrefab.transform.position.x > _screenLimit.x)
        {
            _adjustedPostion.x = _screenLimit.y;
        }
        if (_playerPrefab.transform.position.z > _screenLimit.x)
        {
            
        }
        if (_playerPrefab.transform.position.x < _screenLimit.y)
        {
            
        }
        if (_playerPrefab.transform.position.y < _screenLimit.y)
        {
            
        }

        _playerPrefab.transform.position = _adjustedPostion;
        //<---Update Points
        //<---Update UI
    }
    
    private void BuildLevel()
    {
        for (int i=1; i<=20; i++)
        {
            SpawnAsteroid(new Vector3(Random.Range(-10, +10), Random.Range(-10, +10),0));
        }
    }
    
    private void SpawnAsteroid(Vector3 position)
    {

        GameObject asteroid = Instantiate(_asteroidPrefab) as GameObject;
        asteroid.transform.position = position;
    }

    private void SpawnDebris()
    {

        //<---Spawn Debris by hit

    }

    private void SpawnEnemyShip(Vector3 position)
    {
        GameObject _enemyShip = Instantiate(_enemyPrefab) as GameObject;
        _enemyShip.transform.position = position;
    }

    /*data*/

    public static void AdjustCameraToScreenResolution()
    {
        Debug.Log("set camera FOV to user resolution!");
    }

  

}


