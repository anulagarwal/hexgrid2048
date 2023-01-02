using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class DataManager : MonoBehaviour
{

    [Header("Attributes - Session")]
    [SerializeField] float sessionStartTime;
    [SerializeField] int sessionEndTime;
    [SerializeField] float sessionMinimumDifference;


    [SerializeField] int sessionNumber;

    [Header("Attributes - Day")]
    [SerializeField] int dayNumber;
    [SerializeField] int dayTimeDifference;
    // Start is called before the first frame update
    void Start()
    {
        sessionNumber = PlayerPrefs.GetInt("session", 1);
        dayNumber = PlayerPrefs.GetInt("day", 0);

        int openTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        int lastOpenTime = PlayerPrefs.GetInt("sessionEnd", (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);

        if(openTime - lastOpenTime >= dayTimeDifference)
        {
            //New Day - 24 hrs passed by           
            //Send Data
            dayNumber++;
            PlayerPrefs.SetInt("day", dayNumber);
            DayData dd = new DayData();
            dd.Init(dayNumber, sessionNumber);
            PlayerManager.Instance.AddDayData(dd);
        }


    }

    // Update is called once per frame
    void Update()
    {
              

    }
    //Check for day if it is past 24 hrs or past midnight 12

    //For Session
    //Also time difference between quit and open
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            UIManager.Instance.UpdateDebugText("Session: pp" );

            SessionData sd = new SessionData();
            float sessionLength = Time.time - sessionStartTime;
            sd.Init(sessionNumber, sessionLength, GameManager.Instance.GetCurrentLevel());
            PlayerManager.Instance.AddSessionData(sd);

            sessionEndTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            PlayerPrefs.SetInt("sessionEnd", sessionEndTime);
        }
        else
        {

            int openTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            int lastOpen = PlayerPrefs.GetInt("sessionEnd", (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
            UIManager.Instance.UpdateDebugText(openTime - lastOpen+"");

            if (openTime - lastOpen >= sessionMinimumDifference)
            {
                sessionNumber++;
                UIManager.Instance.UpdateDebugText("Session: " + sessionNumber);
                PlayerPrefs.SetInt("session", sessionNumber);
                sessionStartTime = Time.time;

                if (openTime-lastOpen > dayTimeDifference)
                {
                    //New Day
                    
                }
            }
            else
            {
                sessionNumber = PlayerPrefs.GetInt("session", 1);
                PlayerManager.Instance.RemoveSessionData(sessionNumber);
            }
        }
    }
}
