using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score; //rende di dominio pubblico la variabile score
    private int displayScore; //variabile private per visualizzare lo score
    public Text scoreUI; //variabile pubblica dove inserire l'oggetto testo da modificare

    void Start()
    {
        score = 0; //setta lo score a 0
        displayScore = 0; //setta la visualizzazione a 0
    }

    void Update()
    {
        if (score != displayScore) //cicla quando sono diversi
        {
            displayScore = score; //li uguaglia
            scoreUI.text = displayScore.ToString(); //cambia il testo nell score aggiornato
        }
    }
}
