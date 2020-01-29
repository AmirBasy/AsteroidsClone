using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text Score;
    public Text Life;
    public Transform LifeToSpawn;
    public Manager gameManager;
    public int LifeDisplay;
    public int scoreDisplay;
    public GameObject ManagerGO;

    private void Awake()
    {
        gameManager = ManagerGO.GetComponent<Manager>();
        SetCurrentShipLife();
    }
    
    void SetCurrentShipLife()
    {
        LifeDisplay = gameManager.ActualShip.life;
    }

    void SetCurrentScore()
    {
        scoreDisplay = gameManager.ActualScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentShipLife();
        SetCurrentScore();
        Score.text = "SCORE: "+ scoreDisplay;
        Life.text = "LIFE: " + LifeDisplay;
    }
}
