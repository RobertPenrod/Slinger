using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartPressed()
    {
        SceneFader.Instance.FadeTo("Level Select");
    }

    public void SettingsPressed()
    {

    }

    public void CreditsPressed()
    {
        SceneFader.Instance.FadeTo("Credits");
    }
}
