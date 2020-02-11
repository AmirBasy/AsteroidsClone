using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioSource SoundShot; //variabile per l'audio del proiettile
    public AudioSource SoundAsteroidSmall; //variabile per l'audio del proiettile
    public AudioSource SoundAsteroidMedium; //variabile per l'audio del proiettile
    public AudioSource SoundAsteroidLarge; //variabile per l'audio del proiettile
    public AudioSource SoundGame; //variabile per l'audio del proiettile
    private static SoundManager original; //valore per il controllo di duplicazione
    private static int value = 0; //valore per controllo della duplicazione tra le scene
    public Slider VolumeSlider; //slider per il volume
    public Text Value; //valore dello slider
    int ValueSave;

   /* private void Awake()
    {
        ValueSave = PlayerPrefs.GetInt("ValoreVolume");
        Value.text = ValueSave.ToString(); //cambia valore al testo dello slide
    }*/

    private void Start()
    {
        if (value==0) //controlla il valore
        {
            AudioGame();
            if (original != this) //controlla se original è diverso dall'audio
            {
                if (original != null) //controlla se è diverso
                {
                    Destroy(original.SoundGame); //distrugge l'audio per non duplicarlo

                }
                DontDestroyOnLoad(SoundGame); //permette all'audio di non ricominciare e passare tra le scene
                original = this; //uguaglia original con l'audio
            }
            value++; //aggiunge 1 a value
        }
    }
    void AudioShot()
    {
        if (Ship.ValueShot == 1) //controlla il valore
        {
            SoundShot.Play(); //parte l'audio
            Ship.ValueShot = 0; //resetta il valore
        }
    }
    void AudioExplosionAsteroid()
    {
        if(Shot.ValueAsteroid == 1) //controlla il valore
        {
            SoundAsteroidSmall.Play(); //parte l'audio
            Shot.ValueAsteroid = 0; //resetta il valore
        }
        if (Shot.ValueAsteroid == 2) //controlla il valore
        {
            SoundAsteroidMedium.Play(); //parte l'audio
            Shot.ValueAsteroid = 0; //resetta il valore
        }
        if (Shot.ValueAsteroid == 3) //controlla il valore
        {
            SoundAsteroidLarge.Play(); //parte l'audio
            Shot.ValueAsteroid = 0; //resetta il valore
        }
    }
    void AudioGame()
    {
        SoundGame.Play(); //parte l'audio
    }

    public void ValueSlider()
    {
        ValueSave = ((int)(VolumeSlider.value * 100));
       // PlayerPrefs.SetInt("ValoreVolume", ValueSave);
        Value.text = ValueSave.ToString(); //cambia valore al testo dello slide
        
    }
    public void SetVolume()
    {
        AudioListener.volume = VolumeSlider.value; //cambia il volume con il valore dello slider
    }

    void Update()
    {
        AudioShot();
        AudioExplosionAsteroid();
        ValueSlider();
        SetVolume();   
    }
}