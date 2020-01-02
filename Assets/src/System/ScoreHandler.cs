using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Score
{
    private int points;

    public void AddScore(int amount)
    {
        points += amount;
    }

    public int GetScore()
    {
        return points;
    }

    public void ResetScore()
    {
        points = 0;
    }
}
