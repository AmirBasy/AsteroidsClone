using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    Gamemanager gameManager;

    public static AudioManager instance;
   

   
    // Start is called before the first frame update
    void Awake()
    { 
        
        DontDestroyOnLoad(gameObject);

        if (instance == null) { instance = this; } else { Destroy(gameObject); }
        
        gameManager = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
      
        

        if (SceneManager.GetActiveScene().name == "start")
        {
            gameObject.GetComponent<AudioSource>().pitch = 1;
        }

        if (gameManager.life == 2 & gameObject.GetComponent<AudioSource>().pitch<1.30)
        {
            gameObject.GetComponent<AudioSource>().pitch += 0.008f;
        }
        if (gameManager.life == 1 & gameObject.GetComponent<AudioSource>().pitch < 1.6)
        {
            gameObject.GetComponent<AudioSource>().pitch += 0.008f;
        }


        if (SceneManager.GetActiveScene().name == "GameOver" & gameObject.GetComponent<AudioSource>().pitch>0) 
        {
            gameObject.GetComponent<AudioSource>().pitch -= 0.004f;
        }

    }

}
