using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(SlingInputHandler))]
public class TrailAdjuster : MonoBehaviour
{
    float margin = 0.1f;
    public GameObject body;
    public SlingInputHandler inputHandler;
    private TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        inputHandler = GetComponent<SlingInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not aiming
        if (!inputHandler.isPressed())
        {
            // Adjust width of trail renderer to AFIE scale.
            trailRenderer.startWidth = body.transform.localScale.y - margin;
        }
    }
}
