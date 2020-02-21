using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTimeScalePitcher : MonoBehaviour
{
    private AudioSource audioSource;
    private float startingPitch;
    private float depitchMagnitude = 0.5f;

    public float pitch;
    public bool isPitchReset;
    public bool isPitchDeformed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startingPitch = 1;
        isPitchReset = false;
        isPitchDeformed = false;
        pitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale < 0.5f)
        {
            if (!isPitchDeformed)
            {
                isPitchDeformed = true;
                isPitchReset = false;
                float timeDeform = Mathf.Abs(Time.timeScale - 1); // (0,1) how deformed time is
                float newPitch = startingPitch - depitchMagnitude * timeDeform;
                pitch = newPitch;
                audioSource.pitch = pitch;
            }
        }
        else if (!isPitchReset)
        {
            isPitchReset = true;
            isPitchDeformed = false;
            audioSource.pitch = startingPitch;
        }
    }
}
