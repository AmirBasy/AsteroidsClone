using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text _scoreText;

    protected GameManager _gm;

    protected void Start()
    {
       _gm = Object.FindObjectsOfType<GameManager>()[0];
    }

    protected void FixedUpdate()
    {
        _scoreText.text = _gm.GetScore().ToString();


    }
}
