using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This class squeezes AFIE depending on aim direction
 *  and velocity. This is a visual effect to provide
 *  juice to the game.
 */
public class AFIE_Squeeze : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SlingInputHandler inputHandler;
    public float squeezeAmount;
    public float maxSqueezeVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If aiming > squeeze affie facing aim dirrection based on aim magnitude.
        // else squeeze affie based on velocity.

        // Check if aiming
        if(inputHandler.isPressed() && inputHandler.canSling)
        {
            float newScale = 1 - squeezeAmount * inputHandler.getSlingPercent();
            transform.localScale = new Vector3(newScale, 1, 0);
            Vector2 dragDirection = inputHandler.getDragVector();
            float angle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector2 velocity = rigidBody.velocity;
            float newScale = 1 - squeezeAmount * Mathf.Clamp(velocity.magnitude / maxSqueezeVelocity, 0, maxSqueezeVelocity);
            transform.localScale = new Vector3(1, newScale, 0);
            Vector2 dragDirection = inputHandler.getDragVector();
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
