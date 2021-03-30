using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashInit : MonoBehaviour
{
    void Start()
    {
        Invoke("NextScene", 2f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(1);
    }

}
