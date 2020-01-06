using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private GameManager _gm;

    private void Start()
    {
        _gm = Object.FindObjectsOfType<GameManager>()[0];


    }

    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _gm.BuildLevel();
            Destroy(this.gameObject);
        }
    }
}
