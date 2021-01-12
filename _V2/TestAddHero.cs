using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddHero : MonoBehaviour
{
    public void TestAddHeroClicked()
    {
        CharectorStats.ShardCounter[1] = 9;
        CharectorStats.AddCharecter(1);
        Debug.Log("Num heros" + CharectorStats.numOfHeroes());
        for (int i = 0; i < CharectorStats.numOfHeroes(); i++)
        {
            var what = CharectorStats.UnlockedCharector(i);
            Debug.Log(what);
            Debug.Log("Filler");

        }
    }
}
