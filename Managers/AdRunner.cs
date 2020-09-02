using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRunner : MonoBehaviour
{
    void Awake()
    {
        InvokeRepeating("CallRequestBanner", 0, 30);
        if(AdManager.instance.mainBanner != null)
        {
            Debug.Log("Invoke repeating worked");
        }
    }
    public void CallRequestBanner()
    {
        AdManager.instance.RequestBanner();
    }
}
