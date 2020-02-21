using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    public Animator animator;

    private string levelToLoad;
    private float defaultAnimatorSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            defaultAnimatorSpeed = animator.speed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeTo(string levelName)
    {
        Debug.Log("Fading to " + levelName);
        animator.speed = defaultAnimatorSpeed;
        levelToLoad = levelName;
        animator.ResetTrigger("FadeIn");
        animator.SetTrigger("FadeOut");
    }

    public IEnumerator FadeToAfterSeconds(string levelName, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        FadeTo(levelName);
    }

    public void FadeGlitch(string levelName)
    {
        Debug.Log("Fading Glitching to " + levelName);
        levelToLoad = levelName;

        IEnumerator slomo = slowTime(0.1f, 2f);
        StartCoroutine(slomo);

        IEnumerator glitchAfterSeconds = GlitchAfterSeconds(1.5f);
        StartCoroutine(glitchAfterSeconds);

        IEnumerator fadeToAfterSeconds = FadeToAfterSeconds(levelName, 1.8f);
        StartCoroutine(fadeToAfterSeconds);
    }

    public IEnumerator GlitchAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        GlitchEffect.Instance.activateMax();
    }

    public IEnumerator slowTime(float target, float timeToSlow)
    // Assumes time scale is starting at 1
    {
        // 1 -> target in timeToSlow seconds
        while (true)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, target, 0.05f);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
    }

    public void GlitchTo(string levelName)
    {
        Debug.Log("Glitching to " + levelName);
        levelToLoad = levelName;
        GlitchEffect.Instance.activateMax();
        IEnumerator coroutine = glitchForSeconds(2);
        StartCoroutine(coroutine);
    }

    public IEnumerator glitchForSeconds(float seconds)
    {
        Debug.Log("Slowing Time Scale");
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(seconds);
        OnGlitchComplete();
    }


    public void FadeToWithSpeed(string levelName, float speed)
    {
        animator.speed = speed;
        levelToLoad = levelName;
        animator.ResetTrigger("FadeIn");
        animator.SetTrigger("FadeOut");
    }

    public void OnGlitchComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.ResetTrigger("FadeOut");
        //animator.SetTrigger("FadeIn");
        Time.timeScale = 1f; // return time scale to normal
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.ResetTrigger("FadeOut");
        animator.SetTrigger("FadeIn");
        Time.timeScale = 1f; // return time scale to normal
    }

    public void RestartLevel()
    {
        FadeTo(currentLevelName());
    }
    
    public void FadeToNextLevel()
    {
        // adjust current level
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        FadeTo(currentLevelName());
    }

    private string currentLevelName()
    {
        int levelNumber = PlayerPrefs.GetInt("CurrentLevel");
        int index = levelNumber - 1;
        return levelNames()[index];
    }

    private string[] levelNames()
    {
        string levelNames = PlayerPrefs.GetString("LevelNames");    // Set in LevelButtonHandler
        levelNames += "Level Select"; // if there is not a next level return the player to level select.
        string[] namesArray = levelNames.Split(',');
        return namesArray;
    }
}
