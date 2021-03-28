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


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GetAvailability();
        SetNumbers();
        SetSliderBounds();
        SetSliderDefault();
    }

    void GetAvailability()
    {
        int heroSlotsLeft = GameMaster.BackpackSize - CharectorStats.numOfHeroes();

        if (greenOrPurple == true)
        {
            T1Available = InvManager.T1ShardNumCombo();
            if (T1Available > 10) { T1Available = 10; }
            if (T1Available > heroSlotsLeft) { T1Available = heroSlotsLeft; }
        }
        else
        {
            T2Available = InvManager.T2ShardNumCombo();
            if (T2Available > 10) { T2Available = 10; }
            if (T2Available > heroSlotsLeft) { T2Available = heroSlotsLeft; }
        }

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
        gameObject.SetActive(false);
        confirmPanel.SetActive(false);
        int heroesToMake = (int)slider.value;


        if(greenOrPurple == true)
        {
            InvManager.CombineT1(heroesToMake);           
        }
        else
        {
            InvManager.CombineT2(heroesToMake);
        }
        onHerosGenerated?.Invoke();
        // addedPanel.SetActive(true);
    }

    public void SetGreen()
    {
        greenOrPurple = true;
        gameObject.SetActive(true);      
        fill.color = Color.green;
    }
    public void SetPurple()
    {
        greenOrPurple = false;
        gameObject.SetActive(true);       
        fill.color = Color.magenta;
    }
 
}
