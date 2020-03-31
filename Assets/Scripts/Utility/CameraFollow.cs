using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public bool isLimited;
    public Vector2 limits;

    protected Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + offset.x, objectToFollow.transform.position.y + offset.y, objectToFollow.transform.position.z + offset.z);
        if (isLimited)
        {
            float limitedX = Mathf.Clamp(transform.position.x, -limits.x, limits.x);
            float limitedY = Mathf.Clamp(transform.position.y, -limits.y, limits.y);
            transform.position = new Vector3(limitedX, limitedY, objectToFollow.transform.position.z + offset.z);
        }
    }
}
