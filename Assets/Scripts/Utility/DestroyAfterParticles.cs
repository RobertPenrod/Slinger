using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyAfterParticles : MonoBehaviour
{
    private bool particleSystemPlayed;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystemPlayed = false;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particleSystem.isPlaying)
        {
            particleSystemPlayed = true;
        }
        else
        {
            if (particleSystemPlayed)
                DestroyImmediate(this.gameObject);
        }
    }
}
