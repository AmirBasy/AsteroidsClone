using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    public GameObject PlayerShip;

    [Header("Game Rules")]
    public Vector2 ScreenResolution;
    public int CurrentPoints;
    public int PointsToNextLevel;
    public int currentlevel;

    public void Awake()
    {

        //<---Read the screen resolution and sets the screen limit
        //<---Load Level Achived
        //<---try to find the ship - if found reference else create

    }

    public void Update()
    {
        //<---Exausted Lifes?
        //<---control player position
        //<---player hit something
        //<---Update Points
        //<---Update UI
    }
    
    

  

}


