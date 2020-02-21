using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageDotManager : MonoBehaviour
{
    public GameObject pageDotPrefab;
    public PageSwiper pageSwiper;
    private GameObject[] pageDots;

    // Start is called before the first frame update
    void Start()
    {
        //init();
    }

    public void init()
    // This is called from page swiper once it has loaded all panels.
    {
        // Spawn Page Dots
        int panelCount = pageSwiper.GetPanelCount();
        //Debug.Log("Panel Count: " + panelCount);
        pageDots = new GameObject[panelCount];
        for (int i = 0; i < panelCount; i++)
        {
            pageDots[i] = Instantiate(pageDotPrefab);
            pageDots[i].transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int panelNumber = pageSwiper.currentPanelNumber;
        DeactivateAllDots();
        pageDots[panelNumber].GetComponent<PageDot>().Activate();
    }

    private void DeactivateAllDots()
    {
        for(int i = 0; i < pageDots.Length; i++)
        {
            pageDots[i].GetComponent<PageDot>().Deactivate();
        }
    }
}
