using UnityEngine;
using UnityEngine.UI;

public class OldLevelSelector : MonoBehaviour
{
    public GameObject buttonContainer;
    private GameObject[] levelButtons;

    public void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // Get Buttons
        int childCount = buttonContainer.transform.childCount;
        levelButtons = new GameObject[childCount]; // 2 empty transforms to offset scroll rect.
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i] = buttonContainer.transform.GetChild(i).gameObject;
        }

        // Disable locked levels
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].GetComponent<Button>().interactable = false;
        }

        // Set OnClick even for all buttons.
        // Also store all level names in a string for ease of level switching in end of level menus.
        string levelNames = "";
        for(int i = 0; i < levelButtons.Length; i++)
        {
            string levelName = levelButtons[i].GetComponent<LevelName>().name;

            levelNames += levelName + ",";

            int levelNumber = i + 1;
            levelButtons[i].GetComponent<Button>().onClick.AddListener(delegate { Select(levelName, levelNumber); });
        }

        PlayerPrefs.SetString("LevelNames", levelNames);
    }

    public void Select(string levelName, int levelNumber)
    // Fades to the level of the given name.
    {
        //Debug.Log("Current Level: " + levelNumber);
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        PlayerPrefs.SetString("LevelName", levelName);
        SceneFader.Instance.FadeTo(levelName);
        
    }

    public void GoBack()
    // Returns to Main Menu.
    {
        SceneFader.Instance.FadeTo("Main Menu");
    }
}
