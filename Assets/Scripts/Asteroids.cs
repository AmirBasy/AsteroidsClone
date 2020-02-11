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
        Split(30f);
        Split(-30f);
        Destroy(this.gameObject);

    }

    void Split(float angle)
    {
        if (transform.localScale.x != size / 4)
        {
            var miniAsteroid = Instantiate(asteroid, transform.position, transform.rotation) as GameObject;
            miniAsteroid.transform.Rotate(0f, angle, 0f);
            miniAsteroid.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
        }
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
    }

}
