using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    public GameObject PlayerShip;

    [Header("Game Rules")]
    public static int CurrentPoints;
    private int PointsToNextLevel;

    public void Awake()
    {

        if(SaveLoadManager.SaveGameExists())
        {

            LoadGameSettings();
            LoadLevel();
            
        }

        //<---Read the screen resolution and sets the screen limit
        //<---Load Level Achived
        //<---try to find the ship - if found reference else create

    }

    public void Start()
    {
        
        if(!SaveLoadManager.SaveGameExists())
        {
            //generate level
            SaveLoadManager.Save();
        }

    }

    public void Update()
    {
        //<---Exausted Lifes?
        //<---control player position
        //<---player hit something
        //<---Update Points
        //<---Update UI
    }
    
    /*Pre-construct*/
    private void BuildLevel()
    {
        for (int i=1; i<=20; i++)
        {
            //spawn big asteroids
        }
    }
    
    public void SpawnAsteroidDebris()
    {

    }

    /*data*/
    private void LoadLevel()
    {
        SaveData f = SaveLoadManager.Load();

        PlayerShip.transform.position = f.level.player.position;

        CurrentPoints = f.level.score.score;
    }

    private void LoadGameSettings()
    {
        SaveData f = SaveLoadManager.Load();

        Screen.SetResolution(f.gamesettings.ScreenSize.x, f.gamesettings.ScreenSize.y, false);
    }

    /*game rules*/

    /*events*/
    
  

}


