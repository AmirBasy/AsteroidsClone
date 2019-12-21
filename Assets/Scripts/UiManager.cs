using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject lifePrefab0, lifePrefab1, lifePrefab2;
    public Text Score;
    public string TextInScore = "Score: ";

    GameManager GameManager;
    int playerLife;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        
        SetCurrentShipLife();
    }

    public void UpdateUI()
    {
        //enable or disable lifes in UI
        if (playerLife == 3)
        {
            lifePrefab0.SetActive(true);
            lifePrefab1.SetActive(true);
            lifePrefab2.SetActive(true);
        }
        if (playerLife == 2)
        {
            lifePrefab0.SetActive(true);
            lifePrefab1.SetActive(true);
            lifePrefab2.SetActive(false);
        }
        if (playerLife == 1)
        {
            lifePrefab0.SetActive(true);
            lifePrefab1.SetActive(false);
            lifePrefab2.SetActive(false);
        }
        if (playerLife == 0)
        {
            lifePrefab0.SetActive(false);
            lifePrefab1.SetActive(false);
            lifePrefab2.SetActive(false);
        }

        Score.text = TextInScore + GameManager.ActualScore;
    }

    public void SetCurrentShipLife()
    {
        //set life and update UI
        playerLife = GameManager.ActualShip.Life;
        UpdateUI();
    }
}
