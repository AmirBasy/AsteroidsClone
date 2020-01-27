using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public int scoreTogive;
    public float velocity = 6f;
    public Camera cam;
    Vector3 direction;
    public GameObject asteroid1;
    public GameObject asteroid2;
    Vector3 position1;
    Vector3 position2;
    public GameManager GM;
    public UiManager ui;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.FindObjectOfType<GameManager>();
        ui = UiManager.FindObjectOfType<UiManager>();
        cam = FindObjectOfType<Camera>();
        //from world point to viewport point
       
        direction = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        CrossScreen();

        transform.Translate(direction * velocity * Time.deltaTime);

    }

    void CrossScreen()
    {

        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        //if out of the screen, teleport to the other side

        if (screenPoint.x > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.x < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(1, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.y > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 0, screenPoint.z));
        }
        else if (screenPoint.y < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 1, screenPoint.z));
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Shot>())
        {
            if (this.gameObject.transform.localScale.magnitude <= new Vector3 (0.5f, 0.5f, 0.5f).magnitude)
            {
                GM.ActualScore += 20;
                ui.UpdateScore();
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);

                Split();
            }
        }

    }

    void Split()
    {   
        asteroid1 = Instantiate(this.gameObject);   
        asteroid2 = Instantiate(this.gameObject);

        asteroid1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        asteroid2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        position1 = transform.position + Vector3.right;
        position2 = transform.position + Vector3.left;  
    }
}
