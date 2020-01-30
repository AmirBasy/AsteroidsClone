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

    UiManager uiManager;
    Camera cam;

    void Awake()
    {
        //don't destroy, so we can see scores in end scene
        DontDestroyOnLoad(gameObject);

        //check for scene unloaded, so we can destroy when come back to gameplay
        SceneManager.sceneUnloaded += SceneUnloaded;

        actualShip = FindObjectOfType<Ship>();
        uiManager = FindObjectOfType<UiManager>();
        cam = FindObjectOfType<Camera>();

        //create limits
        CreateLimits();
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

    #region scene Control

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

    #region pause menu

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

    #endregion

    #endregion

    #region update checks

    #region spawn

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

    #endregion

    #region conditions endgame

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

    #endregion

    #endregion

    #region private API

    #region limits

    void CreateLimits()
    {
        float depthScreen = cam.WorldToViewportPoint(actualShip.transform.position).z;
        Vector3 size = GetScale(depthScreen);

        float movementX = size.x / 2;
        float movementZ = size.z / 2;

        CreateWall(new Vector3(1, 0.5f, depthScreen), size, new Vector3(movementX, 0, 0));      //right
        CreateWall(new Vector3(0, 0.5f, depthScreen), size, new Vector3(-movementX, 0, 0));     //left
        CreateWall(new Vector3(0.5f, 1, depthScreen), size, new Vector3(0, 0, movementZ));     //up
        CreateWall(new Vector3(0.5f, 0, depthScreen), size, new Vector3(0, 0, -movementZ));    //down
    }

    Vector3 GetScale(float depth)
    {
        //get size for the wall from the screen width and height
        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 0, depth));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 2, depth));

        Vector3 size = right - left;

        return new Vector3(size.x, 1, size.z);
    }

    void CreateWall(Vector3 viewportPoint, Vector3 size, Vector3 movement)
    {
        //instantiate, move and set size
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = cam.ViewportToWorldPoint(viewportPoint);
        wall.transform.localScale = size;
        wall.transform.position += movement;

        //set trigger and tag
        wall.GetComponent<BoxCollider>().isTrigger = true;
        wall.tag = "Limit";
    }

    #endregion

    #region spawn

    IEnumerator SpawnAsteroids()
    {
        calledSpawnAsteroids = true;

        //wait
        yield return new WaitForSeconds(1);

        //spawn asteroids
        SpawnObject(asteroidReference, numberAsteroids);

        calledSpawnAsteroids = false;
    }

    IEnumerator SpawnAlien()
    {
        canSpawnAlien = false;

        //wait random between min and max
        yield return new WaitForSeconds(Random.Range(minTimeAlien, maxTimeAlien));

        //spawn alien
        SpawnObject(alienReference, 1);
    }

    void SpawnObject(GameObject prefab, int numberOfObjects)
    {
        //instantiate and set
        for(int i = 0; i < numberOfObjects; i++)
        {
            GameObject go = Instantiate(prefab);
            go.GetComponent<ICreation>().Create();
        }
    }

    #endregion

    #region respawn ship

    IEnumerator RespawnShip()
    {
        //disable ship and rigidbody
        Disable(actualShip);

        //wait
        yield return new WaitForSeconds(0.5f);

        //make animation - rotate and minimize
        Transform shipTr = actualShip.transform;
        while (shipTr.transform.localScale.x > 0.1f)
        {
            Animation(shipTr);

            yield return null;
        }

        //reset animation
        ResetAnimation(shipTr);

        //and respawn - invincible
        SetInvincible(true);
        Respawn(actualShip, Vector3.zero);

        //remove invincible after few seconds
        yield return new WaitForSeconds(2);
        SetInvincible(false);
    }

    void Disable(MonoBehaviour script)
    {
        //disable script and stop rigidbody
        script.enabled = false;

        Rigidbody scriptRB = script.GetComponent<Rigidbody>();
        scriptRB.velocity = Vector3.zero;
        scriptRB.angularVelocity = Vector3.zero;
    }

    void Animation(Transform tr)
    {
        //animation - rotate and minimize
        tr.Rotate(Vector3.up * 360 * Time.deltaTime);
        tr.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
    }

    void ResetAnimation(Transform tr)
    {
        tr.rotation = Quaternion.identity;
        tr.localScale = Vector3.one;
    }

    void Respawn(MonoBehaviour script, Vector3 position)
    {
        script.transform.position = position;
        script.enabled = true;
    }

    void SetInvincible(bool invincible)
    {
        actualShip.invincible = invincible;
    }

    #endregion

    #endregion

    #region public API

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

    #region utility

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
            screenPosition.y = 1.1f;
        }
        else if (y == -1)
        {
            //down, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = -1.1f;
        }
        else
        {
            //0 == left, 1 == right
            screenPosition.x = Random.Range(0, 2) == 0 ? -1.1f : 1.1f;

            //random y
            screenPosition.y = Random.Range(0f, 1f);
        }

        //return to world point and set y to 0
        Vector3 worldPosition = cam.ViewportToWorldPoint(screenPosition);
        worldPosition.y = 0;

        return worldPosition;
    }

    #endregion

    #endregion

}
