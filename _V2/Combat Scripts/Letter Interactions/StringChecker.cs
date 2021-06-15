using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StringChecker: MonoBehaviour
{
    private TextMeshPro currentString;

    private void Awake()
    {
        currentString = GameObject.Find("CurrentWord").GetComponent<TextMeshPro>();
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
