using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{

    public ScoreData actualscore;

    public PlayerData player;

    public List<AsteroidData> obstacles;

    public List<AsteroidFragmentData> obstclesfragments;

}
