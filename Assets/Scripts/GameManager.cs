using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int actualScore = 0;
    public int scoreToWin = 10000;
    public GameObject asteroidReference;
    public int numberAsteroids = 10;
    public GameObject alienReference;
    public float minTimeAlien = 10;
    public float maxTimeAlien = 30;

    [HideInInspector] public Ship actualShip;
    [HideInInspector] public List<GameObject> asteroids = new List<GameObject>();
    [HideInInspector] public bool canSpawnAlien = true;

    bool calledSpawnAsteroids = false;

    UIManager uiManager;
    Camera cam;

    void Awake()
    {
        //don't destroy, so we can see scores in end scene
        DontDestroyOnLoad(gameObject);

        //check for scene unloaded, so we can destroy when come back to gameplay
        SceneManager.sceneUnloaded += SceneUnloaded;

        actualShip = FindObjectOfType<Ship>();

        uiManager = FindObjectOfType<UIManager>();

        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        //update only in GamePlay - in other scenes we need only scores
        if (SceneManager.GetActiveScene().name != "Gameplay")
            return;

        //press to pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = Time.timeScale == 0;

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        CheckSpawnAsteroids();

        CheckSpawnAlien();

        VictoryCondition();
        LoseCondition();
    }

    private void SceneUnloaded(Scene scene)
    {
        //die when come back to gameplay from end scene
        if (scene.name != "Gameplay")
            Die();
    }

    void Die()
    {
        //disable so nobody find this gameManager with findObjectOfType
        gameObject.SetActive(false);

        //stop check for scene change
        SceneManager.sceneUnloaded -= SceneUnloaded;

        //than destroy
        Destroy(gameObject);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1; //if called from pause menù
        SceneManager.LoadScene("MainMenu");
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void PauseGame()
    {
        //ferma il tempo e fa apparire il menù di pausa
        Time.timeScale = 0;
        uiManager.PauseMenu(true);
    }

    public void ResumeGame()
    {
        //fa ripartire il tempo e sparire il menù di pausa
        Time.timeScale = 1;
        uiManager.PauseMenu(false);
    }

    void CheckSpawnAsteroids()
    {
        //when there are no asteroids
        if (asteroids.Count <= 0 && !calledSpawnAsteroids)
        {
            StartCoroutine(SpawnAsteroids());
        }
    }

    void CheckSpawnAlien()
    {
        //if can spawn alien
        if(canSpawnAlien)
        {
            StartCoroutine(SpawnAlien());
        }
    }

    void VictoryCondition()
    {
        //score >= win
        if (actualScore >= scoreToWin)
        {
            StopAllCoroutines();
            Win();
        }
    }

    void LoseCondition()
    {
        //life <= 0
        if (actualShip.life <= 0)
        {
            StopAllCoroutines();
            Lose();
        }
    }

    IEnumerator SpawnAsteroids()
    {
        calledSpawnAsteroids = true;

        //wait
        yield return new WaitForSeconds(1);

        //spawn asteroids
        for (int i = 0; i < numberAsteroids; i++)
        {
            GameObject go = Instantiate(asteroidReference);
            go.GetComponent<Asteroid>().CreateAsteroid();
        }

        calledSpawnAsteroids = false;
    }

    IEnumerator SpawnAlien()
    {
        canSpawnAlien = false;

        //wait random between min and max
        yield return new WaitForSeconds(Random.Range(minTimeAlien, maxTimeAlien));

        //spawn alien
        GameObject go = Instantiate(alienReference);
        go.GetComponent<Alien>().CreateAlien();
    }

    IEnumerator RespawnShip()
    {
        //disable ship and rigidbody
        actualShip.enabled = false;
        Rigidbody shipRb = actualShip.GetComponent<Rigidbody>();
        shipRb.velocity = Vector3.zero;
        shipRb.angularVelocity = Vector3.zero;

        //wait
        yield return new WaitForSeconds(0.5f);

        //make animation - rotate and minimize
        Transform shipTr = actualShip.transform;
        while (shipTr.transform.localScale.x > 0.1f)
        {
            shipTr.Rotate(Vector3.up * 360 * Time.deltaTime);
            shipTr.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            yield return null;
        }

        //reset animation
        shipTr.rotation = Quaternion.identity;
        shipTr.localScale = Vector3.one;

        //and respawn - invincible
        actualShip.transform.position = Vector3.zero;
        actualShip.invincible = true;
        actualShip.enabled = true;

        //remove invincible after few seconds
        yield return new WaitForSeconds(2);
        actualShip.invincible = false;
    }

    public void AddScore(int score)
    {
        //add score and update UI
        actualScore += score;
        uiManager.UpdateUI();
    }

    public void AddDamage()
    {
        //only if enabled and not invincible
        if (actualShip.enabled && !actualShip.invincible)
        {
            //life -1 and update UI
            actualShip.life -= 1;
            uiManager.SetCurrentShipLife();

            //disable ship and respawn after few seconds
            StartCoroutine(RespawnShip());
        }
    }

    public Vector3 CrossScreen(Vector3 position, float max, float min)
    {
        Vector3 newPosition = position;

        //from world point to viewport point
        Vector3 screenPoint = cam.WorldToViewportPoint(position);

        //if out of the screen, teleport to the other side
        if (screenPoint.x > max)
        {
            newPosition = cam.ViewportToWorldPoint(new Vector3(min, screenPoint.y, screenPoint.z));    //right to left
        }
        else if (screenPoint.x < min)
        {
            newPosition = cam.ViewportToWorldPoint(new Vector3(max, screenPoint.y, screenPoint.z));    //left to right
        }
        else if (screenPoint.y > max)
        {
            newPosition = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, min, screenPoint.z));    //up to down
        }
        else if (screenPoint.y < min)
        {
            newPosition = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, max, screenPoint.z));    //down to up
        }

        return newPosition;
    }

    public bool OutScreen(Vector3 position, float max, float min)
    {
        //from world point to viewport point
        Vector3 screenPoint = cam.WorldToViewportPoint(position);

        //if out of the screen, return true
        if (screenPoint.x > max || screenPoint.x < min || screenPoint.y > max || screenPoint.y < min)
        {
            return true;
        }

        //else return false
        return false;
    }

    public Vector3 RandomPosition()
    {
        //return random position outside of the screen

        //-1 == down, 0 = in screen, 1 = up
        int y = Random.Range(-1, 2);

        Vector3 screenPosition = Vector3.zero;

        if (y == 1)
        {
            //up, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = 1.5f;
        }
        else if (y == -1)
        {
            //down, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = -1.5f;
        }
        else
        {
            //random y
            screenPosition.y = Random.Range(0f, 1f);

            //0 == left, 1 == right
            screenPosition.x = Random.Range(0, 2) == 0 ? -1.5f : 1.5f;

        }

        //return to world point and set y to 0
        Vector3 worldPosition = cam.ViewportToWorldPoint(screenPosition);
        worldPosition.y = 0;

        return worldPosition;
    }
}
