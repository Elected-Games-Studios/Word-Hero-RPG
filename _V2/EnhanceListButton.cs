using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private MeltHeroGridManager btnControl;
    private int thisButtonIndex;
    private string myTextString;
    private bool clicked = false;

    public void SetIndex(int num)
    {
        thisButtonIndex = num;
    }

    public void SetText(string textString) //change to image tile later
    {
        myTextString = textString;
        myText.text = textString;
    }

    public void OnClick()
    {      
       btnControl.MeltableButtonClicked(thisButtonIndex, clicked);
        clicked = !clicked;
    }
}
