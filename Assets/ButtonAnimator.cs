using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour
{
    public float offsetAmount;

    private Vector3 initialPosition;
    private Vector2 initialEffectDistance;

    private void Start()
    {
        initialPosition = GetComponent<RectTransform>().position;
        initialEffectDistance = GetComponent<Shadow>().effectDistance;
    }

    public void DepressButton()
    {
        GetComponent<RectTransform>().position = initialPosition + new Vector3(offsetAmount, -offsetAmount);
        GetComponent<Shadow>().effectDistance = new Vector2(0, 0);
    }

    public void RestoreButton()
    {
        GetComponent<RectTransform>().position = initialPosition;
        GetComponent<Shadow>().effectDistance = initialEffectDistance;
    }
}
