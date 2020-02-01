using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int actualScore = 0;
    public int scoreToWin = 10000;
    public GameObject asteroidReference;
    public int numberAsteroids = 10;
    public GameObject alienReference;
    public float minTimeAlien = 10;
    public float maxTimeAlien = 30;

    [HideInInspector] public Ship actualShip;
    [HideInInspector] public List<GameObject> asteroids = new List<GameObject>();
    
    bool canSpawnAlien;

    AudioManager audioManager;
    UiManager uiManager;
    Camera cam;

    Coroutine spawnAsteroids, spawnAlien, respawnShip;

    void Awake()
    {
        CheckGameManager();
    }

    private void Update()
    {
        //update only in GamePlay - in other scenes we need only scores
        if (SceneManager.GetActiveScene().name != "Gameplay")
            return;

        //press to pause or resume game (can't set pause when ship destroyed)
        if (Input.GetKeyDown(KeyCode.Escape) && actualShip.enabled)
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

    void CheckGameManager()
    {
        if(instance != null)
        {
            //if there is already a gameManager, set his defaults and destroy this one
            instance.SetDefaults();
            Destroy(this.gameObject);
        }
        else
        {
            //if this is the unique gameManager, set default values
            instance = this;

            SetDefaults();
        }
    }

    void SetDefaults()
    {
        //don't destroy, so we can see scores in end scene
        DontDestroyOnLoad(gameObject);

        audioManager = GetComponent<AudioManager>();

        actualShip = FindObjectOfType<Ship>();
        uiManager = FindObjectOfType<UiManager>();
        cam = Camera.main;

        actualScore = 0;
        asteroids.Clear();
        canSpawnAlien = true;

        //create limits
        CreateLimits();
    }

    #region scene Control

    void ResetValueChangeScene()
    {
        StopCoroutines();
        instance.audioManager.ChangeGameMusic(false);
    }

    void StopCoroutines()
    {
        StopAllCoroutines();

        spawnAsteroids = null;
        spawnAlien = null;
        respawnShip = null;
    }

    public void GoToMenu()
    {
        ResetValueChangeScene();
        Time.timeScale = 1; //if called from pause menù
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameplay()
    {
        ResetValueChangeScene();
        SceneManager.LoadScene("Gameplay");
    }

    public void Win()
    {
        ResetValueChangeScene();
        SceneManager.LoadScene("WinScene");
    }

    public void Lose()
    {
        ResetValueChangeScene();
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
        if (asteroids.Count <= 0 && spawnAsteroids == null)
        {
            spawnAsteroids = StartCoroutine(SpawnAsteroids());
        }
    }

    void CheckSpawnAlien()
    {
        //if can spawn alien
        if(canSpawnAlien && spawnAlien == null)
        {
            spawnAlien = StartCoroutine(SpawnAlien());
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
        //don't create limits, when there isn't the ship (you are not in gameplay scene)
        if (actualShip == null) return;

        //get size of the walls
        float depthScreen = cam.WorldToViewportPoint(actualShip.transform.position).z;
        Vector3 size = GetScale(depthScreen);

        float movementX = size.x / 2;
        float movementZ = size.z / 2;

        CreateWall(new Vector3(1, 0.5f, depthScreen), size, new Vector3(movementX, 0, 0));     //right
        CreateWall(new Vector3(0, 0.5f, depthScreen), size, new Vector3(-movementX, 0, 0));    //left
        CreateWall(new Vector3(0.5f, 1, depthScreen), size, new Vector3(0, 0, movementZ));     //up
        CreateWall(new Vector3(0.5f, 0, depthScreen), size, new Vector3(0, 0, -movementZ));    //down
    }

    Vector3 GetScale(float depth)
    {
        //get size for the wall from the screen width and height
        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 0, depth));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(2, 2, depth));

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
        //wait
        yield return new WaitForSeconds(1);

        //spawn asteroids
        SpawnObject(asteroidReference, numberAsteroids, GetSize(true), GetSpeed(true), GetSoundFunction(true));

        spawnAsteroids = null;
    }

    IEnumerator SpawnAlien()
    {
        canSpawnAlien = false;

        //wait random between min and max
        yield return new WaitForSeconds(Random.Range(minTimeAlien, maxTimeAlien));
        
        //spawn alien
        SpawnObject(alienReference, 1, GetSize(false), GetSpeed(false), GetSoundFunction(false));

        //change game music
        audioManager.ChangeGameMusic(true);

        spawnAlien = null;
    }

    void SpawnObject(GameObject prefab, int numberOfObjects, Vector3 size, float speed, System.Action<AudioClip> soundFunction)
    {
        //instantiate and set
        for(int i = 0; i < numberOfObjects; i++)
        {
            GameObject go = Instantiate(prefab);
            go.GetComponent<ICreation>().Create(RandomPosition(), size, GetDirection(), speed, soundFunction);
        }
    }

    Vector3 RandomPosition()
    {
        //return random position outside of the screen

        //-1 == down, 0 = in screen, 1 = up
        int y = Random.Range(-1, 2);

        Vector3 screenPosition = Vector3.zero;

        if (y == 1)
        {
            //up, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = 1.01f;
        }
        else if (y == -1)
        {
            //down, random x
            screenPosition.x = Random.Range(0f, 1f);
            screenPosition.y = -1.01f;
        }
        else
        {
            //0 == left, 1 == right
            screenPosition.x = Random.Range(0, 2) == 0 ? -1.01f : 1.01f;

            //random y
            screenPosition.y = Random.Range(0f, 1f);
        }

        //return to world point and set y to 0
        Vector3 worldPosition = cam.ViewportToWorldPoint(screenPosition);
        worldPosition.y = 0;

        return worldPosition;
    }

    Vector3 GetSize(bool isAsteroid)
    {
        if (isAsteroid)
            return new Vector3(5, 5, 5);
        else
        {
            float size = Random.Range(1f, 1.5f);
            return new Vector3(size, size, size);                
        }
    }

    Vector3 GetDirection()
    {
        //return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        Vector2 dir = Random.insideUnitCircle.normalized;

        return new Vector3(dir.x, 0, dir.y);
    }

    float GetSpeed(bool isAsteroid)
    {
        if (isAsteroid)
            return Random.Range(250, 300);
        else
            return 3;
    }

    System.Action<AudioClip> GetSoundFunction(bool isAsteroid)
    {
        if (isAsteroid) 
            return audioManager.GetAsteroidFunction();
        else 
            return audioManager.GetAlienFunction();
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

        respawnShip = null;
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
        if(actualShip != null)
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
            actualShip.OnPlayerSound(actualShip.sound_shipDestroy);

            uiManager.SetCurrentShipLife();

            //disable ship and respawn after few seconds
            if(respawnShip == null)
                respawnShip = StartCoroutine(RespawnShip());
        }
    }

    public void DestroyAlien()
    {
        canSpawnAlien = true;
        audioManager.ChangeGameMusic(false);
    }

    public void GetPlayerSoundFunction()
    {
        actualShip.OnPlayerSound = audioManager.GetPlayerFunction();
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

    #endregion

}
