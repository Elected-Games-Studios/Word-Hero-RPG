using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MelterXPBar : MonoBehaviour
{
    public void UpdateSlider()
    {
        var slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 100;
        slider.value = 50;
    }
}
