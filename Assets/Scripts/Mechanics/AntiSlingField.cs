using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AntiSlingField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;

        // Set order in layer
        if(GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().sortingOrder = -3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if(collidingObject.tag.Equals("Player"))
        {
            collidingObject.GetComponent<SlingMovementHandler>().canSling = false;
            collidingObject.GetComponent<AFIE>().inAntiSlingField = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.tag.Equals("Player"))
        {
            collidingObject.GetComponent<SlingMovementHandler>().canSling = true;
            collidingObject.GetComponent<AFIE>().inAntiSlingField = false;
        }
    }
}
