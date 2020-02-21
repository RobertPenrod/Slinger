using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
    public float returnSpeed;
    public float maxAmplitude;

    public GameObject shakeObject;

    // Start is called before the first frame update
    void Start()
    {
        shakeObject = new GameObject();
        shakeObject.name = "Shake Object";

        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        
        for(int i = 0; i < rootObjects.Length; i++)
        {
            rootObjects[i].transform.parent = shakeObject.transform;
        }

        shakeObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCentered())
        {
            Center();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocity = collision.relativeVelocity;
        float magnitude = velocity.magnitude;
        if (magnitude > 15)
        {
            velocity /= magnitude;
            Debug.Log(magnitude);
            velocity *= 1f;
            shakeObject.transform.position = new Vector3(velocity.x, velocity.y, shakeObject.transform.position.z);
        }
    }

    private bool isCentered()
    {
        float displacement = Vector2.Distance(new Vector2(0, 0), shakeObject.transform.position);
        return displacement <= Mathf.Epsilon;
    }

    private void Center()
    {
        shakeObject.transform.position = Vector3.MoveTowards(shakeObject.transform.position, new Vector3(0, 0, shakeObject.transform.position.z), returnSpeed * Time.fixedDeltaTime);
    }
}
