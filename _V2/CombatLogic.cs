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
    private GameObject bubble;

    void Awake()
    {
        //testing only, initializes hero data and chosen character, returns stats to HeroList[0]
        CharectorStats.LoadManagerData("");
        int [] heroStats = CharectorStats.HeroList[0].ToArray();
        //GameObject bubbles = GetComponentInChildren<bubbles>();
        pDmg = heroStats[3];
        pHealth = heroStats[4];
        pCrit = heroStats[5];
        pAgi = heroStats[6];
        pDef = heroStats[7];

        CombatWordManager.StartLevel();

        CombatWordManager.onMaxLengthFound += generateBubble;

    }
    private void Update()
    {
        
    }
    

    public void generateBubble(int length) //called once to choose bubble of longest word size, don't need to re-render
    {
        //JUST TO PISS OFF DUSTIN<<
        bubble = GameObject.Find("Circle " + length + "L");
            bubble.SetActive(true);

        CombatWordManager.onMaxLengthFound -= generateBubble;
    }
    void populateBubble()
    {
        CombatWordManager.wordBreak(currentWordIndex);
        CombatWordManager.InitializeLetters();
        Text [] textsArray = bubble.GetComponentsInChildren<Text>();
        for (var i = 0; i < textsArray.Length; i++)
        {
            textsArray[i].text = CombatWordManager.shuffledWord[i];
        }
    }

    //methods to subscribe to onPlayerAttack and onEnemyAttack events in eventmanager
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
        //duh, duh-duh da duh, duh, da-daaaaaaaaaaaaaaah
        //staged items to DB
    }

    //onenemykilled
    void nextWord()
    {
        currentWordIndex++;
    }

    
}
