using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlingInputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class SlingMovementHandler : MonoBehaviour
{
    public bool canSling = true;
    public int slingEnergy = 1;
    public float maxVelocity;
    protected SlingInputHandler inputHandler;
    protected Rigidbody2D rigidBody;
    [SerializeField]
    protected float maxSlingForce = 50;
    protected bool doSlingInFixedUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<SlingInputHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
        disableRigidBody();
    }

    private void FixedUpdate()
    {
        if(doSlingInFixedUpdate)
        {
            enableRigidBody();
            rigidBody.AddForce(getSlingForceVector(), ForceMode2D.Impulse);
            slingEnergy--;
            doSlingInFixedUpdate = false;
        }
    }

    public Vector2 getSlingForceVector()
    {
        // Trajectory Graphics uses this method for its prediction.
        float force = inputHandler.getSlingPercent() * maxSlingForce;
        Vector2 direction = inputHandler.getDragVector().normalized;

        Vector2 slingForce = force * direction;

        return slingForce;
    }


    // Update is called once per frame
    void Update()
    {
        // if we have energy points to sling, then allow the detection of sling inputs.
        if (slingEnergy > 0 && canSling)
        {
            inputHandler.canSling = true;
        }
        else
        {
            inputHandler.canSling = false;
        }

        if (inputHandler.canSling)
        { 
            if (inputHandler.isSlung())
            {
                doSlingInFixedUpdate = true;
            }
        }
    }

    protected void disableRigidBody()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    protected void enableRigidBody()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
