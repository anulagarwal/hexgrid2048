using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] List<string> tips;
    [SerializeField] float waitTime;


    [Header("Component References")]
    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] SceneHandler sh;
    // Start is called before the first frame update
    void Start()
    {
        tipText.text = tips[Random.Range(0, tips.Count)];
        Invoke("ChangeScene", waitTime);
    }

   void ChangeScene()
    {
        sh.enabled = true;
    }
}
