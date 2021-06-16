using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LetterListener : MonoBehaviour
{
    public bool ticked;
    [SerializeField]


    void OnEnable()
    {
        TouchInput.AllTickedOff += Untick;
    }

    public void AddLetterToCurrent()
    {
        if (!ticked)
        {
            ticked = true;
            CombatWordManager.addToString(GetComponent<TextMeshProUGUI>().text);         
        }      
    }
    public void RemoveLetterFromCurrent()
    {              
            ticked = false;
            CombatWordManager.removeString();
        
    }

    void OnDisable()
    {
        TouchInput.AllTickedOff -= Untick;
    }

    void Untick()
    {
        ticked = false;
    }
}