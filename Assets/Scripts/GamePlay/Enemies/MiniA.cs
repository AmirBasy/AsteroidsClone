using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniA : MonoBehaviour
{
    public float time;
    public int speed = 8;
    Gamemanager gameManager;

    void Randomrot()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Gamemanager>();
        time = 0;
        Randomrot();
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        if (transform.position.y == 0)
        { transform.Translate(Vector3.forward * speed * Time.deltaTime); }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (time > 0.3f & other.tag != "ShipT" & other.tag != "Asteroid") 
        {
            Destroy(this.gameObject);
        }
        if (other.tag =="Bullet")
        {
            Destroy(this.gameObject);
            gameManager.score += 500;
            gameManager.miniA += 1;
        }
    }
}
