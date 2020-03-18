using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Collectable : MonoBehaviour
{
    public Color startColor;
    public Color grabbedColor;
    public float followDistance = 5;
    public float moveForce = 5;
    public AudioSource pickupAudioSource;
    public AudioSource ambientAudioSource;
    public bool collected;        

    private GameObject playerObject; // set when player "grabs" object.
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Remove this update method in final version.
    private void Update()
    {
        if(!collected)
            GetComponent<SpriteRenderer>().color = startColor;
    }

    private void Start()
    {
        collected = false;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (collected)
        // If player has collected collectable
        {
            if (playerObject.GetComponent<AFIE>().alive)
            // Player is alive.
            {
                // have collectable follow player
                followPlayer();
            }
            else
            // Player is dead.
            {
                unGrab();
            }
        }
        else
        {
            if (rigidBody.velocity.magnitude >= 0)
            // If moving, bring to stop.
            {
                applyStoppingForce();
            }
        }
    }

    private void followPlayer()
    {
        Vector2 displacement = playerObject.transform.position - transform.position;
        if(displacement.magnitude > followDistance)
        // Move towards player
        {
            rigidBody.AddForce(displacement.normalized * moveForce);
            applyStoppingForce();
        }
        else
        // Stop witin radius of player
        {
            applyStoppingForce();
        }
    }

    private void applyStoppingForce()
    {
        Vector2 velocity = rigidBody.velocity;
        rigidBody.AddForce(-velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        // If player has collided with collectable
        {
            if (collision.gameObject.GetComponent<AFIE>().alive)
            // If player is alive.
            {
                playerObject = collision.gameObject;
                grabbed();
            }
        }
    }

    private void grabbed()
    {
        if (!collected)
        {
            collected = true;
            animator.SetBool("Captured", true);
            pickupAudioSource.Play();
            // Play ambience after pickup
            //ambientAudioSource.PlayDelayed(pickupAudioSource.clip.length);
            ambientAudioSource.PlayDelayed(0.1f);

            // set color
            StartCoroutine("changeColor");
        }
    }

    private void unGrab()
    {
        collected = false;
        animator.SetBool("Captured", false);
        ambientAudioSource.Stop();
    }

    IEnumerator changeColor()
    {
        bool colorChanged = false;
        float tick = 0;
        float timeToChange = 0.5f;
        while(!colorChanged)
        {
            tick += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, grabbedColor, tick / timeToChange);
            yield return null;
        }
    }
}
