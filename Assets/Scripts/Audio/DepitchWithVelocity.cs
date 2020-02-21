using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepitchWithVelocity : MonoBehaviour
{
    public float maxVelocity;
    public float minVelocity;
    public float maxDepitch;
    public AudioSource audioSource;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        float velMag = rigidBody.velocity.magnitude;
        float percent;
        if (velMag <= minVelocity)
        {
            percent = 0;
        }
        else if (velMag >= maxVelocity)
        {
            percent = 1;
        }
        else
        {
            float dif = maxVelocity - minVelocity;
            velMag -= minVelocity;
            percent = velMag / dif;
        }
        audioSource.pitch = 1 + maxDepitch * percent;
    }
}
