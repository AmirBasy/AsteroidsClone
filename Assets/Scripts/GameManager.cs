using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ship ActualShip;
    public int ActualScore=0;
    public int ScoreToWin;


    public void Awake()
    {
        ActualShip = FindObjectOfType<Ship>();
    }


    private void Update()
    {
        WinCondition();
        LoseCondition();
    }

   
    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }


    public void WinCondition()
    {
        if(ActualScore >= ScoreToWin)
        {
            SceneManager.LoadScene(3);
        }
        
    }


    public void LoseCondition()
    {
        if (ActualShip.Life <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
