using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public string TextInScore = "Score +";
    public Text Score;
    public Transform LifeToSpawn;
    GameManager GameManager;
    public GameObject lifePrefab0, lifePrefab1, lifePrefab2;
    int playerLife;

    private void Awake()
    {
        //Score.text = TextInScore + GameManager.ActualScore;
        GameManager = FindObjectOfType<GameManager>();
        SetCurrentShipLife();
    }

    void activeUi()
    {
        if (playerLife == 3)
        {
            lifePrefab0.SetActive(true);
            lifePrefab1.SetActive(true);
            lifePrefab2.SetActive(true);
        }
        if (playerLife == 2)
        {
            lifePrefab1.SetActive(true);
            lifePrefab2.SetActive(false);
        }
        if (playerLife == 1)
        {
            lifePrefab2.SetActive(false);
        }
        if (playerLife == 0)
        {
            lifePrefab0.SetActive(false);
        }


    }

    void SetCurrentShipLife()
    {
        //playerLife = GameManager.ActualShip.Life;
    }
}