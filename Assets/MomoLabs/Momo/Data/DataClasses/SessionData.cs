using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionData 
{
    public int sessionNumber;
    public float sessionLength;
    public int lastLevel;

    public void Init(int sNum, float sLength, int last)
    {
        sessionNumber = sNum;
        sessionLength = sLength;
        lastLevel = last;
    }
}
