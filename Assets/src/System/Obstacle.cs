using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{

    protected void Awake()
    {
        
        this.gameObject.AddComponent<Rigidbody>();

        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        
    }

    
    
}
