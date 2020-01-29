using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Ship ActualShip; //variabile per inizializzare la ship
    public int ActualScore; //variabile per inizializzare lo score per condizioni di vittore e sconfitta
    public int ScoreToWin; //variabile per lo score di vincità
    public void Awake()
    {
        ActualShip = FindObjectOfType<Ship>(); //trova e inizializza l'oggetto del tipo ship
    }

    public void GoToPlay()
    {
        SceneManager.LoadScene("PlayScene"); //carica la scena PlayScene
    }
    public void GoToOption()
    {
        SceneManager.LoadScene("OptionMenu"); //carica la scena OptionMenu
    }
    public void GoToExit()
    {
        Application.Quit(); //esce dal gioco
    }
    public void GoToBack()
    {
        SceneManager.LoadScene("MainMenu"); //carica la scena MainMenu
    }


   /* Codice per settare una possibile condizione di vittore o sconfitta 
    
     
    private void Update()
    {
        VictoryCondition();
        LoseCondition();
    }

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {

        }
    }

    public void LoseCondition()
    {
        if (ActualShip.Life <= 1)
        {

        }
    }*/
}
