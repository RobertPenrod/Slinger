using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
// Assumes this object is the parent of several panels
{
    public PageDotManager pageDotManager;

    public float sensitivity;   // multiplier for how much swiping moves the panels
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    private Vector3 panelLocation;
    public int panelCount;
    public int currentPanelNumber; // number of the panel currently on screen

    // Start is called before the first frame update
    void Start()
    {
    }

    public void init()
    // This is called from levelButtonHandler after it has added all of the panels it needs.
    {
        currentPanelNumber = 0;
        panelCount = transform.childCount;
        PositionPanels();
        panelLocation = transform.position;

        pageDotManager.init();

        // Check if the player was on a panel previously
        //
        if(PlayerPrefs.HasKey("savedPanelNumber"))
        {
            //Debug.Log("Setting Panel to Panel #" + PlayerPrefs.GetInt("savedPanelNumber"));
            setCurrentPanel(PlayerPrefs.GetInt("savedPanelNumber"));
        }
    }

    public void savePanelNumber(int panelNumber)
    {
        //Debug.Log("Sacing panel #" + panelNumber);
        PlayerPrefs.SetInt("savedPanelNumber", panelNumber);
    }

    public void setCurrentPanel(int panelNumber)
    // Moves the panelNumber'th panel into view instantly
    {
        if (panelNumber < panelCount && panelNumber >= 0)
        {
            // Reset Panel Location
            //panelLocation = transform.position;

            float offset = Screen.width * panelNumber;
            Vector3 newLocation = panelLocation;
            newLocation += new Vector3(-offset, 0, 0);
            transform.position = newLocation;
            panelLocation = newLocation;
            currentPanelNumber = panelNumber;
            savePanelNumber(panelNumber);
        }
        else
        {
            Debug.Log("Panel #" + panelNumber + " Not in Range");
        }
    }

    public int GetPanelCount()
    {
        return transform.childCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float difference = eventData.pressPosition.x - eventData.position.x;
        transform.position = panelLocation - new Vector3(difference * sensitivity, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0)
            // Swipe Right
            {
                if (currentPanelNumber < panelCount-1)
                {
                    newLocation += new Vector3(-Screen.width, 0, 0);
                    currentPanelNumber++;
                }
                else
                {
                    ReturnToPreviousPosition();
                }
            }
            else if (percentage < 0)
            // Swipe Left
            {
                if (currentPanelNumber > 0)
                {
                    newLocation += new Vector3(Screen.width, 0, 0);
                    currentPanelNumber--;
                }
                else
                {
                    ReturnToPreviousPosition();
                }
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            ReturnToPreviousPosition();
        }

        savePanelNumber(currentPanelNumber);
    }

    private void ReturnToPreviousPosition()
    {
        StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
    }
    
    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
    }

    private void PositionPanels()
    // Used to Initialize panel positions at start.
    // Assumes this object is the parent of several panels
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Vector3 oldPosition = transform.GetChild(i).transform.position;
            transform.GetChild(i).transform.position = new Vector3(i * Screen.width + Screen.width/2f, oldPosition.y, oldPosition.z);
        }
    }
}
