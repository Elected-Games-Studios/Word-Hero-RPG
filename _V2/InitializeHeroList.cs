using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeHeroList : MonoBehaviour
{
    void Start()
    {
        
        CharectorStats.LoadManagerData("");
        

        SceneManager.LoadScene(1);
    }

}
