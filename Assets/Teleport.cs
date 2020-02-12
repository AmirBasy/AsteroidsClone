using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform LimitDown;
    public GameObject Ship;
    public GameObject AsteroidBig;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Ship.transform.position = LimitDown.transform.position;
    }
    void OnCollisionEnter(Collision collision)
    {
        AsteroidBig.transform.position = LimitDown.transform.position;
    }
}
