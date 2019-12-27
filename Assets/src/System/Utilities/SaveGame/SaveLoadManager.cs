using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save.sav", FileMode.Create);

        SaveData data = new SaveData();

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/save.sav", FileMode.Open);

            SaveData data = bf.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else return null;
    }

    public static bool SaveGameExists()
    {
        if (File.Exists(Application.persistentDataPath + "/save.sav")) return true;
        else return false;
    }

}

[Serializable]
public class PlayerData
{
    public int id;

    public int lifes;

    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public class AlienshipData
{
    public int id;

    public Vector3 postion;
    public Quaternion rotation;
}

[Serializable]
public class ScoreData
{
    public int score;
}

[Serializable]
public class Asteroid
{
    public int id;

    public Vector3 postion;
    public Quaternion rotation;
}

[Serializable]
public class AsteroidFragment
{
    public int id;

    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public class LevelData
{
    public ScoreData score;

    public AlienshipData enemy;
    public PlayerData player;
    public Asteroid[] big_obstacles;
    public AsteroidFragment[] small_obstacles;
}

[Serializable]
public class ScreenData
{
    public int x;
    public int y;
}

[Serializable]
public class UserSettingsData
{
    public ScreenData ScreenSize;
}

[Serializable]
public class SaveData
{
    public LevelData level;
    public UserSettingsData gamesettings;
}

