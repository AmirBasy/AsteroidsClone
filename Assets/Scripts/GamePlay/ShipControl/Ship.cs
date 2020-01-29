using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    
    Gamemanager gameManager;
    public GameObject bullet;
    public Rigidbody rigidbody;
    public float rotSpeed;
    public float Speed;
    
    
    

    void Rotate(float direction)
    {
        rigidbody.AddTorque(transform.up * rotSpeed * direction);
    }

    void Move()
    {
        rigidbody.AddForce(transform.forward * Speed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject newShot = Instantiate(bullet); 
        newShot.transform.position = transform.position + transform.forward*2;
        gameManager.bullets += 1;
    }

    void CrossSpace()
    {
        if (transform.position.x > 34) { transform.position = new Vector3(-33, transform.position.y, transform.position.z); }
        if (transform.position.x < -34) { transform.position = new Vector3(33, transform.position.y, transform.position.z); }  //TODO da fixare a casa, userò gli Aspawner(bordi).
        if (transform.position.z > 18) { transform.position = new Vector3(transform.position.x, transform.position.y, -17); }
        if (transform.position.z < -18) { transform.position = new Vector3(transform.position.x, transform.position.y, 17); }

    }


    void LoseLife()
    {
       
        

        Instantiate(this.gameObject, new Vector3(0f,0f,0f), Quaternion.Euler(0f,0f,0f));//quando viene instanziata la nuova astronave la UI
                                                                                       //dei contorni delle altre cam(minimappa e retroview)
                                                                                      //si ridimensionano, probabilme c'entra con l'instanziamento
        Destroy(this.gameObject);                                                    //della nuova cam figlia della ship. Non ho idea di come fixare. FIXATO

        gameManager.GetComponent<Gamemanager>().life -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = FindObjectOfType<Gamemanager>();
        
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);//la posizione y della navicella cambia nonostante sia
                                                                                       //freezata nel rigidbody. Credo sia un effetto dell'addForce
        if (Input.GetKey(KeyCode.W)) Move ();                                         //che abbiamo usato per spostarla, ma non sono sicuro. FIXATO
                                                                                       
        if (Input.GetKey(KeyCode.A)) Rotate (-1);
        if (Input.GetKey(KeyCode.D)) Rotate (1);

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();

        CrossSpace();

      
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Asteroid"  | other.tag == "Alien") { LoseLife(); }
    }
}
