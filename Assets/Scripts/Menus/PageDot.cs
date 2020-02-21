using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageDot : MonoBehaviour
{
    Image image;
    Color defaultColor;
    Color darkenedColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
        darkenedColor = new Color(defaultColor.r - 0.5f, defaultColor.g - 0.5f, defaultColor.b - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        image.color = defaultColor;
    }

    public void Deactivate()
    {
        image.color = darkenedColor;
    }
}
