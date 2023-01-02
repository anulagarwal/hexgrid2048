using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sounds
    {
        public AudioClip sound;
        public Sound type;
    }
    public static SoundManager Instance = null;

    [SerializeField] AudioSource source;
    [SerializeField] List<Sounds> sounds;


    private void Awake()
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play( Sound type)
    {
        source.clip = sounds.Find(x => x.type == type).sound;
        source.Play();
    }
}
