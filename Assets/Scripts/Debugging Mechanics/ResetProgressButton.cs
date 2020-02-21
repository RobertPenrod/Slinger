using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressButton : MonoBehaviour
{
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        SaveSystem.DeleteAllLevelData();

        SceneFader.Instance.FadeToWithSpeed("Main Menu", 5f);
    }
}
