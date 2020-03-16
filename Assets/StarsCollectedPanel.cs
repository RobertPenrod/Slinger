using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsCollectedPanel : MonoBehaviour
{
    public TextMeshProUGUI textMeshObject;
    public int starsCollected = 0;
    public bool gottenStarsCollected = false;
    public bool startVisible;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (startVisible)
            Show();
        else
            Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Show()
    {
        animator.SetBool("Visible", true);
    }

    public void Hide()
    {
        animator.SetBool("Visible", false);
    }

    public void AddStar()
    {
        animator.SetTrigger("AddStar");
        //animator.SetBool("AddingStar", true);
        //IncrementStarsCollected();
    }

    public void DoneAddingStar()
    {
        //animator.SetBool("AddingStar", false);
    }

    private void LateUpdate()
    {
        if (!gottenStarsCollected)
        {
            starsCollected = PlayerPrefs.GetInt("StarsCollected");
            textMeshObject.text = starsCollected.ToString();
            gottenStarsCollected = true;
        }
    }

    private void IncrementStarsCollected()
    {
        starsCollected++;
        textMeshObject.text = starsCollected.ToString();
    }
}
