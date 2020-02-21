using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessSceneFader : MonoBehaviour
{
    public void FadeTo(string sceneName)
    {
        SceneFader.Instance.FadeTo(sceneName);
    }
}
