using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    Gamemanager gameManager;
    public GameObject finalScore;
    public GameObject asteroid;
    public GameObject miniA;
    public GameObject alien;
    public GameObject bullets;
    public GameObject time;
    public GameObject accuracy;
    public float kia;
  
    

   
    // Start is called before the first frame update
    void Start()
    {

        gameManager = FindObjectOfType<Gamemanager>();

        kia = gameManager.aliens + gameManager.asteroids + gameManager.miniA;
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        finalScore.GetComponent<Text>().text = "FINAL SCORE: " + gameManager.score;
        asteroid.GetComponent<Text>().text = "ASTEROIDS DESTROYED: " + gameManager.asteroids;
        miniA.GetComponent<Text>().text = "SMALL ASTEROIDS DESTROYED: " + gameManager.miniA;
        alien.GetComponent<Text>().text = "ALIENS SHIPS DESTROYED: " + gameManager.aliens;
        bullets.GetComponent<Text>().text = "BULLETS FIRED: " + gameManager.bullets;
        
        accuracy.GetComponent<Text>().text = "ACCURACY: " + (kia/gameManager.bullets*100).ToString("F2") + "%" ;
        time.GetComponent<Text>().text = "TIME: " + gameManager.time.ToString("F1") + "S";
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
