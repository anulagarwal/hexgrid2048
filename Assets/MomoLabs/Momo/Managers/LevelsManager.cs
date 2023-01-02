using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance = null;


    [Header("Attributes")]
    //Custom level class with difficulty level
    [SerializeField] List<LevelData> defaultLevelProgression;
    [SerializeField] List<LevelData> proLevelProgression;
    [SerializeField] List<LevelData> noobLevelProgression;
    [SerializeField] int currentLevel;




    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public LevelData RetrieveLevel(int levelNumber, PlayerType pt)
    {
        return defaultLevelProgression.Find(x => x.levelNumber == levelNumber);
    }
    
}
