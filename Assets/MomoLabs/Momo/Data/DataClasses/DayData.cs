using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayData
{
    public int DayNumber;
    public int numberOfSessions;

    public void Init(int dNum, int numSess)
    {
        DayNumber = dNum;
        numberOfSessions = numSess;
    }
}
