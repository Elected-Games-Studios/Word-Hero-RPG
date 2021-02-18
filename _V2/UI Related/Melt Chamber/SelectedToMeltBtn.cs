using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedToMeltBtn : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private MeltHeroGridManager btnControl;
    private int thisButtonIndex;
    public int heroNum;
    private string myTextString;
    public bool clicked;
    private Button btn;

    public int ThisButtonIndex { get; }

    private void OnEnable()
    {
        clicked = true;
    }
    public void LowerIndex()
    {
        thisButtonIndex-=1;
        Debug.Log("Lowered to " + thisButtonIndex);
    }

    public void SetIndex(int num)
    {
        thisButtonIndex = num;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void SetHeroNum(int num)
    {
        heroNum = num;
    }

    public void SetText(string textString) //change to image tile later
    {
        myTextString = textString;
        myText.text = textString;
    }

    public void OnClick()
    {
       clicked = btnControl.MeltableButtonClicked(thisButtonIndex, clicked, heroNum);
    }
}
