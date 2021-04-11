using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDebug : MonoBehaviour
{
    private int currentStars = CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero())[3];

    private void Update()
    {
        gameObject.GetComponent<Text>().text = currentStars.ToString();
    }

}
