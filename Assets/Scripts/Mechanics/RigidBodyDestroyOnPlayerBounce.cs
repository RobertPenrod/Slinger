using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBodyDestroyOnPlayerBounce : MonoBehaviour
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
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.tag.Equals("Player"))
        {
            // Create Particle Objects for each child
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                GameObject newParticle = Instantiate(DestroyParticleObject);
                newParticle.transform.position = child.position;
                newParticle.transform.localScale = child.localScale;
                newParticle.transform.rotation = child.rotation;
            }

            // Create Audio Object
            GameObject audioSource = Instantiate(audioSourceObject);
            audioSource.AddComponent<AudioLowPassFilter>();
            audioSource.AddComponent<AudioTimeScaleLowpasser>();
            audioSource.GetComponent<AudioTimeScaleLowpasser>().cutoffFrequency = 500;

            // Destroy this object
            Destroy(this.gameObject);
        }
    }
}
