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
        manager.GetComponent<Manager>().ScoreIncrease();
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
        if (collision.gameObject.name == "collisionTopA") transform.position += new Vector3(0, 0, -21);
        if (collision.gameObject.name == "collisionBottomA") transform.position += new Vector3(0, 0, 21);
        if (collision.gameObject.name == "collisionLeftA") transform.position += new Vector3(39, 0, 0);
        if (collision.gameObject.name == "collisionRightA") transform.position += new Vector3(-39, 0, 0);
    }
}
