using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momo;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance = null;


    [Header("Attributes")]
    //Include all trackers
    //Moves, Retries, Time Spent etc. 
    [SerializeField] List<PlayerLevelData> levelsPlayed;
    [SerializeField] List<PlayerLevelData> samepleLevelsPlayed;

    [SerializeField] List<SessionData> sessions;
    [SerializeField] List<SessionData> sampleSessionData;

    [SerializeField] List<DayData> days;
    [SerializeField] List<DayData> sampleDaysData;


    [SerializeField] PlayerType playerType;
    [SerializeField] int minLevelsToAnalyze;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    //Android
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //Session closed

        }

        else if (!pause)
        {
            //Check time before last session
            //If less than 30s then continue session
            //Otherwise new session
        }
    }
    public void AddLevelData(PlayerLevelData ld)
    {
        levelsPlayed.Add(ld);
        Analytics.Instance.TrackLevel(ld);
    }
    public void AddSessionData(SessionData sd)
    {
        sessions.Add(sd);
        Analytics.Instance.TrackSession(sd);

    }
    public void RemoveSessionData(int sessionNumber)
    {
        sessions.Remove(sessions.Find(x => x.sessionNumber == sessionNumber));
    }

    public void AddDayData(DayData dd)
    {
        days.Add(dd);
        Analytics.Instance.TrackDay(dd);
    }

    public void Analyze()
    {

        foreach(PlayerLevelData pld in levelsPlayed)
        {
            int numberOfMovesDelta = pld.numberOfMoves - samepleLevelsPlayed.Find(x => x.levelNumber == pld.levelNumber).numberOfMoves;
           

            //Create array and feed everything there


            //Check if moves are above or below average
                //Above = noob = low factor
                //Below = pro = high factor
            //Check the fail rate (if any) if it is above or below average
                //High fail rate = noob = low factor
                //Low fail rate = pro = high factor

            //Using above factors calculate a result value for all levels
            //Check if there is an increasing trend
                
        }

        foreach(SessionData sd in sessions)
        {
            //Create array and feed everything there
            float sessionLength = sd.sessionLength - sampleSessionData.Find(x => x.sessionNumber == sd.sessionNumber).sessionLength;
            float sessionLevelsPlayed = sd.lastLevel - sampleSessionData.Find(x => x.sessionNumber == sd.sessionNumber).lastLevel;



            //Check if session lengths are above or below average
            //Check if levels played in a session are above or below average

            //Using above factors calculate a result value for all sessions

            //Check if from 1st session to last if there is an increase trend in delta of result value


        }

        foreach(DayData dd in days)
        {

            //Create array and feed everything there

            //Check how many sessions in a complete day
            //Compare with average
            //Check current day
            //higher the current day, better the player
        }
      
    }
}
