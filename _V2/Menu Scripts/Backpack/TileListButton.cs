using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileListButton : MonoBehaviour //ALSO USED FOR SACRIFICE TILES
{
    public List<Sprite> HeroTokens;
    private Image currentHeroImage;
    [SerializeField]
    private Text heroName;
    [SerializeField]
    private Text heroLevel;
    [SerializeField]
    private GameObject heroStars;
    [SerializeField]
    private TileListButtonControl btnControl;
    private int thisButtonIndex;
    private string myTextString;
    [SerializeField]
    private StatValues statValues;

    //Only used for Ascendancy sacrifice selection
    public bool toggleSacrificeSelect = false;
    private Button btn;
    public int backpackIdx;
    [SerializeField]
    private AscendGrid ag;

    private void Awake()
    {
        currentHeroImage = gameObject.GetComponent<Image>();
    }
    public void SetIndex(int num)
    {
        thisButtonIndex = num;
    }
    
    public void SetSacrificeIndex(int num, int backpackIndex) //Only used for Ascendancy sacrifice selection, needs to add listener for toggle
    {
        thisButtonIndex = num;
        backpackIdx = backpackIndex;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(heroSelectToggle);
    }

    public void SetText(string textString) //change to image tile later
    {
        myTextString = textString;
        heroName.text = textString;
    }

    public void SetTemp()
    {
        CharectorStats.setTempHero(thisButtonIndex);
        statValues.DisplayStats();
    }


    private void heroSelectToggle() //Manage Ascendancy toggle states
    {
        toggleSacrificeSelect = !toggleSacrificeSelect;
        ag.updateHeroesChosen();
    }

    public void SetToken(int heroIndex, int tokenLevel, int tokenStars)
    {
        currentHeroImage.sprite = HeroTokens[heroIndex];
        heroLevel.text = "Lvl: " + tokenLevel;
        for(int i = 0; i< heroStars.transform.childCount; i++)
        {
            if(i == tokenStars)
            {
                heroStars.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                heroStars.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
