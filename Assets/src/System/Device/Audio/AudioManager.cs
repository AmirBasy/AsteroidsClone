using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;

    private SFX[] resource;

    private static event Action<SFX> OnTrackStart;
    private static event Action<SFX> OnTrackEnd;
    private static event Action<SFX> OnTrackLoop;
    private static event Action<SFX> OnGameBegin;

    #region UNITYCALLBACKS
    protected void Awake()
    {
        current = this;
    }
    protected void Start()
    {
        //track resources and enlist by priority
        init_resources();

        //Execute All Playable tracks : OnGameBegin : TEST
        for(int i=0; i<gameObject.GetComponents<SFX>().Length; i++)
        {
            if(resource[i].type == SFX_TYPE.Music || resource[i].type == SFX_TYPE.Environment) 
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
                    SetupTrack(scs[i], 1, 1, 0.5f);
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
}
