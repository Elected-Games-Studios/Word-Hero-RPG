using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MelterXPBar : MonoBehaviour
{
    private int[] bounds;
    private int[] tempHero;
    public void UpdateSlider()
    {
        bounds = CharectorStats.XPStartEnd(CharectorStats.getTempHero());
        tempHero = CharectorStats.setTempHero(CharectorStats.getTempHero());//REMEMBA THIS
        var current = tempHero[2];
        var slider = gameObject.GetComponent<Slider>();
        current -= bounds[0];
        bounds[1] -= bounds[0];
        bounds[0] = 0;
        slider.maxValue = bounds[1];
        slider.value = current;
    }
}
