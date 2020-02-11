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

    bool isPaused = false;
    public GameObject panel;
    bool state;

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
        Time.timeScale = 1;
    }

    void Pause()
    {
        if (isPaused == true) //se è in pausa
        {
            Time.timeScale = 1; //riprende il tempo normalizzandolo
            isPaused = false; //setta la variabile booleana della pausa a falso
        }
        else
        {
            Time.timeScale = 0; //stoppa il tempo
            isPaused = true; //setta la variabile booleana della pausa a vero
        }
    }

    public void SwitchShowHide()
    {
        state = !state; //variabile che indica se il pannello si vede o no e cambia stato
        panel.gameObject.SetActive(state); //aggiorna lo stato del pannello
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(); //mette il pausa
            SwitchShowHide(); //fa vedere il menù di pausa
        }
    }

}
