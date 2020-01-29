﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asteroide : MonoBehaviour
{
    public GameObject Prefab; //rende pubblica l'assegnazione
    public GameObject PrefabM; //rende pubblica l'assegnazione
    public GameObject PrefabL; //rende pubblica l'assegnazione
    public Vector3 center; //variabile per il centro dello spawn
    public Vector3 size; //variabile dell'area dello spawn
    public int max; //variabile che segna il numero massimo di asteroidi spawnati
    GameObject[] AstS; //vettore per gli asteroidi
    GameObject[] AstM; //vettore per gli asteroidi
    GameObject[] AstL; //vettore per gli asteroidi
    public int Speed = 1; //velocità asteroidi
    public static int Valore = 0; //valore per lo sdoppiamento
    public static Vector3 Position; //rende pubblica la posizione a qualunque script
    public static Quaternion Rotation; //rende pubblica la rotazione a qualunque script
    void Start()
    {
        for (int i = 1; i < max; i++) //cicla fino al valore di max - 1
        {
            SpawnPrefab(); //richiama una funzione
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LimitDx") //si attiva quando collide con un oggetto che ha come tag LimitDx
        {
            float i = transform.position.z; //setta posizione z
            transform.position = new Vector3(-35, 0, i); //aggiorna la posizione
        }
        if (collision.gameObject.tag == "LimitSx") //si attiva quando collide con un oggetto che ha come tag Limitsx
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
    void SpawnPrefab()
    {
        Vector3 posL = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); //setta la posizione di spawn
        var rotL = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
        var goL = Instantiate(PrefabL, posL, rotL);
        goL.gameObject.tag = "AsteroidL"; //assegna un tag
        goL.name = "AsteroideLarge"; //assegna un nome
        goL.GetComponent<Rigidbody>(); //assegna il componente rigidbody
    }

    void SpawnS()
    {
        if(Valore==1)
        {
            var RotationS= Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
            var go = Instantiate(Prefab,Position,RotationS); //istanzia un oggetto
            go.gameObject.tag = "AsteroidS"; //assegna un tag
            go.name = "AsteroideSmall"; //assegna un nome
            go.transform.Translate(Vector3.forward *Time.deltaTime); //assegna un movimento
            var RotationS2 = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
            var g = Instantiate(Prefab, Position, RotationS2);//istanzia un oggetto
            g.gameObject.tag = "AsteroidS"; //assegna un tag
            g.name = "AsteroideSmall"; //assegna un nome
            g.transform.Translate(Vector3.forward * Time.deltaTime); //assegna un movimento
            Valore = 0; //setta un valore
        }
    }
    void SpawnM()
    {
        if (Valore == 2)
        {
            var RotationM = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
            var go = Instantiate(PrefabM, Position, RotationM); //istanzia un oggetto
            go.gameObject.tag = "AsteroidM"; //assegna un tag
            go.name = "AsteroideMedium"; //assegna un nome
            go.transform.Translate(Vector3.forward * Time.deltaTime); //assegna un movimento
            var RotationM2 = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
            var g = Instantiate(PrefabM, Position, RotationM2); //istanzia un oggetto
            g.gameObject.tag = "AsteroidM"; //assegna un tag
            g.name = "AsteroideMedium"; //assegna un nome
            g.transform.Translate(Vector3.forward * Time.deltaTime); //assegna un movimento
            Valore = 0; //setta un valore
        }
    }
    void Update()
    {
        AstS = GameObject.FindGameObjectsWithTag("AsteroidS"); //riunisce tutti gli oggetti con un determinato tag
        foreach (GameObject aS in AstS) //per ogni oggetto in un gruppo fa qualcosa
        {
            aS.transform.Translate(Vector3.forward * Speed*2 * Time.deltaTime); //movimento degli asteroidi
        }
        AstM = GameObject.FindGameObjectsWithTag("AsteroidM"); //riunisce tutti gli oggetti con un determinato tag
        foreach (GameObject aM in AstM) //per ogni oggetto in un gruppo fa qualcosa
        {
            aM.transform.Translate(Vector3.forward * Speed * Time.deltaTime); //movimento degli asteroidi
        }
        AstL = GameObject.FindGameObjectsWithTag("AsteroidL"); //riunisce tutti gli oggetti con un determinato tag
        foreach (GameObject aL in AstL) //per ogni oggetto in un gruppo fa qualcosa
        {
            aL.transform.Translate(Vector3.forward * Speed/2 * Time.deltaTime); //movimento degli asteroidi
        }
        SpawnS();
        SpawnM();
    }
}