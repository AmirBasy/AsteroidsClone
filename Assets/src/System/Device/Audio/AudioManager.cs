using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;

    private SFX[] resource;

   
    private event Action OnGameBegin;

    #region UNITYCALLBACKS
    protected void Awake()
    {
        current = this;

        OnGameBegin += PlayTheme;
    }
    protected void Start()
    {
        //track resources and enlist by priority
        init_resources();

        //Execute All Playable tracks : OnGameBegin : TEST
        OnGameBegin.Invoke();

    }

    private void PlayTheme()
    {
        for (int i = 0; i < gameObject.GetComponents<SFX>().Length; i++)
        {
            if (resource[i].type == SFX_TYPE.Music || resource[i].type == SFX_TYPE.Environment)
            {
                Play(resource[i]);
            }
        }
    }
    #endregion
    private void init_resources()
    {
        //get all resources of this entity
        resource = GetComponents<SFX>();

        //setup all tracks 
        SetupTracksByType(resource);
    }

    private static void Play(SFX sc)
    {
        sc.track.Play();
    }
    private void SetupTracksByType(SFX[] scs)
    {
        for(int i=0; i<scs.Length; i++)
        {
            switch(scs[i].priority)
            {
                case (1):
                    SetupTrack(scs[i], 0.7f, 1, 0.5f);
                    break;
                case (2):
                    SetupTrack(scs[i], 0.5f, 1, 0.5f);
                    break;
                case (3):
                    SetupTrack(scs[i], 0.2f, 1, 0.5f);
                    break;
            }
        }
    }
    private void SetupTrack(SFX sc, float volume, float pitch, float reverb)
    {
        sc.track.clip = sc.clip;
        sc.track.volume = volume;
        sc.track.pitch = pitch;
        sc.track.reverbZoneMix = reverb;
    }
   
    //choce the track to execute
    public void PlayShot()
    {
        resource[2].track.Play();
    }
    public void PlayExplotion()
    {
        resource[3].track.Play();
    }
}
