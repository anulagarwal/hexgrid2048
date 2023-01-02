using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] GameObject waterDrop;
    [SerializeField] Transform dropsParent;


    [Header("Attributes")]
    [SerializeField] float interval;
    [SerializeField] float randomRadius;
    [SerializeField] int number;
    [SerializeField] int maxDrops;

    int currentDrops;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartSpawn()
    {
        while (currentDrops < maxDrops)
        {
            for (int i = 0; i < number; i++)
            {
                Spawn(new Vector3(Random.Range(transform.position.x - randomRadius, transform.position.x + randomRadius), Random.Range(transform.position.y - randomRadius, transform.position.y + randomRadius), 0));
            }

            yield return new WaitForSeconds(interval);
        }
    }

    public void Spawn(Vector3 pos)
    {
        GameObject g= Instantiate(waterDrop, pos, Quaternion.identity);
        currentDrops++;
    }
}
