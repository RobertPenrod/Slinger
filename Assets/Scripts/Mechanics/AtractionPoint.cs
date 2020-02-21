using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AtractionPoint : MonoBehaviour
{
    public float attractionForce;
    protected float innerRadius = 0.01f; // distance from attractor object must be under to be at "equilibrium".
    private List<GameObject> objectsInProximity = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        foreach(GameObject gameObject in objectsInProximity)
        {
            applyAttractionForce(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        // Attract colliding object to the center of this attraction point
        if (collidingObject.GetComponent<Rigidbody2D>() != null) // if the colliding object has a rigid body
        {
            addObjectToProximityListIfNotAlreadyDone(collidingObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        // Attract colliding object to the center of this attraction point
        if (collidingObject.GetComponent<Rigidbody2D>() != null) // if the colliding object has a rigid body
        {
            objectsInProximity.Remove(collidingObject);
        }
    }

    private void addObjectToProximityListIfNotAlreadyDone(GameObject gameObject)
    {
        if (!objectsInProximity.Contains(gameObject))
            objectsInProximity.Add(gameObject);
    }

    private void applyAttractionForce(GameObject objectToAttract)
    {
        // get displacement vector between objectToAttract and this object
        Vector2 displacement = this.transform.position - objectToAttract.transform.position;
        Vector2 attractDirection = displacement.normalized;
        float distance = displacement.magnitude;

        float outerRadius = GetComponent<CircleCollider2D>().radius;

        float relativeDistance = (distance - innerRadius) / (outerRadius - innerRadius);
            // when distance == innerRadius : relativeDistance = 0
            // when distance == outerRadius : relativeDistance = 1
        float attractMagnitude = distance <= innerRadius ? 0 : relativeDistance * attractionForce;

        objectToAttract.GetComponent<Rigidbody2D>().AddForce(attractDirection * attractMagnitude);
    }
}
