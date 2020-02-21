using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AFIE : MonoBehaviour
{
    public bool fancyDeath;
    public bool alive = true;
    public GameObject bodyGraphics;
    public GameObject trajectoryGraphics;
    public AudioSource deathSound;
    public AudioSource bounceSound;
    public AudioSource slingSound;
    public TrailRenderer trailRenderer;
    public ParticleSystem deathParticles;
    private Rigidbody2D rigidbody;

    private SlingInputHandler inputHandler;
    private SlingMovementHandler movementHandler;

    private bool restartCalled = false;

    public bool inAntiSlingField = false; // for anti-sling field handling

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        inputHandler = GetComponent<SlingInputHandler>();
        movementHandler = GetComponent<SlingMovementHandler>();
        rigidbody = GetComponent<Rigidbody2D>();
        restartCalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive && !SettingsMenu.GameIsPaused && ! restartCalled)
        {
            Debug.Log("Afie Handling Death");
            restartCalled = true;

            if (fancyDeath)
                SceneFader.Instance.FadeGlitch(SceneManager.GetActiveScene().name);
            else
                SceneFader.Instance.FadeTo(SceneManager.GetActiveScene().name);
        }
    }

    public void kill()
    {
        restartCalled = false;
        alive = false;
        deathSound.Play();

        // hide graphics
        bodyGraphics.SetActive(false);
        trailRenderer.enabled = false;
        trajectoryGraphics.SetActive(false);

        // stop input
        movementHandler.canSling = false;
        inputHandler.enabled = false;

        // stop physics
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<CircleCollider2D>().enabled = false;

        // mute audio
        bounceSound.enabled = false;
        slingSound.enabled = false;

        // Death Particles
        deathParticles.Play();
    }

    public void DisableInput()
    {
        GetComponent<SlingMovementHandler>().canSling = false;
        GetComponent<SlingInputGraphics>().slingGraphicsObject.SetActive(false);
        GetComponent<SlingTrajectoryGraphics>().lineRenderer.gameObject.SetActive(false);
    }

    public void EnableInput()
    {
        if (!inAntiSlingField)
        {
            GetComponent<SlingMovementHandler>().canSling = true;
            GetComponent<SlingInputGraphics>().slingGraphicsObject.SetActive(true);
            GetComponent<SlingTrajectoryGraphics>().lineRenderer.gameObject.SetActive(true);
        }
    }
}
