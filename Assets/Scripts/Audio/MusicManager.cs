using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float defaultVolume;

    private float fadeInTick;
    private float fadeOutTick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Muffle()
    {
        audioSource.volume = defaultVolume / 4.0f;//defaultVolume - 0.1f;
    }

    public void UnMuffle()
    {
        audioSource.volume = defaultVolume;
    }

    public void Mute()
    {
        audioSource.volume = 0;
    }

    public void FadeOut()
    {
        fadeOutTick = 0;
        IEnumerator fadeOut = FadeOutCoroutine(1f);
        StartCoroutine(fadeOut);
    }

    public IEnumerator FadeOutCoroutine(float seconds)
    {
        while(fadeOutTick <= seconds)
        {
            fadeOutTick += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(defaultVolume, 0, fadeOutTick / seconds);
            yield return null;
        }
        audioSource.volume = defaultVolume;
    }

    public IEnumerator FadeInCoroutine(float seconds)
    // Assuming we're fading in from zero
    {
        while (fadeInTick <= seconds)
        {
            fadeInTick += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, defaultVolume, fadeInTick / seconds);
            yield return null;
        }
        audioSource.volume = defaultVolume;
    }
}
