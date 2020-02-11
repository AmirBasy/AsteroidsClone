using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asteroide : MonoBehaviour
{
    public GameObject PrefabAsteroidSmall; //rende pubblica l'assegnazione
    public GameObject PrefabAsteroidMedium; //rende pubblica l'assegnazione
    public GameObject PrefabAsteroidLarge; //rende pubblica l'assegnazione
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
    public float acceleration; //istanzia la accelerazione
    public Rigidbody rb; //variabile per il rigidbody

    void Start()
    {
        for (int i = 1; i < max; i++) //cicla fino al valore di max - 1
        {
            SpawnPrefab(); //richiama una funzione
        }
    }

    void SpawnPrefab()
    {
        Vector3 posL = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); //setta la posizione di spawn
        var rotL = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
        var go = Instantiate(PrefabAsteroidLarge, posL, rotL); //istanzia un oggetto
        go.gameObject.tag = "AsteroidL"; //assegna un tag
        go.GetComponent<Rigidbody>(); //assegna il componente rigidbody
    }

    void SpawnAsteroidMediumAndSmall()
    {
        if (Valore == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                var Rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
                var go = Instantiate(PrefabAsteroidMedium, Position, Rotation); //istanzia un oggetto
                go.transform.Translate(Vector3.forward * Time.deltaTime); //assegna un movimento
                go.gameObject.tag = "AsteroidM"; //assegna un tag
                Valore = 0; //setta un valore
            }
        }
        if (Valore == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                var Rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0); //setta rotazione
                var go = Instantiate(PrefabAsteroidSmall, Position, Rotation); //istanzia un oggetto
                go.transform.Translate(Vector3.forward * Time.deltaTime); //assegna un movimento
                go.gameObject.tag = "AsteroidS"; //assegna un tag
                Valore = 0; //setta un valore
            }
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
        SpawnAsteroidMediumAndSmall();
    }
}