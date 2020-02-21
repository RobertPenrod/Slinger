using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLowpassApplier : MonoBehaviour
{
    public float cutoffFrequency;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].gameObject.tag.Equals("No Audio Effects"))
            {
                if (audioSources[i].gameObject.GetComponent<AudioLowPassFilter>() == null && audioSources[i].gameObject.GetComponent<AudioTimeScaleLowpasser>() == null)
                // If the object does not already have these componenets.
                {
                    audioSources[i].gameObject.AddComponent<AudioLowPassFilter>();
                    audioSources[i].gameObject.AddComponent<AudioTimeScaleLowpasser>();
                    audioSources[i].gameObject.GetComponent<AudioTimeScaleLowpasser>().cutoffFrequency = cutoffFrequency;
                }
            }
        }
    }
}
