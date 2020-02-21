using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDepitcherApplyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
   

        for(int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].gameObject.tag.Equals("No Audio Effects"))
            {
                if (audioSources[i].gameObject.GetComponent<AudioTimeScalePitcher>() == null)
                // If the object does not already have these componenets.
                {
                    audioSources[i].gameObject.AddComponent<AudioTimeScalePitcher>();
                }
            }
        }
    }
}
