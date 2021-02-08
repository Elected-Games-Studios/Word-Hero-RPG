using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeHeroList : MonoBehaviour
{
    void Start()
    {
        
        CharectorStats.LoadManagerData("");
        //Temp Debug Game Master Values
        GameMaster.Region = 0;
        GameMaster.Level = 0;
        GameMaster.Difficulty = 0;
        
        SceneManager.LoadScene(1);
    }

}
