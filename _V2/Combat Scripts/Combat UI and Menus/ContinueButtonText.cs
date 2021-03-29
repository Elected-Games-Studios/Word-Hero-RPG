using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonText : MonoBehaviour
{
    private Text thisText;
    private void OnEnable()
    {
        if(GameMaster.lastCompletedLevel == 24)
        {
            thisText.text = "Proceed To Next Region";
        }
    }
}
