﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LevelButtonHandler : MonoBehaviour
// This script locks and numbers the level buttons. 
// It contains a public method "GetLevelButtons()" that
// returns a list of all of the level buttons.
{
    public GameObject levelHolder;
    public GameObject levelPanelPrefab;

    private void Start()
    {
        LoadLevelsIntoPanels();

        // Unlock First Level
        Level firstLevel = GetLevelButtons()[0].GetComponent<Level>();
        firstLevel.loadData();
        firstLevel.Unlock();

        //lockLevelButtons();   // this should now be handled by level
        numberLevels();

        // Get Level Names seperated by commas and set player prefs
        string levelNames = "";
        foreach(GameObject levelButton in GetLevelButtons())
        {
            levelNames += levelButton.GetComponent<Level>().name + ",";
        }

        PlayerPrefs.SetString("LevelNames", levelNames);

        GetComponent<PageSwiper>().init();
    }

    private void LoadLevelsIntoPanels()
    // Moves all of the levels in levelHolder into their corresponding
    // positions in the level panels.
    {
        // Get a list of the level buttons from the level button holder.
        int levelCount = levelHolder.transform.childCount;
        Transform[] levelButtons = new Transform[levelCount];
        for(int i = 0; i < levelCount; i++)
        {
            levelButtons[i] = levelHolder.transform.GetChild(i);
        }

        /*
        // Load levelButtons into their correct locations.
        int buttonIndex = 0;
        int buttonsPerRow = 5;
        int rowsPerPanel = 3;
        for (int panel = 0; panel < transform.childCount; panel++)
        {
            Transform panelTransform = transform.GetChild(panel);
            for (int row = 0; row < panelTransform.childCount; row++)
            {
                Transform rowTransform = panelTransform.GetChild(row);
                for (int b = 0; b < buttonsPerRow; b++)
                {
                    if (buttonIndex < levelButtons.Length)
                    {
                        levelButtons[buttonIndex].transform.SetParent(rowTransform);
                        buttonIndex++;
                    }
                    else break;
                }
            }
        }
        */

        int buttonsPerRow = 5;
        int rowsPerPanel = 3;

        for (int buttonIndex = 0; buttonIndex < levelCount; buttonIndex++)
        {
            // Calculate buttons position
            int rowIndex = buttonIndex / buttonsPerRow;
            int panelIndex = rowIndex / rowsPerPanel;
            // Get rowIndex relative to panel it is in.
            while(rowIndex >= 3)
            {
                rowIndex -= 3;
            }

            // Check if we need to make a new panel
            int panelCount = this.transform.childCount;
            if (panelIndex > panelCount-1)
            {
                // Create new panel
                GameObject newPanel = Instantiate(levelPanelPrefab, this.transform);
            }

            // Move button to position
            Transform panelTransform = this.transform.GetChild(panelIndex);
            Transform rowTransform = panelTransform.GetChild(rowIndex);
            levelButtons[buttonIndex].transform.SetParent(rowTransform);
            // Set scale of button
            levelButtons[buttonIndex].GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        }
    }

    private void Update()
    {
        // Disable this call in the final version,
        // this should only be called on start then.
        numberLevels();
    }

    public List<GameObject> GetLevelButtons()
    // Returns a list of the Level Button Game Obejcts.
    {
        List<GameObject> buttonList = new List<GameObject>();
        for (int panel = 0; panel < transform.childCount; panel++)
        {
            Transform panelTransform = transform.GetChild(panel);
            for (int row = 0; row < panelTransform.childCount; row++)
            {
                Transform rowTransform = panelTransform.GetChild(row);
                for (int button = 0; button < rowTransform.childCount; button++)
                {
                    buttonList.Add(rowTransform.GetChild(button).gameObject);
                }
            }
        }
        return buttonList;
    }

    private void numberLevels()
    {
        List<GameObject> buttons = GetLevelButtons();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Level>().number = i + 1;
        }
    }

    private void lockLevelButtons()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");
        List<GameObject> buttons = GetLevelButtons();
        for(int i = 0; i < buttons.Count; i++)
        {
            int levelNumber = i + 1;
            if(levelNumber > levelReached)
            // Lock Level
            {
                buttons[i].GetComponent<Level>().Lock();
            }
        }
    }
}
