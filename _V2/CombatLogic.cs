using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic : MonoBehaviour
{
    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;

    private int eDmg, eHealth, eCrit, eAgi, eDef;

    private List<string> words = CombatWordManager.Words;
    private int currentWordIndex;

    void Awake()
    {
        //testing only, initializes hero data and chosen character, returns stats to HeroList[0]
        CharectorStats.LoadManagerData("");
        int [] heroStats = CharectorStats.HeroList[0].ToArray();

        pDmg = heroStats[4];
        pHealth = heroStats[5];
        pCrit = heroStats[6];
        pAgi = heroStats[7];
        pDef = heroStats[8];

        //calls GetDataLevel and GetActualWords, 
        //Initializes public static List<string> Words;
        CombatWordManager.StartLevel();
        
    }

    public static void generateBubble(int length) //called once to choose bubble of longest word size, don't need to re-render
    {
       //switch(length)
       //case 5:
       //turn on/ whateverthefuck 5 bubble
       //case 
    }
    void populateBubble()
    {
        CombatWordManager.wordBreak(currentWordIndex);
        //populate letter zones
    }

    //methods to subscribe to onPlayerAttack and onEnemyAttack events in eventmanager
    void playerTakeDamage()
    {

    }

    void enemyTakeDamage()
    {

    }

    void nextWord()
    {
        currentWordIndex++;
    }

    
}
