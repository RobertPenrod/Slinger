using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AutoNameLevelButton : MonoBehaviour
{
    public GameObject parentObject;

    // Update is called once per frame
    void Update()
    {
        // Set displayed text to level number
        int index = parentObject.transform.GetSiblingIndex(); // -1 to ignore first element

        if (GetComponent<Text>() != null)
        {
            GetComponent<Text>().text = (parentObject.GetComponent<Level>().number).ToString();
        }
        else if(GetComponent<TextMeshProUGUI>() != null)
        {
            GetComponent<TextMeshProUGUI>().text = (parentObject.GetComponent<Level>().number).ToString();
        }

        // Set Button name to level name
        parentObject.transform.name = "[" + (parentObject.transform.GetSiblingIndex()+1) + "] " + parentObject.transform.GetComponent<Level>().name;
    }
}
