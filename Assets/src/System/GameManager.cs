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

    private bool _enemySpawned = false;

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
        
    }
    public void Start()
    {

        _playerPrefab.gameObject.transform.position = Vector3.zero;

        /*
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
        */

    }

    public void Update()
    {
        RecordTime();
        /*
        if(GetAsteroidsCount() == 0)
        {
            BuildLevel();
        }*/

        if(TimeIsOver(5) && !_enemySpawned)
        {
            GameObject _enemy = Instantiate(_enemyPrefab);
            _enemy.gameObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0.0f);

            _enemySpawned = true;

            ResetTime();

        }

        if(LevelFinished(2000))
        {
            //destroy big asteroids
           for(int i = 0; i<GetAsteroidsCount(); i++)
            {
                Destroy(Object.FindObjectsOfType<Asteroid>()[i].gameObject);
            }

           //destroy debris (small asteroids)
           for(int q = 0; q<GetDebrisCount(); q++)
            {
                Destroy(Object.FindObjectsOfType<Debris>()[q].gameObject);
            }

           //destroy enemy ship
           if(Object.FindObjectOfType<Enemy>())
            {
                Destroy(Object.FindObjectOfType<Enemy>().gameObject);
            }

           //set the score to zero
            ResetScore();

            //set player position & rotation
            GameObject aship = GameObject.Find("Ship").gameObject;

            aship.transform.position = new Vector3(0, 0, 0);
            aship.transform.rotation = Quaternion.Euler(-90, 0, 0);

            //rebuild level
            BuildLevel();
        }
     
    }
    #endregion
    #region LevelBuilding
    public void BuildLevel()
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
    public void AddScore(int amount)
    {
        _score.AddScore(amount);
    }

    public int GetScore()
    {
        return _score.GetScore();
    }
    public void ResetScore()
    {
        _score.ResetScore();
    }
    #endregion
    #region Timer
    private float _time;

    void RecordTime()
    {
        _time += Time.deltaTime;
    }
    void ResetTime()
    {
        _time = 0;
    }

    bool TimeIsOver(float time)
    {
        if (Mathf.FloorToInt(_time) == time)
            return true;
        else return false;
    }

    #endregion
    #region LevelManagement
    private int GetAsteroidsCount()
    {
        return Object.FindObjectsOfType<Asteroid>().Length;
    }

    private int GetDebrisCount()
    {
        return Object.FindObjectsOfType<Debris>().Length;
    }
    #endregion

}


