using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SFX_TYPE
{
    none, 
    Music,
    Environment,
    Effect
}

public class SFX : Resource
{
    public static SFX current;

    public AudioSource track;
    [Range(1, 3)]
    public int priority = 1;
    public SFX_TYPE type;
    public bool loop;

    #region UNITYCALLBACKS
    private void Awake()
    {
        current = this;
    }
    #endregion

}
