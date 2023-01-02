using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopupText : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float upSpeed;
    [SerializeField] float fadeSpeed;
    [SerializeField] float destroyTime;

    [Header("Component References")]
    [SerializeField] TextMeshPro tm;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        tm.alpha -= fadeSpeed;
    }
}
