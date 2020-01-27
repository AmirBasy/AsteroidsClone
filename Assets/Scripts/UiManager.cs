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
    public GameObject lifePrefableft, lifePrefabcenter, lifePrefabright;
    int playerLife;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        Score.text = TextInScore + GameManager.ActualScore;
    }

    private void Start()
    {
        SetCurrentShipLife();
    }

    public void activeUi()
    {
        if (playerLife == 3)
        {
            lifePrefableft.SetActive(true);
            lifePrefabcenter.SetActive(true);
            lifePrefabright.SetActive(true);
        }
        if (playerLife == 2)
        {
            lifePrefableft.SetActive(true);
            lifePrefabcenter.SetActive(true);
            lifePrefabright.SetActive(false);
        }
        if (playerLife == 1)
        {
            lifePrefableft.SetActive(true);
            lifePrefabcenter.SetActive(false);
            lifePrefabright.SetActive(false);
        }
        if (playerLife == 0)
        {
            lifePrefableft.SetActive(false);
            lifePrefabcenter.SetActive(false);
            lifePrefabright.SetActive(false);
        }

        
    }

    public void SetCurrentShipLife()
    {
        playerLife = GameManager.ActualShip.Life;
    }

    public void UpdateScore()
    {
        Score.text = TextInScore + GameManager.ActualScore;
    }
}
