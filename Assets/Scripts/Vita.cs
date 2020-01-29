using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vita : MonoBehaviour
{
    static int counter = 0; //conteggio vita
    public int maxHealtTemp = 10; //vita massima
    public static int maxHealth; //rende la vita massima pubblic così da portela modificare

    void Start()
    {
        maxHealth = maxHealtTemp; //uguaglia l'eventuale modifica
        counter++; //conta
        if(counter < maxHealth) //cicla fino a maxHealth - 1
        {
            Life();
        }
    }

    void Life()
    {
        var myPos = transform.position; //setta la posizione
        var go = Instantiate(gameObject, new Vector3(myPos.x, myPos.y + 1, myPos.z + 1),transform.localRotation); //istanzia un oggetto
        go.transform.parent = GameObject.Find("LifeAnchor").transform; //trova il genitore dove instanziare l'oggetto
        go.name = "Life no. " + counter; //rinomina le istanze numerandole

    }
}
