using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAwakener : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
