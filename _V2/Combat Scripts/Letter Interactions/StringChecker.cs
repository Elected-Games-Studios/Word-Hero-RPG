using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StringChecker: MonoBehaviour
{
    private TextMeshProUGUI currentString;

    private void Awake()
    {
        currentString = GameObject.Find("CurrentWord").GetComponent<TextMeshProUGUI>();
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
