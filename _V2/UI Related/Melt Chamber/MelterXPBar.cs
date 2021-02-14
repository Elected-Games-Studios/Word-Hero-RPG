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
    private int[] bounds;
    private int current = 0;
    private int[] heroCloneToPass = new int[10];
    private int heroLevelCap;
    private int accumHeroXp;
    public void SetHeroNameText()
    {
        int[] nameTemp = CharectorStats.setTempHero(CharectorStats.getTempHero());
        heroText.text = CharectorStats.HeroName(nameTemp[0]);
    }
    public void CloneHero()
    {
        heroCloneToPass = CharectorStats.setTempHero(CharectorStats.getTempHero());
        accumHeroXp = heroCloneToPass[2];
        bounds = GetBounds();
        current = accumHeroXp - bounds[0];
        heroLevelCap = CharectorStats.findCurrentMaxLevel(CharectorStats.getTempHero());
    }
    public void UpdateSlider(int xpToBeAdded)
    {

        bounds = GetBounds();
        accumHeroXp += xpToBeAdded;
        current += xpToBeAdded;
        
        if(heroCloneToPass[1] < heroLevelCap && (accumHeroXp + xpToBeAdded < CharectorStats.XpOfMaxLevel(CharectorStats.getTempHero())))
        {
            AddSubtractXP();
           
        }
        else if(heroCloneToPass[1] < heroLevelCap && (accumHeroXp + xpToBeAdded >= CharectorStats.XpOfMaxLevel(CharectorStats.getTempHero())))
        {
            //graphic and text to max level and exp
        }
        else if(heroCloneToPass[1] >= heroLevelCap && xpToBeAdded < 0)
        {
            AddSubtractXP();
        }
        
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
            Debug.Log("LevelDown: " + heroCloneToPass[1]);
            heroCloneToPass[1] -= 1;
            bounds = GetBounds();
            current += (bounds[1] - bounds[0]);
        }

        while (current >= (bounds[1] - bounds[0]))
        {
            Debug.Log("LevelUp: " + heroCloneToPass[1]);
            heroCloneToPass[1]++;
            current -= (bounds[1] - bounds[0]);
            bounds = GetBounds();
        }

        currentLvlText.text = "Level: " + heroCloneToPass[1];
        var slider = gameObject.GetComponent<Slider>();
        //current -= bounds[0];
        bounds[1] -= bounds[0];
        bounds[0] = 0;
        slider.maxValue = bounds[1];
        slider.value = current;
    }
}
