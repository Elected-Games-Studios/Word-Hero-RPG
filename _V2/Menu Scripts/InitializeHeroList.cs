using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeHeroList : MonoBehaviour
{
    void Start()
    {

        // SaveManager.LoadSplit(" # # ");
        //Temp Debug Game Master Values
        //GameMaster.Region = 0;
        //GameMaster.Level = 0;
        //GameMaster.Difficulty = 0;

        Invoke("NextScene", 2f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(1);
    }

}
