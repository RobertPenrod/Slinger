using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LevelPanel : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public PageSwiper pageSwiper;               // Gotten on start.
    public LevelButtonHandler levelHandler;     // Gotten on start.
    public Transform rowHolder; // Holder of the three rows of levels.
    public GameObject lockImage;
    public TextMeshProUGUI starsRequiredText;
    public GameObject lockedFade;
    public AudioSource unlockAudio;
    public bool locked = true;
    public bool canBeUnlocked = false;
    public int starsNeeded = 50;
    private bool dragging = false;

    private bool init = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        pageSwiper = transform.parent.GetComponent<PageSwiper>();
        levelHandler = transform.parent.GetComponent<LevelButtonHandler>();
        animator = GetComponent<Animator>();
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
        // Check if panel is locked
        int panelNumber = transform.GetSiblingIndex();
        int panelsUnlocked = PlayerPrefs.GetInt("PanelsUnlocked");
        if (panelNumber <= panelsUnlocked)
        {
            Unlock();
            PlayerPrefs.SetInt("PanelsUnlocked", PlayerPrefs.GetInt("PanelsUnlocked") - 1); // To offset the unlock we have just done.
        }
        else
        {
            Lock();

            // Check if panel can be unlocked
            if (levelHandler.starsCollected >= starsNeeded)
            {
                canBeUnlocked = true;
                animator.SetBool("CanUnlock", true);
            }
            else
            {
                canBeUnlocked = false;
                animator.SetBool("CanUnlock", false);
            }
        }
    }

    public void Lock()
    {
        animator.SetBool("Locked", true);
        lockImage.SetActive(true);
        lockedFade.SetActive(true);

        // Set stars required text
        starsRequiredText.text = levelHandler.starsCollected + " / " + starsNeeded;
        starsRequiredText.enabled = true;


        locked = true;
        //LockLevels();
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt("PanelsUnlocked", PlayerPrefs.GetInt("PanelsUnlocked") + 1);
        animator.SetBool("Locked", false);
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

    public void Clicked()
    {
        if(canBeUnlocked && locked && !dragging) // Unlock panel
        {
            animator.SetBool("Unlocking", true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;
        pageSwiper.SendMessage("OnDrag", eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        pageSwiper.SendMessage("OnEndDrag", eventData);
    }

    public void PlayUnlockAudio()
    {
        unlockAudio.Play();
    }
}
