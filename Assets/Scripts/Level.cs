using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public string name; // set by editor
    public int number;  // set by external script.
    public Transform starPanel;
    public GameObject buttonObject;

    // Variables Loaded and Saved by SaveSystem
    public int starCount = 0;   // (0-3)
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        // set interactable by default
        buttonObject.GetComponent<Button>().interactable = true;

        UpdateStarCount();
        loadData();
    }

    // Update is called once per frame
    void Update()
    {
        // Turn this off for the final game
        // Call this in star method only instead
        UpdateStarCount();
    }

    public  void loadData()
    // gets level data corresponding to the name of the level
    {
        LevelData data = SaveSystem.loadLevelStats(name);
        if(data != null)
        {
            loadData(data);
            //Debug.Log(locked);
        }
        else
        // File did not exist.
        // Make new Level file with default data
        {
            Debug.Log("Creating level data for level " + name);
            LevelData newData = new LevelData(0, true);
            SaveSystem.SaveLevelStats(name, newData.starCount, newData.locked);
            loadData(newData);
        }

        if(locked)
        // Lock Level
        {
            Lock();
        }
    }

    private void loadData(LevelData data)
    // loads level data within data.
    {
        starCount = data.starCount;
        locked = data.locked;
    }

    public void Lock()
    {
        locked = true;
        buttonObject.GetComponent<Button>().interactable = false;
    }

    public void Unlock()
    {
        locked = false;
        buttonObject.GetComponent<Button>().interactable = true;
        // Unlock level in save system
        SaveSystem.UnlockLevel(name);
    }

    private void UpdateStarCount()
    {
        for (int i = 0; i < starPanel.childCount; i++)
        {
            // Dissable star
            starPanel.GetChild(i).gameObject.GetComponent<Image>().enabled = false;

            // Reenable Star
            if (starCount > i)
                starPanel.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
