using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFIE_ParticleCollision : MonoBehaviour
{
    public GameObject particleSystemObject;

    public float collisionMult;
    public float startSpeedDeviation = 5; // how much particles can deviate from the collision speed
    private float collisionBuffer = 0.25f; // moves particle spawner away from collision point slightly so particles don't get stuck in walls

    // set by particle system
    private float maxParticleBurstCount;    
    private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.EmissionModule psEmission;

    private void Start()
    {
        particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        psMain = particleSystem.main;
        psEmission = particleSystem.emission;
        maxParticleBurstCount = psEmission.GetBurst(0).maxCount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // rotate particle system transform towards collision location
        // play particle system

        // Get Values
        ContactPoint2D contactPoint = collision.GetContact(0);
        Vector2 normal = contactPoint.normal;
        float collisionMagnitude = collision.relativeVelocity.magnitude;
        float collisionFactor = Mathf.Clamp(collisionMagnitude / 25f, 0, 1); // collision magnitude measured from 0 - 1.

        // Position Particle System
        particleSystemObject.transform.position = contactPoint.point + normal * collisionBuffer;
        particleSystem.transform.LookAt((Vector2)particleSystem.transform.position + normal, Vector2.up);

        // Set Particle System Values
        psMain.startSpeed = new ParticleSystem.MinMaxCurve(collisionMagnitude * collisionMult - startSpeedDeviation / 2.0f, collisionMagnitude * collisionMult + startSpeedDeviation / 2.0f);
        int burstCount = (int)(maxParticleBurstCount * collisionFactor);
        psEmission.SetBurst(0, new ParticleSystem.Burst(0f, burstCount));

        particleSystem.Play();
    }
}
