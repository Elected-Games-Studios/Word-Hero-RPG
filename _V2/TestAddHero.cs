using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddHero : MonoBehaviour
{
    public void TestAddHeroClicked()
    {
        CharectorStats.ShardCounter[1] = 9;
        CharectorStats.AddCharecter(1);
        //Debug.Log(CharectorStats.numOfHeroes());
        
    }
}
