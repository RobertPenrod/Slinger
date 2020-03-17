using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitReportButton : MonoBehaviour
{
    public AudioSource audio;

    public void GoToCredits()
    {
        if(audio != null)
            audio.Play();
        SceneFader.Instance.FadeTo("Credits");
    }
}
