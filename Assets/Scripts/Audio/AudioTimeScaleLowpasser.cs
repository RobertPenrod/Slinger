using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimeScaleLowpasser : MonoBehaviour
{
    private AudioLowPassFilter filter;
    public float cutoffFrequency; // set by applier.

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<AudioLowPassFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0.5f)
        {
            //float timeDeform = Mathf.Abs(Time.timeScale - 1); // (0,1) how deformed time is
            filter.cutoffFrequency = cutoffFrequency;
        }
        else
        {
            filter.cutoffFrequency = 22000;
        }
    }
}
