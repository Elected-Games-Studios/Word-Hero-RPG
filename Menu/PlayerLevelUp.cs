using UnityEngine;
using UnityEngine.UI;
using Unity;
using System.Collections;
using System.Collections.Generic;

public class PlayerLevelUp : MonoBehaviour
{
    public static PlayerLevelUp Instance;
    //variables regarding the level-up system

    PlayServices cloud;
    PlayerStats stats;
    public MainMenu main;
    public AudioSource sound;
    public AudioClip ding;
    public AudioClip error;
    public AudioClip respec;

    public GameObject menuCanvas;
    public GameObject menuWorld;
    public GameObject classupCanvas;
    public GameObject playerCache;
    public Text respecCost;

    public GameObject flashOfLight;

    public List<GameObject> classes;

    private void Awake()
    {
        cloud = PlayServices.Instance;
        Instance = this;
        stats = PlayerStats.Instance;

        if (stats.heroLevel == 0)
        {
            stats.heroLevel = 1;
            stats.xpToLevel = 100;
            stats.xpIncrease = 250;
            stats.skillPoints = 1;
        }
    }

    void Update()
    {
        LevelUp();
        ClassUp();
        respecCost.text = (stats.heroLevel * 25).ToString();
    }

    public void LevelUp()
    {
        if (stats.heroXP >= stats.xpToLevel)
        {
            stats.skillPoints++;
            stats.heroXP -= stats.xpToLevel;
            stats.xpToLevel += stats.xpIncrease;
            stats.xpIncrease += 150;
            stats.heroLevel++;

            cloud.SaveData();
        }
    }

    public void CloseButtons()
    {
        for (int i = 0; i < classes.Count; i++)
        {
            classes[i].gameObject.SetActive(false);
        }
    }

    public void ClassUp()
    {
        if (stats.heroLevel >= 10 && !stats.isClassOne)
        {
            classes[stats.activeHero].gameObject.SetActive(true);
        }
        if (stats.heroLevel >= 25 && stats.isClassOne && !stats.isClassTwo)
        {
            classes[stats.activeHero].gameObject.SetActive(true);
        }
        if (stats.heroLevel >= 50 && stats.isClassTwo && !stats.isClassThree)
        {
            classes[stats.activeHero].gameObject.SetActive(true);
        }
        if (stats.heroLevel >= 100 && stats.isClassThree && !stats.isClassFour)
        {
            classes[stats.activeHero].gameObject.SetActive(true);
        }
    }

    public void RespecHero()
    {
        if(stats.playerGold >= (stats.heroLevel * 25))
        {
            stats.playerGold -= (stats.heroLevel*25);
            sound.clip = respec;
            sound.Play();
            stats.activeHero = 0;

            stats.skillPoints = stats.heroLevel;

            stats.playerDamage = 0;
            stats.pDmgLvl = 0;
            stats.playerHealth = 0;
            stats.pHpLvl = 0;
            stats.playerSpeed = 0;
            stats.pSpLvl = 0;
            stats.playerEvade = 0;
            stats.pEvLvl = 0;
            stats.playerLuck = 0;
            stats.pLkLvl = 0;
            stats.isClassOne = false;
            stats.isClassTwo = false;
            stats.isClassThree = false;
            stats.isClassFour = false;
            
            cloud.SaveData();
        }
        else
        {

        }

    }

    #region LevelUpButtons
    public void DamageUp()
    {
        if(stats.skillPoints >= 1 && stats.pDmgLvl < 100)
        {
            sound.clip = ding;
            sound.Play();
            stats.skillPoints -= 1;
            stats.playerDamage += 1;
            stats.pDmgLvl++;

            cloud.SaveData();
        }
        else
        {
            sound.clip = error;
            sound.Play();
        }
    }

    public void HealthUp()
    {
        if (stats.skillPoints >= 1 && stats.pHpLvl < 100)
        {
            sound.clip = ding;
            sound.Play();
            stats.skillPoints -= 1;
            stats.playerHealth += 1;
            stats.pHpLvl++;
            cloud.SaveData();
        }
        else
        {
            sound.clip = error;
            sound.Play();
        }
    }

    public void SpeedUp()
    {
        if (stats.skillPoints >= 1 && stats.pSpLvl < 100) 
        {
            sound.clip = ding;
            sound.Play();
            stats.skillPoints -= 1;
            stats.playerSpeed += .5f;
            stats.pSpLvl++;
            cloud.SaveData();
        }
        else
        {
            sound.clip = error;
            sound.Play();
        }
    }

