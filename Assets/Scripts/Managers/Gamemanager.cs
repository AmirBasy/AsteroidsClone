using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int aliens;
    public int miniA;
    public int asteroids;
    public int bullets;
    public int score;
    public float time;
    public int life;
    bool stoptime;
    bool startGameplay;
    public bool pause;
    public AudioManager music;

    public static Gamemanager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null) { instance = this; } else { Destroy(gameObject); }

        time = 0;
        score = 0;
        life = 3;
        aliens = 0;
        miniA = 0;
        asteroids = 0;
        bullets = 0;
        stoptime = false;
        pause = false;
        music = FindObjectOfType<AudioManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "SampleScene") { startGameplay = true; }

        if (SceneManager.GetActiveScene().name == "SampleScene" & startGameplay == true)
        {

            time = 0;
            score = 0;
            life = 3;
            aliens = 0;
            miniA = 0;
            asteroids = 0;
            bullets = 0;
            stoptime = false;
            startGameplay = false;

        }


        if (life == 0) { SceneManager.LoadScene(2); life = -1; stoptime = true; }

        if (stoptime == false) { time = time + Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause(); 
        }

        
    }
    public void SetPause()
    {
        if (pause == false)
        {
            Time.timeScale = 0;
            pause = true;
            music.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
            music.GetComponent<AudioSource>().mute = false;
        }
    }

}
