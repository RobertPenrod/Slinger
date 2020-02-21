using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlingInputHandler))]
[RequireComponent(typeof(SlingMovementHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class SlingTrajectoryGraphics : MonoBehaviour
{
    public LineRenderer lineRenderer;
    protected SlingInputHandler inputHandler;
    protected SlingMovementHandler movementHandler;
    protected Rigidbody2D rigidBody;
    //public float length;
    public int steps;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<SlingInputHandler>();
        movementHandler = GetComponent<SlingMovementHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputHandler.isPressed() && inputHandler.canSling)
        {
            lineRenderer.enabled = true;
            // draw trajectory 
            Vector2 velocity = (movementHandler.getSlingForceVector() / rigidBody.mass);
            velocity += rigidBody.velocity;
            Vector2[] pointsV2 = PhysicsPrediction.PlotRigidbodyPositions(rigidBody, transform.position, velocity, steps);
            Vector3[] pointsV3 = new Vector3[pointsV2.Length];
            lineRenderer.positionCount = pointsV2.Length;
            for(int i = 0; i < pointsV2.Length; i++)
            {
                pointsV3[i] = pointsV2[i];
                pointsV3[i].z = -5;
            }
            lineRenderer.SetPositions(pointsV3);
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if(!movementHandler.canSling)
        {
            lineRenderer.enabled = false;
        }
    }
}
