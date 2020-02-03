using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Debris : Obstacle
{
    private event Action OnDestroy;

    private float _time;

    private GameManager _gm;

    #region Unity Callbacks
    protected void Awake()
    {
        OnDestroy += AudioManager.current.PlayExplotion;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            _gm.AddScore(10);
            OnDestroy.Invoke();
            Destroy(this.gameObject);
        }
    }

    protected void Start()
    {
        _gm = FindObjectsOfType<GameManager>()[0];
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if(TimeIsOver())
        {
            Destroy(this.gameObject);
        }
    }

    #endregion
    #region Timer
    protected bool TimeIsOver()
    {
        if (Mathf.FloorToInt(_time) == 5)
        {
            return true;
        }
        else return false;
    }
    #endregion



}
