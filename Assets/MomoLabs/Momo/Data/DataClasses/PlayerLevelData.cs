using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{

    public int levelNumber;
    public int retries;
    public bool win;
    public int numberOfMoves;
    public float timeSpent;

    public void Init(int level, int retry, bool didWin, int moves, float time)
    {
        levelNumber = level;
        retries = retry;
        win = didWin;
        numberOfMoves = moves;
        timeSpent = time;
    }
}
