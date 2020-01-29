using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float time;
    public int speed = 7;
    public MiniA MiniA1;
    public MiniA MiniA2;
    Gamemanager gameManager; 
    

    void RotationFromU()
    {
        transform.rotation = Quaternion.Euler(0f,Random.Range(120f,230f),0f);
    }
    void RotationFromD()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(-70f, 70f), 0f);
    }
    void RotationFromR()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(240f, 300f), 0f);
    }
    void RotationFromL()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(60f, 120f), 0f);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }




    // Start is called before the first frame update
    void Start()
    {
       

        gameManager = FindObjectOfType<Gamemanager>();

        time = 0;

        if (transform.position.z >  17) { RotationFromU(); }            //TODO da fixare a casa. userò i colliders degli Aspawner
        if (transform.position.z < -16) { RotationFromD(); }
        if (transform.position.x >  31) { RotationFromR(); }
        if (transform.position.x < -30) { RotationFromL(); }
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        if (transform.position.y == 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Bullet")
        {
            Instantiate(MiniA1,transform.position,transform.rotation); 
            Instantiate(MiniA2,transform.position,transform.rotation);
            gameManager.score += 100;
            gameManager.asteroids += 1;
        }

        if (time > 0.5f & other.tag != "Alien" &  other.tag !="Asteroid"  ) {Die(); }

        

    }
}
