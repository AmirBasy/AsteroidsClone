using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public void OnSave()
    {
        SerializationManager.Save("playersave", SaveData.current);
    }

    public void OnLoad()
    {
        


    }

}
