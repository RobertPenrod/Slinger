using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AFIE))]
public class AfieResetInputHandler : MonoBehaviour
{
    private AFIE afie;
    private float doublePressTime = 0.2f;
    private float timer;
    private bool trackingDoublePress;
    private Vector2 initialPressLocation;
    private float doublePressRadius = 1f; // was 0.7

    // Start is called before the first frame update
    void Start()
    {
        afie = GetComponent<AFIE>();

        trackingDoublePress = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool initialPress = Input.GetMouseButtonDown(0);
        if (initialPress && afie.alive)
        {
            if(!trackingDoublePress)
            {
                trackingDoublePress = true;
                timer = 0;
                initialPressLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else // double press has happened
            {
                // check if new press is within doublePressRadius of initial press
                Vector2 currentPressLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distance = Vector2.Distance(currentPressLocation, initialPressLocation);
                if (distance <= doublePressRadius)
                {
                    afie.kill();
                }
                else
                {
                    trackingDoublePress = false;
                }
            }
        }

        if(trackingDoublePress)
        {
            // increment timer
            timer += Time.deltaTime;

            if (timer > doublePressTime) // timer has exceeded time of double press
                trackingDoublePress = false;
        }
    }
}
