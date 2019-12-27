using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SaveManager : MonoBehaviour
{

    public TMP_InputField savename;
    public GameObject loadbuttonprefab;

    public string[] savefiles;

    public void OnSave()
    {

        SerializationManager.Save(savename.text, SaveData.current);

    }

    public void GetLoadFiles()
    {

        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        savefiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");

    } 


}
