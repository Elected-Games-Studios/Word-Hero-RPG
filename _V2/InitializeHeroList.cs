using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeHeroList : MonoBehaviour
{
    void Start()
    {
        
        CharectorStats.LoadManagerData("");
        CharectorStats.SetCurrentHero(CharectorStats.HeroList[0][0]);
        for(var i = 0; i < CharectorStats.HeroList[0].ToArray().Length; i++)
        {
            var gay = (CharectorStats.HeroList[0].ToArray());
            Debug.Log(gay[i].ToString());
        }

        SceneManager.LoadScene(1);
    }

}
