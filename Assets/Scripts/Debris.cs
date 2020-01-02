using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : Obstacle
{
    private float _time;

    #region Unity Callbacks

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
 
            Destroy(this.gameObject);
        }
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
