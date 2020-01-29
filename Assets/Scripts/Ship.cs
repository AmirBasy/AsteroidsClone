using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigbod;
    public GameObject shotReference;
    GameManager Gm;
    public Transform turret;
    public ShipData _data;

    private void Awake()
    {
        Gm = FindObjectOfType<GameManager>();
        turret.localPosition = _data.turretPos;
    }
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

    void Shot()
    {
        Instantiate(shotReference, turret);
    }

    void Start()
    {
        
    }

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit")
        {
            this.transform.position = Gm.Teleport(this.transform, collision.gameObject);
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
