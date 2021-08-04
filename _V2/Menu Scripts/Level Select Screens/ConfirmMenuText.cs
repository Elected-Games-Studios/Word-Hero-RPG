using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMenuText : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Text>().text = "Level: " + (GameMaster.Level + 1);
    }
}
