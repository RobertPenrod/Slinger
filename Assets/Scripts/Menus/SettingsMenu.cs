using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject settingsMenuUI;
    public GameObject AFIE;
    public GameObject settingsButton;

    private void Start()
    {
        GameIsPaused = false;
    }

    private void Update()
    {
        bool afieAlive = AFIE.GetComponent<AFIE>().alive;
        settingsButton.GetComponent<Button>().interactable = afieAlive;
        settingsButton.GetComponent<EventTrigger>().enabled = afieAlive;
    }

    public void Resume()
    {
        // Enable AFIE's bullet time script
        AFIE.GetComponent<SlingBulletTime>().enabled = true;
        // Enable AFIE input
        AFIE.GetComponent<AFIE>().EnableInput();

        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameManager.Instance.menuClosed();
    }

    public void Pause()
    {
        // Disable AFIE's bullet time script
        AFIE.GetComponent<SlingBulletTime>().enabled = false;
        // Disable AFIE input
        AFIE.GetComponent<AFIE>().DisableInput();

        settingsMenuUI.SetActive(true);
        Time.timeScale = 0f;    // Pause physics using timescale
        GameIsPaused = true;

        GameManager.Instance.menuOpened();
    }

    public void BackToMap()
    {
        GameIsPaused = false;
        SceneFader.Instance.FadeTo("Level Select");
        Debug.Log("Settings Menu Fading to Level Select");
    }
}
