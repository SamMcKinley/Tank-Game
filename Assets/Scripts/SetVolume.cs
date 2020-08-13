using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    private AudioSource BackgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic = gameObject.GetComponent<AudioSource>();
        BackgroundMusic.volume = GameManager.Instance.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
