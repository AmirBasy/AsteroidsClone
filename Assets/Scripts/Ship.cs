using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ship : MonoBehaviour
{
    public Rigidbody rigibody; //variabile per il rigidbody
    public float rotationVelocity=45; //inizializza la velocità di rotazione
    public float acceleration; //istanzia la accelerazione
    public GameObject shotReference; //riferimento al proiettile
    public Image images; //riferimento a una immagine
    public int Life; //valore per la vita
    int counter = Vita.maxHealth; //inizializza il couter al massimo delle vite
    public AudioSource Audio; //variabile per l'audio del proiettile
    public Text GameOver; //variabile per il testo del game over

    void Rotate(float direction)
    {
        rigibody.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction); //codice per ruotare la nave
    }

    void Shot()
    {
        GameObject newShot = Instantiate(shotReference, transform.position, transform.localRotation); //instanzia l'oggetto
        Audio.Play(); //attiva il suono di uno sparo
    }

    void Accelerate()
    {
        rigibody.AddForce(transform.forward * acceleration * Time.deltaTime); //codice per accelerare
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AsteroidS" || collision.gameObject.tag == "AsteroidM" || collision.gameObject.tag == "AsteroidL") //si attiva quando collide con gli asteroidi
        {
            if (Vita.maxHealth == 0) //se la vita scende a 0
            {
                Die(); //muore
                GameOver.text = "Game Over"; //compare la scritta game over
            }
            else
            {
                Vita.maxHealth--; //scende la vita di 1
                counter = Vita.maxHealth; //setta il conteggio la vita
                GameObject go = GameObject.FindGameObjectWithTag("Health"); //trova un oggetto con un determinato tag
                Destroy(collision.collider.gameObject); //distrugge l'oggetto colliso
                Destroy(go); //distrugge una vita
            }
        }
        if (collision.gameObject.tag == "LimitDx") //si attiva quando collide con un oggetto che ha come tag LimitDx
        {
            float i = transform.position.z; //setta posizione z
            transform.position = new Vector3(-35, 0, i); //aggiorna la posizione
        }
        if (collision.gameObject.tag == "LimitSx") //si attiva quando collide con un oggetto che ha come tag LimitSx
        {
            float i = transform.position.z; //setta posizione z
            transform.position = new Vector3(35, 0, i); //aggiorna la posizione
        }
        if (collision.gameObject.tag == "LimitUp") //si attiva quando collide con un oggetto che ha come tag LimitUp
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, -16); //aggiorna la posizione
        }
        if (collision.gameObject.tag == "LimitDown") //si attiva quando collide con un oggetto che ha come tag LimitDown
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, 16); //aggiorna la posizione
        }
    }
    void Die()
    {
        Destroy(this.gameObject); //distrugge se stesso
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(-1); //ruota a sinistra
        if (Input.GetKey(KeyCode.D)) Rotate(1); //ruota a destra
        if (Input.GetKey(KeyCode.W)) Accelerate(); //accellera
        if (Input.GetKeyDown(KeyCode.Space)) Shot(); //spara
        print("Healh "+Vita.maxHealth); //stampa la vita in console
        print("counter " + counter); //stampa il counter in console
    }
}