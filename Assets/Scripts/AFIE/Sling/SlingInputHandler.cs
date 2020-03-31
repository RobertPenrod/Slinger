using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingInputHandler : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(initialPressPosition, cancelRadius);
    }

    protected Vector2 pressPosition;
    protected Vector2 initialPressPosition;
    protected bool press;
    protected bool initialPress;
    protected bool slung;
    [SerializeField]
    protected float cancelRadius;
    protected float maxDragRadius; // maximum radius of pull, = to 1/4 length of smallest screen size

    public bool canSling; // controlled by movement handler

    void Start()
    {
        press = initialPress = false;
        pressPosition = new Vector2(0, 0);
        initialPressPosition = new Vector2(0, 0);
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        maxDragRadius = Mathf.Min(width, height) * 0.3f;
        canSling = true;
    }

    void Update()
    {
        press = Input.GetMouseButton(0);
        initialPress = Input.GetMouseButtonDown(0);
        bool initialRelease = Input.GetMouseButtonUp(0);

        if(press)
            pressPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (initialPress)
            initialPressPosition = Input.mousePosition;

        if(canSling)
            slung = initialRelease && getDragMagnitude() > 0;
    }



    public bool isPressed()
    {
        return press;
    }

    public bool isFirstPress()
    {
        return initialPress;
    }

    public bool isSlung()
    {
        return slung;
    }

    public float getDragMagnitude()
    {
        return getDragVector().magnitude - cancelRadius;
    }

    public float getSlingPercent()
    // Returns value betwee 0 and 1 based on drag magnitude.
    {
        float dragMagnitude = Mathf.Clamp(getDragMagnitude(), 0f, maxDragRadius);
        return dragMagnitude / maxDragRadius;
    }

    public Vector2 getDragVector()
    {
        return getInitialPressPosition() - getPressPosition();
    }

    public Vector2 getPressPosition()
    {
        return pressPosition;
    }

    public Vector2 getInitialPressPosition()
    {
        return Camera.main.ScreenToWorldPoint(initialPressPosition);
    }

    public float getCancelRadius()
    {
        return cancelRadius;
    }

    public float getMaxDragRadius()
    {
        return maxDragRadius;
    }
}
