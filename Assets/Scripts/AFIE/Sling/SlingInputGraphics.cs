using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlingInputHandler))]
public class SlingInputGraphics : MonoBehaviour
{
    public GameObject slingGraphicsObject;

    protected SlingInputHandler inputHandler;
    protected LineRenderer lineRenderer;
    [SerializeField]
    protected GameObject cancelCircleSpriteObject;

    private void OnDrawGizmos()
    {
        inputHandler = GetComponent<SlingInputHandler>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, inputHandler.getCancelRadius());
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(inputHandler.getInitialPressPosition(), inputHandler.getMaxDragRadius());
    }

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<SlingInputHandler>();
        lineRenderer = slingGraphicsObject.GetComponent<LineRenderer>();
        slingGraphicsObject.transform.parent = this.transform;
        cancelCircleSpriteObject.transform.localScale = new Vector3(inputHandler.getCancelRadius(), inputHandler.getCancelRadius(), inputHandler.getCancelRadius());
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputHandler.isPressed() && inputHandler.canSling)
        {
            cancelCircleSpriteObject.transform.eulerAngles = new Vector3(0, 0, 0);
            cancelCircleSpriteObject.transform.position = inputHandler.getInitialPressPosition();

            float dragMagnitude = inputHandler.getDragMagnitude();
            //float magnitudeToCancelRadius = inputHandler.getDragMagnitude() - inputHandler.getCancelRadius();
            float maxDrag = inputHandler.getMaxDragRadius();
            //Vector2 positionOnCancelRadius = inputHandler.getPressPosition() + inputHandler.getDragVector() * (magnitudeToCancelRadius / dragMagnitude);

            Vector3[] positions = new Vector3[2];

            if (dragMagnitude > maxDrag)
            {
                maxDrag += inputHandler.getCancelRadius(); // Not sure why I have to do this but this makes it work.
                Vector3 dragPosRelativeToInitialPress = inputHandler.getPressPosition() - inputHandler.getInitialPressPosition();
                float relativeDragMagnitude = dragPosRelativeToInitialPress.magnitude;
                // clamp magnitude to maxDrag
                dragPosRelativeToInitialPress.x *= maxDrag / relativeDragMagnitude;
                dragPosRelativeToInitialPress.y *= maxDrag / relativeDragMagnitude;
                Vector3 limitedDragPosition = dragPosRelativeToInitialPress + (Vector3)inputHandler.getInitialPressPosition();
                positions[0] = limitedDragPosition;
                positions[1] = inputHandler.getInitialPressPosition();
            }
            else
            {
                positions[0] = inputHandler.getPressPosition();
                positions[1] = inputHandler.getInitialPressPosition();
            }
            // adjust positions.z so line is draw above any objects in scene
            positions[0].z = -5;
            positions[1].z = -5;
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
            cancelCircleSpriteObject.SetActive(true);
        }
        else
        {
            lineRenderer.enabled = false;
            cancelCircleSpriteObject.SetActive(false);
        }
    }
}
