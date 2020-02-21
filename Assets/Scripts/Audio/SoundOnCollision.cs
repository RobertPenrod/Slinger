using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SoundOnCollision : MonoBehaviour
{
    public float minVelocity = 0.5f;
    public AudioSource audioSource;
    protected Collider2D collider;
    public float maxVelocity = 10;
    protected float startingVolume; // gotten from audio source

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        startingVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if(collidingObject != null)
        {
            float velocity = collision.relativeVelocity.magnitude;

            if(velocity > minVelocity)
            {
                float mult = velocity > maxVelocity ? 1f : velocity / maxVelocity;
                audioSource.volume = startingVolume * mult;
                audioSource.Play();
            }
        }
    }
}
