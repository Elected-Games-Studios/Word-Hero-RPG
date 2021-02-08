using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddHero : MonoBehaviour
{
    public void TestAddHeroClicked()
    {
        CharectorStats.ShardCounter[1] = 9;
        CharectorStats.AddCharecter(1);
        for (int i = 0; i < CharectorStats.numOfHeroes(); i++)
        {
            var character = CharectorStats.UnlockedCharector(i);
            CharectorStats.testAddExp(i, 7);
        }
        GameMaster.CallSave();
        

    }
}
