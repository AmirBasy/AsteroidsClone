using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : Obstacle
{


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
        
    }

    #endregion



}
