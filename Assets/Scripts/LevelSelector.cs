using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelButtonHandler LevelButtonHandler;
    public bool playLevelSelectSound;
    public AudioSource levelSelectSound;

    // Start is called before the first frame update
    void Start()
    {
        // Set OnClick event for all buttons.
        // Also store all level names in a string for ease of level switching
        // in end of level menu.
        SetOnClickEvents();
    }

    public void Select(string levelName, int levelNumber)
    // Fades to the level of the given name
    {
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.SetString("LevelName", levelName);
        SceneFader.Instance.FadeTo(levelName);

        // Play button sounds if applicable
        if(playLevelSelectSound)
        {
            levelSelectSound.Play();
        }
    }

    public void GoBack()
    // Returns to Main Menu.
    {
        SceneFader.Instance.FadeTo("Main Menu");
    }

    private void SetOnClickEvents()
    {
        // Set OnClick event for all buttons.
        // Also store all level names in a string for ease of level switching
        // in end of level menu.
        string levelNames = "";
        List<GameObject> buttons = LevelButtonHandler.GetLevelButtons();
        for (int i = 0; i < buttons.Count; i++)
        {
            // Get Level Name
            string levelName = buttons[i].GetComponent<Level>().name;
            levelNames += levelName + ",";

            // Get Level Number
            int levelNumber = i + 1;

            // Set On Click Event
            Button button = buttons[i].GetComponent<Level>().buttonObject.GetComponent<Button>();
            button.onClick.AddListener(delegate { Select(levelName, levelNumber); });
        }
    }
}
