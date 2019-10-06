using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip startClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startClip);
        audioSource.PlayScheduled(AudioSettings.dspTime + startClip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
