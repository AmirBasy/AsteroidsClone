using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    Gamemanager gameManager;
    public float time;
    public float speed = 10;
    Ship ship;
    public Transform target;
    
   
    void MoveToShip ()
    {
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Gamemanager>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ship = FindObjectOfType<Ship>();
        time = time + Time.deltaTime;

        if (transform.position.y == 0 & time < 3)
        { transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime); }
       
        if (transform.position.y == 0 & time > 3)
        { MoveToShip(); }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        
        if ( other.tag ==  "Aspawner" | other.tag == "ShipT" )
        { Destroy(this.gameObject); }
        
        if (other.tag == "Bullet") { gameManager.score += 350; Destroy(this.gameObject); gameManager.aliens += 1; }
    }
}
