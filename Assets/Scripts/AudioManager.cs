using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip alienInScene;

    AudioSource musicSource;
    AudioSource asteroidsSource;
    AudioSource alienSource;

    Coroutine replayMusicSource;
    GameManager gameManager;

    void Start()
    {
        CreateAudioSources();

        gameManager = GetComponent<GameManager>();

        //start game music
        OnMusicStart(gameMusic);
    }

    #region private API

    void CreateAudioSources()
    {
        //add audio sources
        AudioSource[] listSource = new AudioSource[3];
        for (int i = 0; i < 3; i++)
        {
            listSource[i] = gameObject.AddComponent<AudioSource>();
        }

        //set them to variables
        musicSource = listSource[0];
        asteroidsSource = listSource[1];
        alienSource = listSource[2];

        //and set them
        SetSources();
    }

    void SetSources()
    {
        musicSource.priority = 0;

        alienSource.priority = 50;

        asteroidsSource.priority = 200;
    }

    void OnMusicStart(AudioClip audioClip)
    {
        //stop coroutine 
        if(replayMusicSource != null)
            StopCoroutine(replayMusicSource);

        musicSource.clip = audioClip;
        musicSource.Play();

        //call coroutine
        replayMusicSource = StartCoroutine(ReplayMusicSource(audioClip));
    }

    IEnumerator ReplayMusicSource(AudioClip audioClip)
    {
        //wait music to end
        yield return new WaitForSeconds(audioClip.length);

        //edit pitch and replay
        musicSource.pitch = Random.Range(1f, 1.5f);
        OnMusicStart(audioClip);
    }

    void OnPlayerSound(AudioClip audioClip)
    {
        //use ship audioSource
        gameManager.actualShip.audioSource.clip = audioClip;
        gameManager.actualShip.audioSource.Play();
    }

    void OnAsteroidSound(AudioClip audioClip)
    {
        //can't call from asteorid source, 'cause they are destroyed 
        asteroidsSource.clip = audioClip;
        asteroidsSource.Play();
    }

    void OnAlienSound(AudioClip audioClip)
    {
        //can play shotSound from alien, but destroySound only from here, so I used only this audioSource
        alienSource.clip = audioClip;
        alienSource.Play();
    }

#endregion

    #region public API

    public System.Action<AudioClip> GetPlayerFunction()
    {
        return OnPlayerSound;
    }

    public System.Action<AudioClip> GetAsteroidFunction()
    {
        return OnAsteroidSound;
    }

    public System.Action<AudioClip> GetAlienFunction()
    {
        return OnAlienSound;
    }

    public void ChangeGameMusic(bool isAlienInScene)
    {
        if (isAlienInScene)
        {
            if(musicSource.clip != alienInScene)
                OnMusicStart(alienInScene);
        }
        else
        {
            if (musicSource.clip != gameMusic)
                OnMusicStart(gameMusic);
        }
    }

    #endregion
}
