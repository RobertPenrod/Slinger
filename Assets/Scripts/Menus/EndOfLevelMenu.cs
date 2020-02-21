using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelMenu : MonoBehaviour
{
    public void LevelSelect()
    {
        SceneFader.Instance.FadeTo("Level Select");
    }

    public void RestartLevel()
    {
        SceneFader.Instance.RestartLevel();
    }

    public void NextLevel()
    {
        SceneFader.Instance.FadeToNextLevel();
    }
}
