using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LetterListener : MonoBehaviour
{
    public bool ticked;
    [SerializeField]
    private EventManagerv2 em; // inspector assignement can be mitigated through onEnable assignment

    private void Awake()
    {
        
    }
    void OnEnable()
    {
        TouchInput.allTickedOff += untick;
    }

    public void AddLetterToCurrent()
    {
        if (!ticked)
        {
            ticked = true;
            CombatWordManager.addToString(GetComponent<Text>().text);         
        }      
    }
    public void RemoveLetterFromCurrent()
    {              
            ticked = false;
            CombatWordManager.removeString();
        
    }

    void OnDisable()
    {
        TouchInput.allTickedOff -= untick;
    }

    void untick()
    {
        ticked = false;
    }
}