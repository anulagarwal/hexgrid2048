using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : Tile
{
    [Header("Attributes")]
    [SerializeField] int value;
    [SerializeField] HexType type;

    [Header("Component References")]
    [SerializeField] Tile parentTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
