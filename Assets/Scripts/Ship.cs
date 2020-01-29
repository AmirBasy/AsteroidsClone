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
        if (Input.GetKeyDown(KeyCode.Space)) Shot(shotReference);

        CrossScreen();
    }

    #region private API
    void Rotate(float direction)
    {
        rb.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
    }

    GameObject istantiateShot(GameObject _missile)
    {
        //spawn shot
        GameObject newShot = Instantiate(_missile);
        return newShot;
    }

    #region Shot API

    Vector3 setDirection(GameObject _prefab)
    {
        Vector3 direction = _prefab.transform.forward;
        return direction;
    } 

    Vector3 setPosition(GameObject _prefab)
    {
        Vector3 position = _prefab.transform.position + _prefab.transform.forward;
        return position;
    }

    void Shot(GameObject _prefab)
    {
        GameObject _gameObject = istantiateShot(_prefab);
        bool isPlayerShot = true;
        _gameObject.GetComponent<Shot>().CreateShot(setPosition(this.gameObject), setDirection(this.gameObject), isPlayerShot);
    } 
    #endregion

    // <summary>
    /// if out of the screen, teleport to the other side
    /// </summary>
    void CrossScreen()
    {
        transform.position = gameManager.CrossScreen(transform.position, 1, 0);
    }


    public void Accelerate()
    {
        rb.AddForce(transform.forward * acceleration * Time.deltaTime);
    }

    public void Rotate()
    {
        throw new System.NotImplementedException();
    }

	#endregion    
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

