using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedVariables
{
    public int _heroXP;
    public int _xpBarMin;
    public int _playerDamage;           
    public int _pDmgLvl;
    public int _playerHealth;            
    public int _pHpLvl;
    public float _playerSpeed;           
    public int _pSpLvl;
    public int _playerEvade;             
    public int _pEvLvl;
    public int _playerLuck;              
    public int _pLkLvl;

    public int _heroLevel;
    public int _xpToLevel;
    public int _skillPoints;
    public int _xpBarMax;
    public int _xpIncrease;


    public int _healthPU;
    public int _secondWindPU;
    public int _timePU;
    public int _shieldPU;

    public int _activeHero;

    public int _playerGold;

    public bool _isClassOne;
    public bool _isClassTwo;
    public bool _isClassThree;
    public bool _isClassFour;

    public int _regionLoad;
    public int _levelSelect;
    public int _regionProgress;
    public int _levelProgress;
    public int _endlessRecord;
    public int _date;
    public bool _dailyActive;
    public int[] _levelStars = new int[75];

    public float _heroMana;

    public SavedVariables (PlayerStats data)
    {
        _playerGold = data.playerGold;
        _heroLevel = data.heroLevel;
        _heroXP = data.heroXP;
        _playerDamage = data.playerDamage;
        _pDmgLvl = data.pDmgLvl;
        _playerHealth = data.playerHealth;
        _pHpLvl = data.pHpLvl;
        _playerSpeed = data.playerSpeed;
        _pSpLvl = data.pSpLvl;
        _playerEvade = data.playerEvade;
        _pEvLvl = data.pEvLvl;
        _playerLuck = data.playerLuck;
        _pLkLvl = data.pLkLvl;

        _healthPU = data.healthPU;
        _secondWindPU = data.secondWindPU;
        _shieldPU = data.shieldPU;
        _timePU = data.timePU;

        _xpToLevel = data.xpToLevel;
        _skillPoints = data.skillPoints;
        _xpIncrease = data.xpIncrease;

        _isClassFour = data.isClassFour;
        _isClassThree = data.isClassThree;
        _isClassTwo = data.isClassTwo;
        _isClassOne = data.isClassOne;

        _activeHero = data.activeHero;

        _regionProgress = data.regionProgress;
        _levelProgress = data.levelProgress;
        _endlessRecord = data.endlessRecord;
        _date = data.date;
        _dailyActive = data.dailyActive;
        for (int i = 0; i < _levelStars.Length; i++)
        {
            _levelStars[i] = data.levelStars[i];
        }

        _heroMana = data.heroMana;
    }
}
