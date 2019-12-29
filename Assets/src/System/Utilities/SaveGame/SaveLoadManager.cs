using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DATA;

public static class SaveLoadManager
{

    public static void SaveData(GameManager _gameManager)
    {
        if(!SaveFolderExists())
        {
            CreateSavesDirectory();
        }
        
        FileStream file = new FileStream(GetSavePath() + "/save.sav", FileMode.OpenOrCreate);

        SaveData data = new SaveData();

        //Debug.Log(data);

        data.gamesettings = new UserSettingsData();
        data.level = new LevelData();

        data.gamesettings.ScreenSize.x = Screen.width;
        data.gamesettings.ScreenSize.y = Screen.height;

        //player lifes
        data.level.score.score = _gameManager.CurrentPoints;
        data.level.big_obstacles_count = _gameManager._asteroidsCount;
        data.level.small_obstacles_count = _gameManager._debrisCount;

        data.level.player.position = _gameManager._playerPrefab.transform.position;
        data.level.player.rotation = _gameManager._playerPrefab.transform.rotation;

        data.level.enemy.postion = _gameManager._enemyPrefab.transform.position;
        data.level.enemy.rotation = _gameManager._enemyPrefab.transform.rotation;

        data.level.big_obstacles = _gameManager._asteroids;
        data.level.small_obstacles = _gameManager._debris;

        GetBinaryFormatter().Serialize(file, data);

        file.Close();
    }

    public static void LoadData(GameManager _gameManger)
    {
        FileStream file = new FileStream(GetSavePath() + "/save.sav", FileMode.Open);

        SaveData data = GetBinaryFormatter().Deserialize(file) as SaveData;

        file.Close();

        _gameManger._playerPrefab.transform.position = data.level.player.position;
        _gameManger._playerPrefab.transform.rotation = data.level.player.rotation;
    }

    private static void CreateSavesDirectory()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/saves");
    }

    public static string GetSavePath()
    {
        return Application.persistentDataPath + "/saves";
    }

    public static bool SaveFolderExists()
    {
        if (Directory.Exists(GetSavePath()))
            return true;
        else return false;
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }

    public static bool SaveGameExists()
    {
        if (File.Exists(Application.persistentDataPath + "/save.sav")) return true;
        else return false;
    }

}

namespace DATA
{
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
        public int big_obstacles_count;
        public int small_obstacles_count;
        public bool enemy_spawned;

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
}
