using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    public LevelButtonHandler levelHandler;
    public Transform rowHolder; // Holder of the three rows of levels.
    public GameObject lockImage;
    public TextMeshProUGUI starsRequiredText;
    public GameObject lockedFade;
    public bool locked = true;
    public int starsNeeded = 50;

    private bool init = false;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = transform.parent.GetComponent<LevelButtonHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!init && levelHandler.gottenStarsCollected)
        {
            init = true;
            Init();
        }
    }

    private void Init()
    {
        if(levelHandler.starsCollected >= starsNeeded)
            Unlock();
        else
            Lock();
    }

    public void Lock()
    {
        lockImage.SetActive(true);
        lockedFade.SetActive(true);

        // Set stars required text
        starsRequiredText.text = levelHandler.starsCollected + " / " + starsNeeded;
        starsRequiredText.enabled = true;


        locked = true;
        LockLevels();
    }

    public void Unlock()
    {
        lockedFade.SetActive(false);
        lockImage.SetActive(false);
        starsRequiredText.enabled = false;
        locked = false;
    }

    private void LockLevels()
    {
        for(int row = 0; row < rowHolder.childCount; row++)
        {
            for(int column = 0; column < rowHolder.GetChild(row).childCount; column++)
            {
                rowHolder.GetChild(row).GetChild(column).GetComponent<Level>().Lock();
            }
        }
    }
}
