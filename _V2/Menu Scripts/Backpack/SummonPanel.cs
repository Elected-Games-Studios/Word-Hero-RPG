using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonPanel : MonoBehaviour
{
    private bool greenOrPurple; //g-true p-false
    public Image fill;
    public GameObject confirmPanel;
    public GameObject addedPanel;
    public Text maxText;
    public Slider slider;
    private int T1Available;
    private int T2Available;
    public event Action onHerosGenerated;

    private void OnEnable()
    {
        GetAvailability();
        SetNumbers();
        SetSliderBounds();
        SetSliderDefault();
    }

    void GetAvailability()
    {
        T1Available = InvManager.T1ShardNumCombo();
        if (T1Available > 10) { T1Available = 10; }

        T2Available = InvManager.T2ShardNumCombo();
        if (T2Available > 10) { T2Available = 10; }
    }

    void SetNumbers()
    {
        if (greenOrPurple == true){ maxText.text = T1Available.ToString();} else { maxText.text = T2Available.ToString();}
    }

    void SetSliderBounds()
    {
        if (greenOrPurple == true) { slider.maxValue = T1Available; } else { slider.maxValue = T2Available; }
    }

    void SetSliderDefault()
    {
        slider.value = 0;
    }

    public void generateHeros()
    {
        Debug.Log("generating " + slider.value + " heroes");
        gameObject.SetActive(false);
        confirmPanel.SetActive(false);
        if(greenOrPurple == true)
        {
            InvManager.CombineT1((int)slider.value);           
        }
        else
        {
            InvManager.CombineT1((int)slider.value);
        }
        onHerosGenerated?.Invoke();
        // addedPanel.SetActive(true);
    }

    public void SetGreen()
    {
        gameObject.SetActive(true);
        greenOrPurple = true;
        fill.color = Color.green;

    }
    public void SetPurple()
    {
        gameObject.SetActive(true);
        greenOrPurple = false;
        fill.color = Color.magenta;

    }
}
