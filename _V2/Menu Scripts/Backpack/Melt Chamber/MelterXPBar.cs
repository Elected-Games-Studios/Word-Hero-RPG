﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MelterXPBar : MonoBehaviour
{
    [SerializeField]
    private Text heroText;
    [SerializeField]
    private Text xpText;
    [SerializeField]
    private Text UBText;
    [SerializeField]
    private Text currentLvlText;
    [SerializeField]
    private GameObject xpBar;
    [SerializeField]
    private MeltHeroGridManager gridMan;
    public event Action tempMax;
    public event Action tempMaxReduced;
    private int[] bounds;
    private int current = 0;
    private int[] heroCloneToPass = new int[10];
    private int heroLevelCap;
    private int accumHeroXp;

   public void reduceTempMax()//needed to call from external
    {
        tempMaxReduced?.Invoke();
    }
    public void SetHeroNameText()
    {
        int[] nameTemp = CharectorStats.setTempHero(CharectorStats.getTempHero());
        heroText.text = CharectorStats.HeroName(nameTemp[0]);
    }
    public void CloneHero()
    {
        heroCloneToPass = CharectorStats.setTempHero(CharectorStats.getTempHero());
        accumHeroXp = heroCloneToPass[2];
        if (!gridMan.isMaxedActually)
        {
            bounds = GetBounds();
            current = accumHeroXp - bounds[0];
        }
        heroLevelCap = CharectorStats.findCurrentMaxLevel(CharectorStats.getTempHero());
    }
    public void UpdateSlider(int xpToBeAdded)
    {
        if (!gridMan.isMaxedActually)
        {
            bounds = GetBounds();
        }
        
        current += xpToBeAdded;
        if (!gridMan.isMaxedActually)
        {
            if (heroCloneToPass[1] < heroLevelCap && (accumHeroXp + xpToBeAdded < CharectorStats.XpOfMaxLevel(CharectorStats.getTempHero())))
            {
                xpBar.SetActive(true);
                AddSubtractXP();
                tempMaxReduced?.Invoke();
            }
            else if (heroCloneToPass[1] < heroLevelCap && (accumHeroXp + xpToBeAdded >= CharectorStats.XpOfMaxLevel(CharectorStats.getTempHero())))
            {
                xpBar.SetActive(false);
                currentLvlText.text = CharectorStats.findCurrentMaxLevel(CharectorStats.getTempHero()).ToString();
                tempMax?.Invoke();
            }
            else if (heroCloneToPass[1] >= heroLevelCap && xpToBeAdded < 0)
            {
                xpBar.SetActive(true);
                AddSubtractXP();
                tempMaxReduced?.Invoke();
            }
        }
        accumHeroXp += xpToBeAdded;


    }
    public void SetCurrentAndBoundText()
    {
        xpText.text = current.ToString();
        UBText.text = "/" + bounds[1].ToString();
    }
    private int [] GetBounds()
    {

            var temp = CharectorStats.MeltSetBounds(heroCloneToPass[0], heroCloneToPass[1]);
            return temp;
          
    }
    private void AddSubtractXP()
    {
        while (current < 0)
        {
            heroCloneToPass[1] -= 1;
            if (!gridMan.isMaxedActually)
            {
                bounds = GetBounds();
            }
            current += (bounds[1] - bounds[0]);
        }

        while (current >= (bounds[1] - bounds[0]))
        {
            heroCloneToPass[1]++;
            current -= (bounds[1] - bounds[0]);
            if (!gridMan.isMaxedActually)
            {
                bounds = GetBounds();
            }
        }

        currentLvlText.text = "Level: " + heroCloneToPass[1];
        var slider = gameObject.GetComponentInChildren<Slider>();
        //current -= bounds[0];
        bounds[1] -= bounds[0];
        bounds[0] = 0;
        slider.maxValue = bounds[1];
        slider.value = current;
    }

   
}
