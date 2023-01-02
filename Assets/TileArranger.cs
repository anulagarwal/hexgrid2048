using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class TileArranger : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] List<Tile> tiles;
    private void OnEnable()
    {       
        foreach(Tile t in tiles)
        {
//            t.Highlight(ColorManager.Instance.GetHexColor(t.hexType));
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
