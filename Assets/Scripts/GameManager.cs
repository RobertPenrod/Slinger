using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool playDeathSound;
    public bool muteMusicOnDeath;
    private bool isLevelWon;
    public bool isMenuOpen;
    public GameObject endOfLevelMenu;
    public AudioSource winLevelAudio;
    public GameObject AFIE;
    public GameObject slingEnergyUIObject;
    public StarAnimationHandler starAnimationHandler;
    public AudioSource deathSoundMusic;

    public static GameManager Instance { get; private set; }

    private bool handledDeath = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isLevelWon = false;
        isMenuOpen = false;

        // Add 2 slings to AFIE for 2 and 1 star level completion.
        AFIE.GetComponent<SlingMovementHandler>().slingEnergy += 2;
        //slingEnergyUIObject.GetComponent<SlingEnergyUI>().UpdateSlingEnergyUI();

        Time.timeScale = 1;
        // Resume Music
        MusicManager.Instance.UnMuffle();
        //Debug.Log("Unmuffling music");
    }

    // Update is called once per frame
    void Update()
    {
        if(!AFIE.GetComponent<AFIE>().alive && !handledDeath)
        {
            handledDeath = true;

            if (muteMusicOnDeath)
            {
                // Mute music manager
                if (MusicManager.Instance != null)
                    MusicManager.Instance.Mute();
            }

            // Play death sound
            if (playDeathSound)
            {
                deathSoundMusic.Play();
                Debug.Log("Playing death sound music");
            }
        }
    }

    public IEnumerator WinLevel()
    {
        Debug.Log("WinLevel called");
        int currentLevelReached = PlayerPrefs.GetInt("levelReached");
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        int nextLevel = currentLevel + 1;
        if(nextLevel > currentLevelReached)
            PlayerPrefs.SetInt("levelReached", nextLevel);
        //SceneFader.Instance.FadeTo("Level Select");

        // Save Level Data
        string currentLevelName = levelNames()[currentLevel - 1];
        string nextLevelName = levelNames()[nextLevel - 1];
        // Only save stars if earned stars is greater than previously earned stars
        int previousStarsEarned = SaveSystem.LoadLevelStars(currentLevelName);

        // Determined Stars Earned
        int starsEarned = 0;
        int slingEnergyLeft = AFIE.GetComponent<SlingMovementHandler>().slingEnergy;
        if(slingEnergyLeft >= 2)
        {
            starsEarned = 3;
        }
        else if(slingEnergyLeft >= 1)
        {
            starsEarned = 2;
        }
        else
        {
            starsEarned = 1;
        }
        
        
        if (starsEarned > previousStarsEarned)
            SaveSystem.SaveLevelStars(currentLevelName, starsEarned);

        // Unlock Next Level
        SaveSystem.UnlockLevel(nextLevelName);

        // Mute music
        if(MusicManager.Instance != null)
            MusicManager.Instance.Muffle();
        // Play win audio
        winLevelAudio.Play();

        isLevelWon = true;

        // Display end of level menu
        endOfLevelMenu.SetActive(true);
        endOfLevelMenu.GetComponent<Animator>().SetBool("Level Won", true);
        menuOpened();

        // Wait for end of level menu to display
        while(!endOfLevelMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Displaying"))
        {
            yield return null;
        }

        // Trigger Star Animations
        StartCoroutine(starAnimationHandler.ShowStars(starsEarned));

        // Wait for star audio to finish
        while(!starAnimationHandler.isAudioDone)
        {
            yield return null;
        }

        if(MusicManager.Instance != null)
            MusicManager.Instance.UnMuffle();
    }

    public void menuOpened()
    {
        isMenuOpen = true;
        DisableAFIEInput();
    }

    public void menuClosed()
    {
        isMenuOpen = false;
        EnableAFIEInput();
    }

    public void DisableAFIEInput()
    {
        AFIE.GetComponent<AfieResetInputHandler>().enabled = false;
        AFIE.GetComponent<AFIE>().DisableInput();
    }

    public void EnableAFIEInput()
    {
        AFIE.GetComponent<AfieResetInputHandler>().enabled = true;
        AFIE.GetComponent<AFIE>().EnableInput();
    }

    private string[] levelNames()
    {
        string levelNames = PlayerPrefs.GetString("LevelNames");    // Set in LevelButtonHandler
        levelNames += "Level Select"; // if there is not a next level return the player to level select.
        string[] namesArray = levelNames.Split(',');
        return namesArray;
    }
}
