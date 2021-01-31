using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;
    private int eDmg, eAgi, eHealth, eDef;
    public static bool isPlayerAlive = true;
    private int stagedGold, stagedShard1, stagedShard2 = 0;

    private List<string> words = CombatWordManager.Words;
    int wordsLeft = CombatWordManager.Words.Count;
    private int currentWordIndex;
    private Image[] bubbles;
    private GameObject currentBubble;

    //temporary
    [SerializeField]
    private Text toSpell;
    private Slider HPSlider;
    private Text HPText;
    private Slider slider;

    //debug enemy stats
    public List<Text> allStats;
    //end temporary^

    public event Action<int> onDamageEnemy, onDamagePlayer;
    public event Action onEnemyKilled, onLevelComplete, onPlayerKilled;
    Coroutine CRRef;

    void Awake()
    {
        //Temp Debug Game Master Values
        GameMaster.Region = 1;
        GameMaster.Level = 1;
        GameMaster.Difficulty = 0;
        //end Temp Debug
        bubbles = GetComponentsInChildren<Image>(true);
        CharectorStats.LoadManagerData("");
        int[] heroStats = CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero());
        
        pDmg = heroStats[4];
        pHealth = heroStats[5];
        pCrit = heroStats[6];
        pAgi = heroStats[7];
        pDef = heroStats[8];


    //event subs
    CombatWordManager.onMaxLengthFound += generateBubble;
        CombatWordManager.onCorrectWord += spelledWord;
        onDamageEnemy += enemyTakeDamage;
        onDamagePlayer += playerTakeDamage;
        onEnemyKilled += nextWord;
        onEnemyKilled += stageLoot;
        onLevelComplete += levelFinished;
        onPlayerKilled += playerKilled;

        CombatWordManager.StartLevel();
        InitializePlayer();
        InitializeEnemy();
        CRRef = StartCoroutine(CombatTimer());
    } 

    IEnumerator CombatTimer()
    {
        while (isPlayerAlive)
        {
            Debug.Log("Restarted Timer");
            float difference = (pAgi - eAgi);
            yield return new WaitForSeconds(5f + (difference/100));
            onDamagePlayer?.Invoke(eDmg);         
        }
    }

    public void generateBubble(int length) //called once to choose bubble of longest word size, don't need to re-render
    {      
        bubbles[length - 4].gameObject.SetActive(true);
        populateBubble();
        CombatWordManager.onMaxLengthFound -= generateBubble;
    }
    void removeBubble()//onPlayerKilled
    {
        currentBubble.SetActive(false);
    }
    public void populateBubble()
    {
        int length = CombatWordManager.longestWord.Length;
        //CombatWordManager.wordBreak(currentWordIndex); i dont think this needs to be here. wb was called in start before populatebubble. this generates shuffledword
        currentBubble = bubbles[length - 4].gameObject;
        Text [] lettersArr = currentBubble.GetComponentsInChildren<Text>();
        for(int i = 0; i < length; i++)
        {
            lettersArr[i].text = CombatWordManager.shuffledWord[i].ToUpper();
        }

        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }
   

    private void InitializeEnemy()//must be called after WordBreak()
    {
        //eHealth = CombatWordManager.enemyHealth * 5;
        eHealth = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(100 * Math.Pow(2, GameMaster.Difficulty));
        eDmg = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(10 * Math.Pow(5, GameMaster.Difficulty));
        eAgi = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(10 * Math.Pow(2, GameMaster.Difficulty));//needs changed
        eDef = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(10 * Math.Pow(2, GameMaster.Difficulty));//needs changed
        allStats[0].text += eHealth;
        allStats[1].text += eDmg;
        allStats[2].text += eAgi;
        allStats[3].text += eDef;
        //temp
        slider = GameObject.FindGameObjectWithTag("EnemyHP").GetComponent<Slider>();
        slider.maxValue = eHealth;
        slider.value = eHealth;
        //^ end temp
    }
    private void InitializePlayer()
    {
        allStats[4].text += pHealth;
        allStats[5].text += pDmg;
        allStats[6].text += pAgi;
        allStats[7].text += pCrit;
        allStats[8].text += pDef;

        HPSlider = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Slider>();
        HPText = HPSlider.GetComponentInChildren<Text>();
        HPSlider.maxValue = pHealth;
        HPSlider.value = pHealth;
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
    }

    //event methods
    void playerTakeDamage(int damage)
    {      
        if(damage-pDef > 0)
        {
            //Debug.Log(damage + " damage mitigated by " + pDef + " player defense. Damage reduced to " + (damage - pDef));
            pHealth -= (damage - pDef);
            HPSlider.value -= (damage - pDef);
        }
        else if(damage - pDef <= 0)
        {
            Debug.Log(damage + " damage mitigated by " + pDef + " player defense. Damage reduced to 1");
            pHealth -= 1;
            HPSlider.value -= 1;
        }
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
        if(pHealth <= 0)
        {
            onPlayerKilled?.Invoke();
        }
    }

    void enemyTakeDamage(int length)
    {
        int totalDmg = (pDmg * length * checkCrit()) - eDef; //Verify W/Dylan       
        if (totalDmg >= 2)
        {
            Debug.Log("Total dmg dealt: " + totalDmg);
            eHealth -= totalDmg;
        }
        else
        {
            Debug.Log("Too much eDef. Damage reduced to 1");
            eHealth -= 1;
        }
        if(eHealth <= 0)
        {
            onEnemyKilled?.Invoke();
        }
        slider.value = eHealth;
        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }

    int checkCrit()
    {
        int result;
        int roll = UnityEngine.Random.Range(0, 10000);
        if(pCrit >= roll)
        {
           Debug.Log("Critical hit!");
            result = 2;
        }
        else
        {
            Debug.Log("Normal attack.");
            result = 1;
        }
        return result;
    }

    void spelledWord(int length)
    {
        onDamageEnemy?.Invoke(length);
    }

    void stageLoot()
    {
        Debug.Log("staging loot");
        stagedGold += 100;
        stagedShard1 += 1;
        stagedShard2 += 2;
    }
    //onLevelComplete
    void levelFinished()
    {
        //add experience as well... dunno why im calling a method that returns an int []??
        Debug.Log("Victory!" + stagedGold + " gold, " + stagedShard1 + " T1shards, " + stagedShard2 + " T2shards being added to DB");
        InvManager.GoldAdd(stagedGold);
        stagedGold = 0;
        InvManager.T1ShardAdd(stagedShard1);
        stagedShard1 = 0;
        InvManager.T2ShardAdd(stagedShard2);
        stagedShard2 = 0;
    }

    //onEnemyKilled
    void nextWord()
    {
        foreach(Text stat in allStats)
        {
            stat.text = "";
        }
        StopCoroutine(CRRef);
        CRRef = StartCoroutine(CombatTimer());
        wordsLeft--;
        //decide where to set a short timer to allow enemy entrance
        if (wordsLeft > 0)
        {
            currentWordIndex++;
            CombatWordManager.wordBreak(currentWordIndex);
            CombatWordManager.InitializeLetters();
            populateBubble();
            InitializeEnemy();
            
        }
        else
        {
            onLevelComplete?.Invoke();
        }
       
 
    }

    //onPlayerKilled
    void playerKilled()
    {
        Debug.Log("gameover sequence");
        isPlayerAlive = false;
        removeBubble();
        CombatWordManager.resetString();
        CombatWordManager.GameOverTrigger();
    }

    private void OnDisable()
    {
        //unsubs
        CombatWordManager.onCorrectWord -= spelledWord;
        onDamageEnemy -= enemyTakeDamage;
        onDamagePlayer -= playerTakeDamage;
        onEnemyKilled -= nextWord;
        onEnemyKilled -= stageLoot;
        onPlayerKilled -= playerKilled;
    }

}
