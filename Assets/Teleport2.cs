using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport2 : MonoBehaviour
{
    public Transform LimitSx;
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
        Ship.transform.position = LimitSx.transform.position;
    }
}
