using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Camera Main;
    public Camera P1;
    public Camera P2;
    AudioManager music;
    public Text onOff;
    


    
    private void Awake()
    {
        Main.depth = 1;
        P1.depth = 0;
        P2.depth = -1;
    }

    public void GoToP1() //gotodocP1 button pressed
    {
        Main.depth = 0;
        P1.depth = 1;
        P2.depth = -1;
    }

    public void GoToP2() //goyodocP2 button pressed
    {
        Main.depth = -1;
        P1.depth = 0;
        P2.depth = 1;

    }

    public void MainMenu() //mainMenu button pressed
    {
        Main.depth = 1;
        P1.depth = 0;
        P2.depth = -1;
    }

    public void StartB()
    {
        SceneManager.LoadScene(1);
    }

    public void StopMusic()
    {
        if (music.GetComponent<AudioSource>().mute == false)
        {
            music.GetComponent<AudioSource>().mute = true;
            onOff.GetComponent<Text>().text = "OFF";
        }
        else 
        {
            music.GetComponent<AudioSource>().mute = false;
            onOff.GetComponent<Text>().text = "ON";
        }
    }
    public void Start()
    {
        music = FindObjectOfType<AudioManager>();
    }

}
