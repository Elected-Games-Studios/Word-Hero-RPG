using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddHero : MonoBehaviour
{
    public Text goldText;
    private void OnEnable()
    {
        //goldText.text = InvManager.GoldReturn().ToString();
    }
    public void TestAddHeroClicked()
    {    
        //InvManager.GoldAdd(30);
        InvManager.T1ShardAdd(10);
        InvManager.T2ShardAdd(10);
        Debug.Log("current Inv:" + InvManager.GoldReturn().ToString() + " gold, " + InvManager.T1ShardAmount().ToString() + " T1s, " + InvManager.T2ShardAmount().ToString() + " T2s");
        for (int i = 0; i < CharectorStats.numOfHeroes(); i++)
        {
            var character = CharectorStats.UnlockedCharector(i);
            CharectorStats.testAddExp(i, 10);
        }

        SaveManager.SaveParse();
        //GameMaster.CallSave();
        

    }
}
