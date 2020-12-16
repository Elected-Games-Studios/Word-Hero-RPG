using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringChecker: MonoBehaviour
{
    private Text currentString;

    private void Awake()
    {
        currentString = GameObject.Find("CurrentWord").GetComponent<Text>();
    }

    private void OnEnable()
    {
        CombatWordManager.onUpdateString += updateString;
    }
    private void OnDisable()
    {
        CombatWordManager.onUpdateString -= updateString;
    }

    public void updateString()
    {
        currentString.text = CombatWordManager.checkString;
    }
}
