using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int startCoins;
    [SerializeField] int currentCoins;

    [Header("Rewards")]
    [SerializeField] int levelReward;
    // Start is called before the first frame update
    void Start()
    {
        startCoins = PlayerPrefs.GetInt("coins", startCoins);
        currentCoins = startCoins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Level Rewards

    public void AddToLevelReward(int v)
    {
        levelReward += v;
        UIManager.Instance.UpdateLevelReward(levelReward);
    }

    public void MultiplyLevelReward(int v)
    {
        levelReward *= v;
        UIManager.Instance.UpdateLevelReward(levelReward);
    }

    #endregion

    #region Coin Getter Setter
    public void AddCoins(int v)
    {
        currentCoins += v;
        PlayerPrefs.SetInt("coins", currentCoins);
        UIManager.Instance.UpdateCurrentCoins(currentCoins);
    }

    public bool SubtractCoins(int v)
    {
        if (currentCoins - v > 0)
        {
            currentCoins -= v;
            PlayerPrefs.SetInt("coins", currentCoins);
            UIManager.Instance.UpdateCurrentCoins(currentCoins);

            return true;
        }
        else
        {
            return false;
        }

    }

    #endregion

}
