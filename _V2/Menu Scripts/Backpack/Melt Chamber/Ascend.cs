using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct HeroRequirements
{
    public int amount { get; set; }
    public string tier { get; set; }
    public int stars { get; set; }
    public int goldReq { get; set; }
    public int heroType { get; set; }
}

public class Ascend : MonoBehaviour
{
    [SerializeField]
    private GameObject meltBackButton;
    [SerializeField]
    private GameObject selectHeroesButton;
    [SerializeField]
    private List<GameObject> Icons = new List<GameObject>();
    [SerializeField]
    private GameObject confirmPanel;
    [SerializeField]
    private GameObject selectSacrificePanel;
    [SerializeField]
    private AscendGrid ascendGrid;
    private List<Sprite> spritesToDisplay = new List<Sprite>();
    private bool heroesChosenSuccessfully;

    HeroRequirements heroRequirements;
    private static int[] currentHeroArray;
    private int[] indexesChosenToSacrifice = new int[] { }; //will be backpack indexes of heroes from AscendGrid Chosen
    private string selectedHeroTier;

    private void OnEnable()
    {
        heroesChosenSuccessfully = false;
        selectHeroesButton.GetComponent<Button>().onClick.AddListener(OnSelectClick);
        meltBackButton.GetComponent<Button>().interactable = false;
        currentHeroArray = CharectorStats.setTempHero(CharectorStats.getTempHero());
        selectedHeroTier = CharectorStats.HeroTier(currentHeroArray[0]);
        determineRequirements();
        setIconsNeedFilled();
    }

    private void determineRequirements()
    {
        heroRequirements.heroType = currentHeroArray[0];
        switch (selectedHeroTier)
        {
            case "T1":
                if (currentHeroArray[3] == 1 && currentHeroArray[1] == 25)
                {
                    //must choose 1 Tier1 hero with 1star
                    heroRequirements.amount = 1;
                    heroRequirements.tier = "T1";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 250;
                }
                else
                {
                    heroRequirements.goldReq = 100;
                }
                break;
            case "T2":
                if (currentHeroArray[3] == 1 && currentHeroArray[1] == 25)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T2";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 500;
                }
                else if (currentHeroArray[3] == 2 && currentHeroArray[1] == 50)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T2";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 1000;
                }
                else
                {
                    heroRequirements.goldReq = 250;
                }
                    break;
            case "T3":
                if (currentHeroArray[3] == 1 && currentHeroArray[1] == 25)
                {
                    heroRequirements.amount = 1;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 1000;
                }
                else if (currentHeroArray[3] == 2 && currentHeroArray[1] == 50)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 2500;
                }
                else if (currentHeroArray[3] == 3 && currentHeroArray[1] == 75)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 5000;
                }
                break;
            case "T4":
                if (currentHeroArray[3] == 2 && currentHeroArray[1] == 50)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 5000;
                }
                else if (currentHeroArray[3] == 3 && currentHeroArray[1] == 75)
                {
                    heroRequirements.amount = 4;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 10000;
                }
                else if (currentHeroArray[3] == 4 && currentHeroArray[1] == 100)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 15000;
                }
                break;
            case "T5":
                if (currentHeroArray[3] == 3 && currentHeroArray[1] == 75)
                {
                    heroRequirements.amount = 1;
                    heroRequirements.tier = "T5";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 15000;
                }
                else if (currentHeroArray[3] == 4 && currentHeroArray[1] == 100)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T5";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 30000;
                }
                else if (currentHeroArray[3] == 5 && currentHeroArray[1] == 150)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T5";
                    heroRequirements.stars = 4;
                    heroRequirements.goldReq = 75000;
                }
                break;
            default:             
                break;
        }
    }

    private void setIconsNeedFilled()
    {
        //if hero requires sacrifices, set image active for each required
        Icons.ForEach((icon) =>
        {
            icon.SetActive(false);
        });
        for (int i=0; i < heroRequirements.amount; i++)
        {
            Icons[i].SetActive(true);
        }
        //if not, display "No heroes required"
        if (heroRequirements.amount == 0)
        {
            Icons[4].SetActive(true);
            selectHeroesButton.GetComponent<Button>().interactable = false;
        }
        heroesChosenSuccessfully = true;
    }

    public void fillIconsWithChosen(List<int> heroesChosen)
    {
        //set sprites to display based on heroesChosen
        for (int i = 0; i < heroesChosen.Count; i++)
        {
            //spritesToDisplay[i] = heroesChosen.sprite??  <<Make this work
        }
        for (int i = 0; i < Icons.Count; i++)
        {
           // icon.GetComponent<Image>().sprite = spritesToDisplay[i];
        };

    }

    private void OnSelectClick()
    {
        selectSacrificePanel.SetActive(true);
        ascendGrid.setHeroRequirements(heroRequirements);
        ascendGrid.GenButtons();
    }

    public void ascendHero() //MAKE UNCLICKABLE UNLESS HEORESCHOSENSUCCESSFUL = TRUE
    {
       
        CharectorStats.buyHeroUpgrade(CharectorStats.getTempHero());
        confirmPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        meltBackButton.GetComponent<Button>().interactable = true;
    }
}
