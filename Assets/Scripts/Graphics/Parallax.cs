using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject objectToTrack;
    
    public float amount;

    public Vector3 initialPosition;
    public Vector3 initialObjectPosition;

    // DEBUG
    public Vector3 displacement;

    // Start is called before the first frame update
    void Start()
    {
        initialObjectPosition = new Vector3(objectToTrack.transform.position.x, objectToTrack.transform.position.y, objectToTrack.transform.position.z);
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectDisplacement = objectToTrack.transform.position - initialObjectPosition;
        transform.position = initialPosition + objectDisplacement * -amount;
    }
}
