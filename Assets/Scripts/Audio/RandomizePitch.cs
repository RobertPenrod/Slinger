using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomizePitch : MonoBehaviour
{
    public float detuneMagnitude;

    // Automatically acquired
    private AudioSource audioSource;
    private float defaultPitch;
    private bool depitched;
    public bool timeDeformed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultPitch = audioSource.pitch;
        timeDeformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAudioDepitcher();

        if(audioSource.isPlaying)
        {
            if (!depitched)
            {
                if (!timeDeformed)
                // Random default pitch
                {
                    audioSource.pitch = defaultPitch + Random.Range(-detuneMagnitude * 0.5f, detuneMagnitude * 0.5f);
                }
                else
                // Random timeDeformed pitch
                {
                    float deformedPitch = GetComponent<AudioTimeScalePitcher>().pitch;
                    audioSource.pitch = deformedPitch + Random.Range(-detuneMagnitude * 0.5f, detuneMagnitude * 0.5f);
                }

                depitched = true; // Commenting this results in random pitching durring collision
                // It sounds kinda cool.
            }
        }
        else
        {
            depitched = false;
        }
    }

    public void CheckAudioDepitcher()
    {
        AudioTimeScalePitcher timeScalePitcher = GetComponent<AudioTimeScalePitcher>();
        if(timeScalePitcher != null)
        {
            if(timeScalePitcher.isPitchDeformed)
            // Pitch is deformed
            {
                timeDeformed = true;
            }
            else if(timeScalePitcher.isPitchReset)
            // Pitch is normal
            {
                timeDeformed = false;
            }
        }
    }
}
