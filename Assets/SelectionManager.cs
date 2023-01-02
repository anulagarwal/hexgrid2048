using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int maxBox;
    [SerializeField] int spawnIndex;

    [Header("Component References")]
    [SerializeField] List<GameObject> tiles;
    [SerializeField] List<Transform> boxPoints;
    [SerializeField] List<GameObject> spawnedTiles;

    public static SelectionManager Instance = null;


    void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

    }

        // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i< maxBox; i++)
        {
            Spawn(boxPoints.Find(x => x.childCount == 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    public void Spawn(Transform t)
    {
        GameObject g = Instantiate(tiles[spawnIndex], t.position, Quaternion.identity);
        g.transform.SetParent(t);
        spawnedTiles.Add(g);
        spawnIndex++;
        if(spawnIndex == tiles.Count)
        {
            spawnIndex = 0;
        }
    }

    public void RemoveTile(GameObject g)
    {
        Spawn(boxPoints.Find(x => x.GetChild(0).gameObject == g));
        g.transform.SetParent(null);

        //Destroy(g);
        spawnedTiles.Remove(g);
    }


}
