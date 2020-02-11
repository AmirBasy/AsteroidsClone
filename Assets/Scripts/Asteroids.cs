using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject asteroid;
    public float speed=1;
    public float size;
    public GameObject manager;
    

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Die()
    {
        manager.GetComponent<GameManager>().ScoreIncrease();
        Destroy(this.gameObject);

    }

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Limit_UP") transform.position += new Vector3(0, 0, -24);
        if (collision.gameObject.name == "Limit_DX") transform.position += new Vector3(-45, 0, 0);
        if (collision.gameObject.name == "Limit_DOWN") transform.position += new Vector3(0, 0, 24);
        if (collision.gameObject.name == "Limit_SX") transform.position += new Vector3(45, 0, 0);
        if (collision.gameObject.name == "Limit_TOP") transform.position += new Vector3(45, 0, 24);
        
        if (collision.gameObject.tag == "Asteroid")
        {
            Die();
        }
        if (collision.gameObject.tag == "Shot")
        {
            Die();
        }
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Die();
        }
        if (collision.gameObject.tag == "Shot")
        {
            Die();
        }
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }
    }

}
