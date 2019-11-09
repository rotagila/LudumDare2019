using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{

    AudioSource audioSource;
    AudioSource startSource;

    bool startedLoop = false;
    //public AudioClip startClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>()[0];
        startSource = GetComponents<AudioSource>()[1];

        //startSource.Play();
        //audioSource.PlayScheduled(AudioSettings.dspTime + startClip.length);
    }

    void FixedUpdate()
    {
        if (!startSource.isPlaying && !startedLoop)
        {
            audioSource.Play();
            startedLoop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
