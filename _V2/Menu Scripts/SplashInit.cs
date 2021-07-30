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

    private void FadeLogo()
    {
        logoCanvas.alpha = 0;
        logoCanvas.LeanAlpha(1, 1f);

    }
}
