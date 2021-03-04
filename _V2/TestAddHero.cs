using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddHero : MonoBehaviour
{
    public Text goldText;
    private void OnEnable()
    {
        goldText.text = InvManager.GoldReturn().ToString();
    }
    public void TestAddHeroClicked()
    {
        CharectorStats.ShardCounter[1] = 9;
        CharectorStats.AddCharecter(1);
        for (int i = 0; i < CharectorStats.numOfHeroes(); i++)
        {
            var character = CharectorStats.UnlockedCharector(i);
            CharectorStats.testAddExp(i, 7);
        }



        InvManager.GoldAdd(30);



        SaveManager.SaveParse();
        //GameMaster.CallSave();
        

    }
}
