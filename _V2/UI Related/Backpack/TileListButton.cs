using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private TileListButtonControl btnControl;
    private int thisButtonIndex;
    private string myTextString;
    private Button btn;
    [SerializeField]
    private StatValues statValues;
    
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SetTemp);
    }
    public void SetIndex(int num)
    {
        thisButtonIndex = num;
    }

    public void SetText(string textString) //change to image tile later
    {
        myTextString = textString;
        myText.text = textString;
    }

    public void SetTemp()
    {
        CharectorStats.setTempHero(thisButtonIndex);
        statValues.DisplayStats();
    }
 
}
