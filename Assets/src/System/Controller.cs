using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float _forwardAxis;
    public float _rightAxis;

    public void Update()
    {
        _forwardAxis = Input.GetAxis("Vertical");
        _rightAxis = Input.GetAxis("Horizontal");
    }
}
