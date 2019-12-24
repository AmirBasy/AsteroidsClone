using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed = 50;
    public Vector3 direction = Vector3.forward;
    public bool playerShot = true;

    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        Move();

        OutScreen();
    }

    public void CreateShot(Vector3 position, Vector3 newDirection, bool isPlayerShot)
    {
        transform.position = position;
        direction = newDirection;
        playerShot = isPlayerShot;

        //if is not a player shot, change color
        if(!playerShot)
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
    }

    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OutScreen()
    {
        //if out screen, destroy gameObject
        bool outScreen = gameManager.OutScreen(transform.position, 1.5f, -0.5f);

        if (outScreen)
            Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
