﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}