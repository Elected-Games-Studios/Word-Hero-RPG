using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegionTween : MonoBehaviour
{
    public void OnEnable()
    {
        {
            transform.localScale = new Vector3(0, 0);
            transform.LeanScale(new Vector3(1, 1, 1), .5f);
        }
    }

    public void OnClose()
    {
        transform.LeanScale(new Vector3(0,0,0),.5f).setOnComplete(DisableMe);  
    }

    void DisableMe()
    {
       gameObject.SetActive(false);
    }
}
