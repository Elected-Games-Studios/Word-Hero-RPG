using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegionTween : MonoBehaviour
{
    public void OnEnable()
    {
        {
            transform.localPosition = new Vector3(1266, transform.localPosition.y, 0);
            LeanTween.moveX(gameObject, 0, .2f);
        }
    }

    public void OnClose()
    {

        LeanTween.moveX(gameObject, 6f, .2f).setOnComplete(DisableMe);
       
    }

    void DisableMe()
    {
       gameObject.SetActive(false);
    }
}
