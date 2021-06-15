using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashInit : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup logoCanvas;

    void Start()
    {
        StartCoroutine("FadeLogo");
    }

    private void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator FadeLogo()
    {
        logoCanvas.alpha = 0;
        logoCanvas.LeanAlpha(1, 1f);
        yield return new WaitForSeconds(2f);
        Invoke("NextScene", .5f);
    }
}
