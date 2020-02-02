using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sound_cue;

    private SFX[] resource;

    private static event Action<SFX> OnTrackStart;
    private static event Action<SFX> OnTrackEnd;
    private static event Action<SFX> OnTrackLoop;
    private static event Action<SFX> OnGameBegin;

    #region UNITYCALLBACKS
    protected void Awake()
    {
        sound_cue = this;
    }
    protected void Start()
    {
        //track resources and enlist by priority
        init_resources();

        //Execute All Playable tracks : OnGameBegin : TEST
        for(int i=0; i<resource.Length; i++)
        {
            if(resource[i].type == SFX_TYPE.Music) { Play(resource[i]); }
        }
    }
    #endregion
    private void init_resources()
    {
        for(int i=0; i<resource.Length; i++)
        {
            //get resources of this entity
            resource[i] = GetComponent<SFX>();
        }

        //sort resources by priority
        resource = SortByPriority(resource);

        //setup all tracks 
        SetupTracksByType(resource);
    }

    private static void Play(SFX sc)
    {
        sc.track.Play();
    }
    private SFX[] SortByPriority(SFX[] scs)
    {
        SFX[] priorized_list = null;
        int counter = 0;

        //select tracks with priority 1
        for(int i=0; i<scs.Length; i++)
        {
            if(scs[i].priority==1)
            {
                priorized_list[counter] = scs[i];
                counter++;
            }

        }
        for(int q = counter; q<scs.Length; q++)
        {
            if(scs[q].priority == 2)
            {
                priorized_list[counter] = scs[q];
                counter++;
            }
        }
        for(int k = counter; k<scs.Length; k++)
        {
            if(scs[k].priority == 3)
            {
                priorized_list[counter] = scs[k];
                counter++;
            }
        }

        return priorized_list;
    }
    private void SetupTracksByType(SFX[] scs)
    {
        for(int i=0; i<scs.Length; i++)
        {
            switch(scs[i].priority)
            {
                case (1):
                    SetupTrack(scs[i], 7, 5);
                    break;
                case (2):
                    SetupTrack(scs[i], 5, 5);
                    break;
                case (3):
                    SetupTrack(scs[i], 3, 5);
                    break;
            }
        }
    }

    private void SetupTrack(SFX sc, float volume, float pitch)
    {
        sc.track.volume = volume;
        sc.track.pitch = 10;
    }
}
