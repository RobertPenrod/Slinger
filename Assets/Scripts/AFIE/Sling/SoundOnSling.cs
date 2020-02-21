using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlingInputHandler))]
public class SoundOnSling : MonoBehaviour
{
    public AudioSource audioSource;
    protected SlingInputHandler slingHandler;

    // Start is called before the first frame update
    void Start()
    {
        slingHandler = GetComponent<SlingInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slingHandler.isSlung())
        {
            audioSource.Play();
        }
    }
}
