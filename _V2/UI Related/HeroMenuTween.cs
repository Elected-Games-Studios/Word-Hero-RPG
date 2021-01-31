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
        CanvasDisplayManager.GoAway += GoAway;
        CanvasDisplayManager.ComeBack += ComeBack;
    }
    public void OnRise()
    {
        heroObj.transform.localPosition = new Vector3(heroObj.transform.localPosition.x, -857f, heroObj.transform.localPosition.z);
        LeanTween.moveY(heroObj, 8f, .4f).setEaseOutSine();
        heroObj.GetComponentInParent<Canvas>().sortingOrder = 50;
    }
   
    public void OnRevert()
    {
        heroObj.transform.localPosition = new Vector3(heroObj.transform.localPosition.x, -857f, heroObj.transform.localPosition.z);
        heroObj.GetComponentInParent<Canvas>().sortingOrder = 1;
    }
    public void GoAway()
    {
        heroObj.SetActive(false);
    }
    public void ComeBack()
    {
        heroObj.SetActive(true);
    }

    private void OnDestroy()
    {
        CanvasDisplayManager.Rise -= OnRise;
        CanvasDisplayManager.Revert -= OnRevert;
        CanvasDisplayManager.GoAway -= GoAway;
        CanvasDisplayManager.ComeBack -= ComeBack;
    }
}
