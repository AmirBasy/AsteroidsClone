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

   
    public void GoToGameplay()                                                  //funzione per andare alla scena di gameplay
    {
        SceneManager.LoadScene(1);
    }

    public void WinCondition()                                                  //funzione per andare alla scena di vittoria
    {
        if(ActualScore >= ScoreToWin)
        {
            SceneManager.LoadScene(3);
        }
        
    }

    public void LoseCondition()                                                 //funzione per andare alla scena di sconfitta
    {
        if (ActualShip.Life <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
