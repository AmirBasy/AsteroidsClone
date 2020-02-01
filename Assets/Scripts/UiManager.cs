using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject lifePrefab0, lifePrefab1, lifePrefab2;
    public Text score;
    public string textInScore = "Score: ";
    public GameObject pauseMenu;

    int playerLife;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;

        if (gameManager != null)
        {
            SetCurrentShipLife();
            pauseMenu.transform.Find("Panel").Find("Resume_Button").GetComponent<Button>().onClick.AddListener(gameManager.ResumeGame);
        }
    }

    public void SetCurrentShipLife()
    {
        //set life and update UI
        playerLife = gameManager.actualShip.life;
        UpdateUI();
    }

    public void UpdateUI()
    {
        //enable or disable lifes in UI
        if (playerLife >= 3)
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

        //update score
        score.text = textInScore + gameManager.actualScore;
    }

    public void PauseMenu(bool pause)
    {
        pauseMenu.SetActive(pause);
    }
}
