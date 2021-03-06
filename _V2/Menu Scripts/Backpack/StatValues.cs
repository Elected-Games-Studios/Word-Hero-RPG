﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatValues : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects = new List<GameObject> { };

    public void DisplayStats()
    {
        int[] heroStats = CharectorStats.setTempHero(CharectorStats.getTempHero()); //displays stats of hero picked from tile but not necessarily selected
        objects[0].GetComponent<Text>().text = heroStats[2].ToString();//XP
        objects[1].GetComponent<Text>().text = heroStats[5].ToString();//health
        objects[2].GetComponent<Text>().text = heroStats[4].ToString();//dmg
        objects[3].GetComponent<Text>().text = heroStats[7].ToString();//agi
        objects[4].GetComponent<Text>().text = heroStats[6].ToString();//crit
        objects[5].GetComponent<Text>().text = heroStats[8].ToString();//def
        objects[6].GetComponent<Text>().text = heroStats[9].ToString();//xptonext
        objects[7].GetComponent<Text>().text = heroStats[1].ToString();//level
    }
}
// 0-Idx 1-Lvl 2-XP 3-Stars 4-dmg 5-health 6-crit 7-agi 8-def 9-XPtoNextLevel 