using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerolistCountDebug : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = CharectorStats.numOfHeroes().ToString();
    }
}
