using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport3 : MonoBehaviour
{
    public Transform LimitDx;
    public GameObject Ship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Ship.transform.position = LimitDx.transform.position;
    }
}
