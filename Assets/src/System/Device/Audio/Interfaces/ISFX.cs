using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISFX<T>
{

    void Play(T trak);
    void Stop(T track);
    void Resume(T track);
    void Accellerate(float time);
    void Decellerate(float time);

    #region Utilities
    T GetCurrentTrack();
    float GetCurrentTime();
    #endregion



}
