using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitTeleport : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LimitDx") //si attiva quando collide con un oggetto che ha come tag LimitDx
        {
            float i = transform.position.z; //setta posizione z
            transform.position = new Vector3(-30, 0, i); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }
        if (collision.gameObject.tag == "LimitSx") //si attiva quando collide con un oggetto che ha come tag LimitSx
        {
            float i = transform.position.z; //setta posizione z
            transform.position = new Vector3(30, 0, i); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }
        if (collision.gameObject.tag == "LimitUp" && CompareTag("AsteroidL")) //si attiva quando collide con un oggetto che ha come tag LimitUp
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, -12); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }
        if (collision.gameObject.tag == "LimitDown" && CompareTag("AsteroidL")) //si attiva quando collide con un oggetto che ha come tag LimitDown
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, 12); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }



        if (collision.gameObject.tag == "LimitUp" && (CompareTag("AsteroidM") || CompareTag("AsteroidS"))) //si attiva quando collide con un oggetto che ha come tag LimitUp
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, -15); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }
        if (collision.gameObject.tag == "LimitDown" && (CompareTag("AsteroidM") || CompareTag("AsteroidS"))) //si attiva quando collide con un oggetto che ha come tag LimitDown
        {
            float i = transform.position.x; //setta posizione x
            transform.position = new Vector3(i, 0, 15); //aggiorna la posizione
            transform.Translate(Vector3.forward * Time.deltaTime); //movimento degli asteroidi
        }


    }
}
