using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class slows time when the user is preparing to sling AFIE
 */

[RequireComponent(typeof(SlingInputHandler))]
public class SlingBulletTime : MonoBehaviour
{
    protected SlingInputHandler inputHandler;
    public float slowdownFactor;
    public float slowdownTime;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<SlingInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AFIE>().alive)
        {
            if (inputHandler.isPressed() && inputHandler.canSling)
            {
                doSlowMo();
            }
            else
            {
                undoSlowMo();
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    protected void doSlowMo()
    {
        Time.timeScale = slowdownFactor;
    }

    protected void undoSlowMo()
    {
        Time.timeScale = 1;
    }
}
