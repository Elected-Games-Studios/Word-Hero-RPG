using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    #region HeroStats
    //player's stats
    public int heroXP;
    public int playerDamage;            // +1 damage per word/level
    public int pDmgLvl;
    public int playerHealth;            // +1hp
    public int pHpLvl;
    public float playerSpeed;           // +0.5 sec to timer
    public int pSpLvl;
    public int playerEvade;             // + 1% chance to dodge an attack
    public int pEvLvl;
    public int playerLuck;              // + 1% chance to crit (2x damage)
    public int pLkLvl;

    public int heroLevel;
    public int xpToLevel;
    public int skillPoints;
    public int xpIncrease;


    public int healthPU;
    public int secondWindPU;
    public int timePU;
    public int shieldPU;

    public int playerGold;

    public int activeHero;

    public bool isClassOne;
    public bool isClassTwo;
    public bool isClassThree;
    public bool isClassFour;

    public float heroMana;
    #endregion

    #region SavedProgress
    public int regionLoad;
    public int levelSelect;
    public int regionProgress;
    public int levelProgress;
    public int endlessRecord;
    public int date;
    public bool dailyActive;
    public int[] levelStars;


    #endregion
    private void Awake()
    {
        levelStars = new int[75];
        Instance = this;
        DontDestroyOnLoad(gameObject);       
    }
    public void SavePlayer()
    {
        LocalSaveEngine.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        SavedVariables data = LocalSaveEngine.LoadPlayer();
        if(data == null)
        {
            return;
        }
        heroXP = data._heroXP;
        playerDamage = data._playerDamage;
        pDmgLvl = data._pDmgLvl;
        playerHealth = data._playerHealth;
        pHpLvl = data._pHpLvl;
        playerSpeed = data._playerSpeed;
        pSpLvl = data._pSpLvl;
        playerEvade = data._playerEvade;
        pEvLvl = data._pEvLvl;
        playerLuck = data._playerLuck;
        pLkLvl = data._pLkLvl;

        heroLevel = data._heroLevel;
        xpToLevel = data._xpToLevel;

        skillPoints = data._skillPoints;
        xpIncrease = data._xpIncrease;

        healthPU = data._healthPU;
        secondWindPU = data._secondWindPU;
        shieldPU = data._shieldPU;
        timePU = data._timePU;
        playerGold = data._playerGold;

        activeHero = data._activeHero;

        isClassFour = data._isClassFour;
        isClassThree = data._isClassThree;
        isClassTwo = data._isClassTwo;
        isClassOne = data._isClassOne;

        regionProgress = data._regionProgress;
        levelProgress = data._levelProgress;
        endlessRecord = data._endlessRecord;
        date = data._date;
        dailyActive = data._dailyActive;

        for(int i = 0; i < levelStars.Length; i++)
        {
            levelStars[i] = data._levelStars[i];
        }

        heroMana = data._heroMana;
    }
}