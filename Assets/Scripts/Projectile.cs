using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int _projectileLifetime;
    private float _time;

    private void Update()
    {

        this.gameObject.GetComponent<ProjectileMovementComponent>().UpdateProjectileMovement(this);
        
        RecordTimeFromSpawn();

        if(LifetimeIsValid())
        {
            if(CanDestroy())
            {
                DestroyProjectile();
            }
        }
        else
        {
            Debug.LogWarning("Lifetime has no sense!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyProjectile();
    }

    private void RecordTimeFromSpawn()
    {
        _time += Time.deltaTime;
    }
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    /*Functions*/
    private bool LifetimeIsValid()
    {
        if (_projectileLifetime > 0.0f) return true;
        else return false;
    }
    private bool CanDestroy()
    {
        if (ConvertFloatToInteger(_time) == _projectileLifetime) return true;
        else return false;
    }
    private float ConvertFloatToInteger(float amount)
    {
        return Mathf.FloorToInt(amount);
    }
    
    

}
