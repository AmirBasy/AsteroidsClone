using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed = 50;
    public Vector3 direction = Vector3.forward;
    public bool playerShot = true;


    protected virtual void Update()
    {
        Move(transform);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if reached limit destroy shot
        if (other.gameObject.CompareTag("Limit"))
            Die();
    }

    #region protected API

    protected virtual void Move(Transform tr)
    {
        tr.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    #endregion

    #region public API

    public virtual void CreateShot(Vector3 position, Vector3 newDirection, bool isPlayerShot)
    {
        transform.position = position;
        direction = newDirection;
        playerShot = isPlayerShot;
        
        //if is not a player shot, change color
        if(!playerShot)
        { 
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
            speed /= 3;
        }
    }

    #endregion
}
