using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StarCount : MonoBehaviour
{
    public int starCount = 0;
    public Transform starPanel;

    private void Start()
    {
        UpdateStarCount();
    }

    // Update is called once per frame
    void Update()
    {
        // Turn this off for the final game
        // Call this in star method only instead
        UpdateStarCount();
    }

    public void UpdateStarCount()
    {
        for(int i = 0; i < starPanel.childCount; i++)
        {
            // Dissable star
            starPanel.GetChild(i).gameObject.GetComponent<Image>().enabled = false;

            // Reenable Star
            if (starCount > i)
                starPanel.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
