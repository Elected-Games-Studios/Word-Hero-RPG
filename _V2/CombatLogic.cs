using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;

    private int eDmg, eHealth, eCrit, eAgi, eDef;

    private List<string> words = CombatWordManager.Words;
    private int currentWordIndex;
    private Image[] bubbles;
    private GameObject currentBubble;

    void Awake()
    {
        bubbles = GetComponentsInChildren<Image>(true);
        //testing only, initializes hero data and chosen character, returns stats to HeroList[0]
        CharectorStats.LoadManagerData("");
        int [] heroStats = CharectorStats.HeroList[0].ToArray();
        //GameObject bubbles = GetComponentInChildren<bubbles>();
        pDmg = heroStats[3];
        pHealth = heroStats[4];
        pCrit = heroStats[5];
        pAgi = heroStats[6];
        pDef = heroStats[7];
        CombatWordManager.onMaxLengthFound += generateBubble;
        CombatWordManager.StartLevel();
        
        

    }
    private void Update()
    {
        
    }
    

    public void generateBubble(int length) //called once to choose bubble of longest word size, don't need to re-render
    {      
        bubbles[length - 4].gameObject.SetActive(true);
        populateBubble();
        CombatWordManager.onMaxLengthFound -= generateBubble;
    }

    public void populateBubble()
    {
        int length = CombatWordManager.longestWord.Length;
        CombatWordManager.wordBreak(currentWordIndex);
        currentBubble = bubbles[length - 4].gameObject;
        Text [] lettersArr = currentBubble.GetComponentsInChildren<Text>();
        for(int i = 0; i < length; i++)
        {
            lettersArr[i].text = CombatWordManager.shuffledWord[i].ToUpper();
        }
    }

    //event methods
    void playerTakeDamage()
    {

    }

    void enemyTakeDamage()
    {

    }
    //onEnemyKilled
    void stageLoot()
    {
        //animations based on enemy exp and loot
        //private static staged variable
    }
    //event onLevelComplete
    void levelFinished()
    {
    
        //staged items to DB
    }

    //onenemykilled
    void nextWord()
    {
        currentWordIndex++;
    }

    
}
