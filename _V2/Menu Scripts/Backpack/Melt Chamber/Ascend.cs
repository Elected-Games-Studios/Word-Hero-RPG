using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct HeroRequirements
{
    public int amount { get; set; }
    public string tier { get; set; }
    public int stars { get; set; }
    public int goldReq { get; set; }
}

public class Ascend : MonoBehaviour
{
    [SerializeField]
    private GameObject meltBackButton;
    [SerializeField]
    private List<GameObject> Icons = new List<GameObject>();
    HeroRequirements heroRequirements;
    private static int[] selectedHero = CharectorStats.setTempHero(CharectorStats.getTempHero());
    private string selectedHeroTier = CharectorStats.HeroTier(selectedHero[0]);

    private void determineRequirements()
    {
        switch (selectedHeroTier)
        {
            case "T1":
                if (selectedHero[3] == 1 && selectedHero[1] == 25)
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
                if (selectedHero[3] == 1 && selectedHero[1] == 25)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T2";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 500;
                }
                else if (selectedHero[3] == 2 && selectedHero[1] == 50)
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
                if (selectedHero[3] == 1 && selectedHero[1] == 25)
                {
                    heroRequirements.amount = 1;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 1000;
                }
                else if (selectedHero[3] == 2 && selectedHero[1] == 50)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 1;
                    heroRequirements.goldReq = 2500;
                }
                else if (selectedHero[3] == 3 && selectedHero[1] == 75)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T3";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 5000;
                }
                break;
            case "T4":
                if (selectedHero[3] == 2 && selectedHero[1] == 50)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 5000;
                }
                else if (selectedHero[3] == 3 && selectedHero[1] == 75)
                {
                    heroRequirements.amount = 4;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 2;
                    heroRequirements.goldReq = 10000;
                }
                else if (selectedHero[3] == 4 && selectedHero[1] == 100)
                {
                    heroRequirements.amount = 3;
                    heroRequirements.tier = "T4";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 15000;
                }
                break;
            case "T5":
                if (selectedHero[3] == 3 && selectedHero[1] == 75)
                {
                    heroRequirements.amount = 1;
                    heroRequirements.tier = "T5";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 15000;
                }
                else if (selectedHero[3] == 4 && selectedHero[1] == 100)
                {
                    heroRequirements.amount = 2;
                    heroRequirements.tier = "T5";
                    heroRequirements.stars = 3;
                    heroRequirements.goldReq = 30000;
                }
                else if (selectedHero[3] == 5 && selectedHero[1] == 150)
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


    private void OnEnable()
    {
        meltBackButton.GetComponent<Button>().interactable = false;

    }

    private void setIconsNeedFilled()
    {
        //if hero requires sacrifices, set image active for each required
        //if not, display "No heroes required"
    }

    private void OnDisable()
    {
        meltBackButton.GetComponent<Button>().interactable = true;
    }
}
