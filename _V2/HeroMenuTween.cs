using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMenuTween : MonoBehaviour
{
    [SerializeField]
    private GameObject heroObj;
    

    private void Awake()
    {
        CanvasDisplayManager.Rise += OnRise;
        CanvasDisplayManager.Revert += OnRevert;
    }
    private void OnRise()
    {
        LeanTween.moveY(heroObj, 8f, .4f).setEaseOutSine();
        heroObj.GetComponentInParent<Canvas>().sortingOrder = 50;
    }
   
    private void OnRevert()
    {
        heroObj.transform.localPosition = new Vector3(heroObj.transform.localPosition.x, -857f, heroObj.transform.localPosition.z);
        heroObj.GetComponentInParent<Canvas>().sortingOrder = 1;
    }

    private void OnDestroy()
    {
        CanvasDisplayManager.Rise -= OnRise;
        CanvasDisplayManager.Revert -= OnRevert;
    }
}
