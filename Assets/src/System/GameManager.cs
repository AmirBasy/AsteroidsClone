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

    [Header("Game Rules")]
    Score _score = new Score();
    public string[] _sceneName = new string[10];

    #region Unity Callbacks
    protected void Awake()
    {
        Score _score = new Score();
    }
    public void Start()
    {

        _playerPrefab.gameObject.transform.position = Vector3.zero;

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
       
        if(LevelFinished(2000))
        {
            //pause game
            //display victory
        }
     
    }
    #endregion
    #region LevelBuilding
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

    private void SpawnEnemyShip(Vector3 position)
    {
        GameObject _enemyShip = Instantiate(_enemyPrefab) as GameObject;
        _enemyShip.transform.position = position;
    }
    #endregion
    #region ScoreController
    public bool LevelFinished(int _pointsToNextLevel)
    {
        if (_score.GetScore() >= _pointsToNextLevel)
            return true;
        else return false;
    }
    #endregion

}


