using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int life = 3;
    public float acceleration = 6000;
    public float rotationVelocity = 1800;
    public bool invincible = false;
    public GameObject shotReference;

    Rigidbody rb;
    GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();

        CrossScreen();
    }

    void Accelerate()
    {
        rb.AddForce(transform.forward * acceleration * Time.deltaTime);
    }

    void Rotate(float direction)
    {
        rb.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Shot()
    {
        //spawn shot
        GameObject newShot = Instantiate(shotReference);

        //and set it
        Vector3 position = transform.position + transform.forward;
        Vector3 direction = transform.forward;
        bool isPlayerShot = true;

        newShot.GetComponent<Shot>().CreateShot(position, direction, isPlayerShot);
    }

    void CrossScreen()
    {
        //if out of the screen, teleport to the other side
        transform.position = gameManager.CrossScreen(transform.position, 1, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            Shot shot = other.GetComponentInParent<Shot>();

            //if alien shot
            if (!shot.playerShot)
            {
                gameManager.AddDamage();
            }
        }
    }
}
