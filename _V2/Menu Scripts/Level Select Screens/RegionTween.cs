using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegionTween : MonoBehaviour
{
    public bool rightSideOfScreen;
    float regionModifier;
    public void OnEnable()
    {
        if (rightSideOfScreen)
            regionModifier = 1.5f;
        else
            regionModifier = -1.5f;

        {
            transform.localPosition = new Vector2(Screen.width * regionModifier, 0f);
            transform.LeanMoveLocalX(0, .5f).setEaseInOutQuint();
        }
    }

    public void OnClose()
    {
        transform.LeanMoveLocalX(Screen.width* regionModifier, .5f).setEaseInOutQuad().setOnComplete(DisableMe);  
    }

    void DisableMe()
    {
       gameObject.SetActive(false);
    }
}
