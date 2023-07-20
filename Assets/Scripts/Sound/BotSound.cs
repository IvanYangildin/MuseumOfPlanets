using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotSound : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ProcessSound(float v)
    {
        if (v == 0)
        {
            if (source.isPlaying)
                source.Pause();
        }
        else
        {
            if (source.isPlaying)
                source.UnPause();
            else
                source.Play();
        }
    }
}
