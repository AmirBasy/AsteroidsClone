using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    GameManager GM;
    public Rigidbody rigbod;
    public GameObject shotReference;
    /*
    Empty object used as a reference for the spawning of the shots,
    Allowing to move the turret, in case of different types of ship
    */
    public Transform turret;
    //Class that contains all the basic information needed by every ship
    public ShipData _data;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        turret.localPosition = _data.turretPos;
    }

    #region Movement
    //Movement-related functions
    void Rotate(float direction)
    {
        rigbod.AddTorque(transform.up * _data.rotationVelocity * Time.deltaTime * direction);
    }

    void Accelerate()
    {
        rigbod.AddForce(transform.forward * _data.acceleration * Time.deltaTime);
    }
    void Decelerate()
    {
        rigbod.AddForce(transform.forward * _data.acceleration * Time.deltaTime * -1);
    }
    #endregion

    //Creates a new Shot-type object, at the turret's position
    void Shot()
    {
        Instantiate(shotReference, turret);
    }

    //Checks the input for the ship's movement and action
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { Rotate(-1); }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        { Rotate(1); }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        { Accelerate(); }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        { Decelerate(); }
        if (Input.GetKeyDown(KeyCode.Space))
        { Shot(); }
    }

    //Collision control and management
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit")
        {
            this.transform.position = GM.Teleport(this.transform, collision.gameObject);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            //Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