    public void EvadeUp()
    {
        if (stats.skillPoints >= 1 && stats.pEvLvl < 50)
        {
            sound.clip = ding;
            sound.Play();
            stats.skillPoints -= 1;
            stats.playerEvade += 1;
            stats.pEvLvl++;
            cloud.SaveData();
        }
        else
        {
            sound.clip = error;
            sound.Play();
        }
    }

    public void LuckUp()
    {
        if (stats.skillPoints >= 1 && stats.pLkLvl < 50)
        {
            sound.clip = ding;
            sound.Play();
            stats.skillPoints -= 1;
            stats.playerLuck += 1;
            stats.pLkLvl++;
            cloud.SaveData();
        }
        else
        {
            sound.clip = error;
            sound.Play();
        }
    }
    #endregion

    #region FighterButtons

    public void Fighter()
    {
        stats.activeHero = 1;
        stats.playerHealth += 5;
        stats.isClassOne = true;
        cloud.SaveData();
        CloseButtons();
        StartCoroutine("WaitForFlash");
    }

    public void Barbarian()
    {
        stats.activeHero = 2;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Soldier()
    {
        stats.activeHero = 3;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void BladeMaster()
    {
        stats.activeHero = 4;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void WarMage()
    {
        stats.activeHero = 5;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Knight()
    {
        stats.activeHero = 6;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Lancer()
    {
        stats.activeHero = 7;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }
    #endregion

    #region MageButtons

    public void Mage()
    {
        stats.activeHero = 2; //Should be 8
        stats.playerDamage += 5;
        stats.isClassOne = true;
        cloud.SaveData();
        CloseButtons();
        StartCoroutine("WaitForFlash");
    }

    public void Wizard()
    {
        stats.activeHero = 9;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Acolyte()
    {
        stats.activeHero = 10;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Druid()
    {
        stats.activeHero = 11;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Sorcerer()
    {
        stats.activeHero = 12;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Cleric()
    {
        stats.activeHero = 13;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Warlock()
    {
        stats.activeHero = 14;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }
    #endregion

    #region RogueButtons

    public void Rogue()
    {
        stats.activeHero = 3; //Should be 15
        stats.playerLuck += 5;
        stats.playerEvade += 5;
        stats.isClassOne = true;
        cloud.SaveData();
        CloseButtons();
        StartCoroutine("WaitForFlash");
    }

    public void Thief()
    {
        stats.activeHero = 16;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Gambler()
    {
        stats.activeHero = 17;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void ArcaneTrickster()
    {
        stats.activeHero = 18;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Assassin()
    {
        stats.activeHero = 19;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Tactician()
    {
        stats.activeHero = 20;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Bard()
    {
        stats.activeHero = 21;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }
    #endregion

    #region RangerButtons

    public void Ranger()
    {
        stats.activeHero = 4;     //should be 22
        stats.playerSpeed += 2.5f;
        stats.isClassOne = true;
        cloud.SaveData();
        CloseButtons();
        StartCoroutine("WaitForFlash");
    }

    public void Archer()
    {
        stats.activeHero = 23;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Duelist()
    {
        stats.activeHero = 24;
        stats.isClassTwo = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void SpellSniper()
    {
        stats.activeHero = 25;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void BowMaster()
    {
        stats.activeHero = 26;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Trapper()
    {
        stats.activeHero = 27;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }

    public void Hunter()
    {
        stats.activeHero = 28;
        stats.isClassThree = true;
        cloud.SaveData();
        CloseButtons();
    }
    #endregion

    #region TheRestOfTheClassButtons

    #endregion    

    public void ClassUpShow()
    {
        gameObject.GetComponent<MainMenu>().CloseWindow();
        menuCanvas.SetActive(false);
        menuWorld.SetActive(false);

    }

    IEnumerator WaitForFlash()
    {
        Debug.Log("Coroutine Started");
        Camera.main.gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<MainMenu>().CloseWindow();
        menuCanvas.SetActive(false);
        menuWorld.SetActive(false);
        classupCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        Instantiate(flashOfLight, classupCanvas.transform);
        playerCache.GetComponent<MenuHeroShow>().HeroSelect();
        yield return new WaitForSeconds(3);
        menuCanvas.SetActive(true);
        menuWorld.SetActive(true);
        classupCanvas.SetActive(false);
        Camera.main.gameObject.GetComponent<AudioSource>().Play();
        yield return null;
    }
}