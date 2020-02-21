using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script destroys the object it is attatched to if another object
// with the "Player" tag collides with this object.
[RequireComponent(typeof(Collider2D))]
public class DestroyOnPlayerBounce : MonoBehaviour
{
    private GameObject DestroyParticleObject;
    private GameObject audioSourceObject;

    private void Start()
    {
        DestroyParticleObject = Resources.Load<GameObject>("Particle Objects/Destroyed Surface Particles");
        audioSourceObject = Resources.Load<GameObject>("Audio Objects/Destroyed Surface Audio");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collidingObject)
    {
        if(collidingObject.tag.Equals("Player"))
        {
            // Create Destroy Particle Object
            GameObject particleObject = Instantiate(DestroyParticleObject);
            particleObject.transform.position = transform.position;
            particleObject.transform.localScale = transform.localScale;
            particleObject.transform.rotation = transform.rotation;

            GameObject audioSource = Instantiate(audioSourceObject);
            audioSource.AddComponent<AudioLowPassFilter>();
            audioSource.AddComponent<AudioTimeScaleLowpasser>();
            audioSource.GetComponent<AudioTimeScaleLowpasser>().cutoffFrequency = 500;

            // Destroy this game object
            Destroy(this.gameObject);
        }
    }
}
