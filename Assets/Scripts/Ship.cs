using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigbod;
    public float rotationVelocity;
    public float acceleration;
    public GameObject shotReference;
    public int life;
    GameManager Gm;

    private void Awake()
    {
        Gm = FindObjectOfType<GameManager>();
    }
    void Rotate(float direction)
    {
        rigbod.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Accelerate()
    {
        rigbod.AddForce(transform.forward * acceleration * Time.deltaTime);
    }
    void Decelerate()
    {
        rigbod.AddForce(transform.forward * acceleration * Time.deltaTime * -1);
    }

    void Shot()
    {
        Instantiate(shotReference, transform.localPosition, transform.localRotation);
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
