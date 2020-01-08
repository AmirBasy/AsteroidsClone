using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float velocity = 50;
    public Vector3 direction = Vector3.forward;
    public Camera cam;
    GameManager GameManager;

    void Move()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider collider)                                        //funzione per distruggre l'asteroide e il colpo 
    {
        if (collider.gameObject.tag == "Asteroid")
        {
            Destroy(this.gameObject);
            Destroy(collider.gameObject);
            GameManager.ActualScore++;
        }     
    }

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Move();

        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        if (screenPoint.x > 1)
        {
            Destroy(this.gameObject);
        }
        else if (screenPoint.x < 0)
        {
            Destroy(this.gameObject);
        }
        else if (screenPoint.y > 1)
        {
            Destroy(this.gameObject);
        }
        else if (screenPoint.y < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
