using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class GoalLocationOnText : MonoBehaviour
// Assumes it is placed on text mesh conatining object
// and that its parent is a goal object
{
    // Start is called before the first frame update
    void Update()
    {
        string nextSceneName = transform.parent.GetComponent<LevelHandlingGoal>().nextScene;
        GetComponent<TextMeshPro>().text = nextSceneName;
    }

}
