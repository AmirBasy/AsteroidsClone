using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{

    public GameObject time;
    public GameObject score;
    Gamemanager gameManager;
    public Image loselife;
    public Image loselife1;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Gamemanager>();

    }

    // Update is called once per frame
    void Update()
    {
        score.GetComponent<Text>().text = "Score: " + gameManager.score;

        time.GetComponent<Text>().text = "Time: " + gameManager.time.ToString("f1");

        if (gameManager.life == 2) { Destroy(loselife); }
        if (gameManager.life == 1) { Destroy(loselife1); }
    }

   
}

