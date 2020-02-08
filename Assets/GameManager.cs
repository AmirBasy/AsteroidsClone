using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ship ActualShip;
    public int ActualScore;
    public int ScoreToWin;
    public void Awake()
    {
        ActualShip = FindObjectOfType<Ship>();
    }

    private void Update()
    {
        VictoryCondition();
        LoseCondition();
    }
    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void VictoryCondition()
    {
        if (ActualScore >= ScoreToWin)
        {
        SceneManager.LoadScene(0);
        }
    }

    public void LoseCondition()
    {
        if (ActualShip.Life <= 1)
        {
        SceneManager.LoadScene(0);
        }
    }
   
    public void QuitGame()
    {
    Debug.Log("QUIT!");
    Application.Quit();
    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
