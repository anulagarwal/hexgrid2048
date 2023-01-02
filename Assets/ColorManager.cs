using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexColor
{
    public int val;
    public Color col;
}
public class ColorManager : MonoBehaviour
{

    [SerializeField] List<HexColor> hc;
    public static ColorManager Instance = null;


    void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

    }


   public Color GetHexColor(int h)
    {
        return hc.Find(x => x.val == h).col;
    }
}
