using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;

    private int eDmg, eAgi, eHealth, eCrit;

    public static bool isPlayerAlive = true;

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
    //end temporary^

    public event Action<int> onDamageEnemy;
    public event Action<int> onDamagePlayer;
    public event Action onEnemyKilled;
    public event Action onLevelComplete;
    public event Action onPlayerKilled;

    void Awake()
    {

        bubbles = GetComponentsInChildren<Image>(true);
        //testing only, initializes hero data and chosen character, returns stats to HeroList[0]
        CharectorStats.LoadManagerData("");
        int [] heroStats = CharectorStats.SetCurrentHero(0);
        
        //GameObject bubbles = GetComponentInChildren<bubbles>();
        pDmg = heroStats[3];
        pHealth = heroStats[4];
        pCrit = heroStats[5];
        pAgi = heroStats[6];
        pDef = heroStats[7];
        eDmg = 20; //TOCHANGE
        eCrit = 200;
        eAgi = 10;
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
        StartCoroutine(CombatTimer());
    }
    private void Update()
    {

    }   

    IEnumerator CombatTimer()
    {
        while (isPlayerAlive)//combat is happening, or something
        {
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
   

    private void InitializeEnemy()
    {
        eHealth = CombatWordManager.enemyHealth * 5;

        //temp
        slider = GameObject.FindGameObjectWithTag("EnemyHP").GetComponent<Slider>();
        slider.maxValue = eHealth;
        slider.value = eHealth;
        //^ end temp
    }
    private void InitializePlayer()
    {
        HPSlider = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Slider>();
        HPText = HPSlider.GetComponentInChildren<Text>();
        HPSlider.maxValue = pHealth;
        HPSlider.value = pHealth;
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
    }

    //event methods
    void playerTakeDamage(int damage)
    {
        pHealth -= damage;
        HPSlider.value -= damage;
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
        if(pHealth <= 0)
        {
            onPlayerKilled?.Invoke();
        }
    }

    void enemyTakeDamage(int length)
    {
        int totalDmg = pDmg * length * checkCrit();
       // Debug.Log("total dmg" + totalDmg);
        eHealth -= totalDmg;
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
           // Debug.Log("crit");
            result = 2;
        }
        else
        {
            //Debug.Log("not crit");
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
    }
    //onLevelComplete
    void levelFinished()
    {
        Debug.Log("Victory! staged items being added to DB");
    }

    //onEnemyKilled
    void nextWord()
    {
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
