using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SerializationManager 
{ 
    
    public static bool Save(string savename, object savedata)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + savename + ".save";

        FileStream file = File.Create(path);

        formatter.Serialize(file, savedata);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path)) return null;

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("failed to load file at {0}", path);
            file.Close();
            return null;
        }

    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //surrogate selector

        Vector3Surrogates vector3surrogate = new Vector3Surrogates();
        QuaternionSurrogates quaternionsurrogate = new QuaternionSurrogates();

        return formatter;
    }

}
