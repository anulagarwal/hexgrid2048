using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FingerFollow : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public GameObject finger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            finger.GetComponent<Image>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            finger.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            finger.GetComponent<Image>().enabled = false;
        }
    }
}
