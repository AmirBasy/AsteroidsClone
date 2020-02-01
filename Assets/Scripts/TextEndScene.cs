using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if there is gameManager (so after gameplay)
        GameManager gameManager = GameManager.instance;
        if (gameManager != null)
        {
            Text textScores = GetComponent<Text>();

            //show scores
            textScores.text += "\nScores: " + gameManager.actualScore;
        }
    }
}
