using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;

    private void Start()
    {



        
        
    }

    private void Update()
    {

        this.gameObject.GetComponent<ProjectileMovementComponent>().UpdateProjectileMovement(this, speed);

    }

    
    
}
