using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public float velocity = 10f; //velocità proiettile
    public Vector3 direction = Vector3.forward; //direzione proiettile

    private void OnCollisionEnter(Collision collision) //rileva una collisione
    {
        if(collision.gameObject.tag == "Limit") //se collide con un oggetto che ha come tag limit
        {
            Die(); //muore
        }
        if (collision.gameObject.tag == "AsteroidS") //se collide con un oggetto che ha come tag asteroidS
        {
            Score.score++; //aumenta lo score tramite lo script score
            Destroy(collision.collider.gameObject); //distrugge l'oggetto con cui ha colliso
            Destroy(this.gameObject); //distrugge se stesso
        }
        if (collision.gameObject.tag == "AsteroidM") //se collide con un oggetto che ha come tag AsteroidM
        {
            Score.score = Score.score + 2; //aumenta lo score tramite lo script score
            Asteroide.Valore = 1; //assegna valore uno alla varibile valore situato in un altro script
            Asteroide.Position = transform.position; //aggiorna posizione
            Asteroide.Rotation = transform.rotation; //aggiorna rotazione
            Destroy(collision.collider.gameObject); //distrugge l'oggetto con cui ha colliso
            Destroy(this.gameObject); //distrugge se stesso
        }
        if (collision.gameObject.tag == "AsteroidL") //se collide con un oggetto che ha come tag AsteroidL
        {
            Score.score = Score.score + 3; //aumenta lo score tramite lo script score
            Asteroide.Valore = 2; //assegna valore uno alla varibile valore situato in un altro script
            Asteroide.Position = transform.position; //aggiorna posizione
            Asteroide.Rotation = transform.rotation; //aggiorna rotazione
            Destroy(collision.collider.gameObject); //distrugge l'oggetto con cui ha colliso
            Destroy(this.gameObject); //distrugge se stesso
        }

    }
    void Die()
    {
        Destroy(this.gameObject); //distrugge se stesso
    }
    void Move()
    {
        transform.Translate(direction * velocity * Time.deltaTime * 2); //codice per il movimento
    }

    void Update()
    {
        Move();
    }
}