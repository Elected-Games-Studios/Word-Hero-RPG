using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;
    private int eDmg, eAgi, eHealth, eDef, initialEHealth;
    private double lengthMultiplier;
    public static bool isGameplay = true;
    
    public int stagedXP{ get; private set; }
    public int stagedShard1 { get; private set; }
    public int stagedShard2 { get; private set; }
    public int stagedGold { get; private set; }

    private double[] lengthBonus = { .25, .5, 1, 2, 3, 5 };
    private List<string> words = CombatWordManager.Words;
    int wordsLeft;

    //UI Stuff
    [SerializeField]
    private LootPooler lootPooler;
    private int currentWordIndex;
    private Image[] bubbles;
    private GameObject currentBubble;
    [SerializeField]
    private GameObject vicDefPanel;
    [SerializeField]
    private Text vicDefText;
    [SerializeField]
    private GameObject levelUpText;
    private GameObject SelectedHero;
    private GameObject SelectedEnemy;
    private Animator characterAnimator;
    private Animator enemyAnimator;
    private GameObject heroParticles;
    [SerializeField]
    private GameObject characterHolder;
    [SerializeField]
    private GameObject enemyHolder;
    [SerializeField]
    private List<GameObject> allCharacters;
    [SerializeField]
    private List<GameObject> allEnemies;

    //temporary
    [SerializeField]
    private Text toSpell;
    private Slider HPSlider;
    private Text HPText;
    private Slider slider;

    //timer
    private float timerMax;
    private float timer;
    [SerializeField]
    private Slider timeSlide;
    //debug enemy stats
    public List<Text> allStats;
    //end temporary^

    public event Action<int> onDamageEnemy, onDamagePlayer;
    public event Action onEnemyKilled, onLevelComplete, onPlayerKilled;
   // Coroutine CRRef;

    void Awake()
    {
        
        for (int i = 0; i < allCharacters.Count; i++)
        {
            if(i != CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero())[0])
            {
                allCharacters[i].SetActive(false);
                
            }
            else
            {
                SelectedHero = allCharacters[i];
            }
        }
        characterAnimator = characterHolder.GetComponentInChildren<Animator>();
        enemyAnimator = enemyHolder.GetComponentInChildren<Animator>();

        heroParticles = SelectedHero.transform.Find("HeroAttackParticles").gameObject;
        characterAnimator.SetBool("inCombat", true);
        //Temp Debug Game Master Values
       // GameMaster.Region = 0;
        //GameMaster.Level = 0;
       // GameMaster.Difficulty = 0;
        //end Temp Debug
        bubbles = GetComponentsInChildren<Image>(true);
        //CharectorStats.LoadManagerData("");
        int[] heroStats = CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero());
        
        pDmg = heroStats[4];
        pHealth = heroStats[5];
        pCrit = heroStats[6];
        pAgi = heroStats[7];
        pDef = heroStats[8];
        lengthMultiplier = 0;
        stagedXP = 0;
        stagedGold = 0;
        stagedShard1 = 0;
        stagedShard2 = 0;
 
        //event subs
        CombatWordManager.onMaxLengthFound += generateBubble;
        CombatWordManager.onCorrectWord += spelledWord;
        CombatWordManager.onIncorrectWord += wrongWord;
        CharectorStats.leveledUp += onLeveledUp;
        onDamageEnemy += enemyTakeDamage;
        onDamagePlayer += playerTakeDamage;
        onEnemyKilled += nextWord;
        onLevelComplete += levelFinished;
        onPlayerKilled += playerKilled;

        CombatWordManager.StartLevel();
        wordsLeft = CombatWordManager.Words.Count;
        Debug.Log(wordsLeft + " words left");
        InitializePlayer();
        InitializeEnemy();
       InitializeTimer();
        //CRRef = StartCoroutine(CombatTimer());
    } 

    private void InitializeTimer()
    {
        timerMax = 5 + (pAgi - eAgi) / 100;
        timer = timerMax;
        timeSlide.maxValue = timerMax;
        timeSlide.value = timerMax;
    }

  
    private void Update()
    {
        if(isGameplay)
        {
            timer -= (Time.deltaTime);
            if(timer >= 0){timeSlide.value = timer; } else { timeSlide.value = 0; }

        }
        if(timer <= 0)
        {
            onDamagePlayer?.Invoke(eDmg);
            InitializeTimer();
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
        getNewEnemy();
        //eHealth = CombatWordManager.enemyHealth * 5;
        eHealth = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(100 * Math.Pow(2, GameMaster.Difficulty));
        initialEHealth = Convert.ToInt32(eHealth);
        eDmg = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(10 * Math.Pow(5, GameMaster.Difficulty));
        eAgi = (GameMaster.Region * 25 + GameMaster.Level) + Convert.ToInt32(10 * Math.Pow(2, GameMaster.Difficulty));//needs changed
        eDef = 0;//needs changed
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
    private void getNewEnemy()
    {
        int random = UnityEngine.Random.Range(0, allEnemies.Count);
        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (i != random)
            {
                allEnemies[i].SetActive(false);

            }
            else
            {
                SelectedEnemy = allCharacters[i];
            }
        }
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
        enemyAnimator.SetTrigger("attack");

        if (damage-pDef > 0)
        {
            pHealth -= (damage - pDef);
            HPSlider.value -= (damage - pDef);
        }
        else if(damage - pDef <= 0)
        {
            pHealth -= 1;
            HPSlider.value -= 1;
        }
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
        if(pHealth <= 0)
        {
            isGameplay = false;
            characterAnimator.SetTrigger("isDead");
            onPlayerKilled?.Invoke();
        }
        if (isGameplay)
        {
            characterAnimator.SetTrigger("gotHit");
        }
    }

    void enemyTakeDamage(int length)
    {
        enemyAnimator.SetTrigger("gotHit");
        heroParticles.transform.GetChild(length - 3).gameObject.SetActive(true);
        int totalDmg = (pDmg * length * checkCrit()) - eDef;  
        lengthMultiplier += lengthBonus[length-3];
        if (totalDmg >= 2)
        {
            eHealth -= totalDmg;
        }
        else
        {
            eHealth -= 1;
        }
        if(eHealth <= 0)
        {
            StartCoroutine("SpawnParticles");
            enemyAnimator.SetTrigger("isDead");
            stageXP();
            onEnemyKilled?.Invoke();
            
        }
        slider.value = eHealth;
        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }
    private IEnumerator SpawnParticles()
    {
        for (int i = 0; i < 10; i++) //num of coins
        {
            lootPooler.SpawnFromPool("coin", enemyHolder.transform.position, enemyHolder.transform.rotation);
            yield return new WaitForSeconds(.05f);
        }
    }
    int checkCrit()
    {
        int result;
        int roll = UnityEngine.Random.Range(0, 10000);
        if(pCrit >= roll)
        {
            result = 2;
        }
        else
        {
            result = 1;
        }
        return result;
    }

    void spelledWord(int length)
    {
        timer += length * 3 / 10;
        characterAnimator.SetTrigger(length + "letter");
        onDamageEnemy?.Invoke(length);
    }

    void wrongWord()
    {
        timer -= 1f;
    }
    void stageLoot()
    {
        stagedGold += 10;
        stagedShard1 += 1;
        stagedShard2 += 2;
    }
    void stageXP()
    {

        int xpToAdd = Convert.ToInt32(initialEHealth * (1 + lengthMultiplier));
        stagedXP += xpToAdd;
        lengthMultiplier = 0;
    }
    //onLevelComplete
    void levelFinished()
    {
        characterAnimator.SetBool("celebrate", true);
        //add experience as well... dunno why im calling a method that returns an int []??
        vicDefPanel.SetActive(true);
        int[] updatedHero = CharectorStats.EndofLevel(stagedXP);
        InvManager.GoldAdd(stagedGold);
        stagedGold = 0;
        InvManager.T1ShardAdd(stagedShard1);
        stagedShard1 = 0;
        InvManager.T2ShardAdd(stagedShard2);
        stagedShard2 = 0;
        
    }
    void onLeveledUp()
    {
        levelUpText.SetActive(true);
        //string temp = levelUpText.GetComponent<Text>();
        //regex to replace number?

    }

    //onEnemyKilled
    void nextWord()
    {
        timer = timerMax;
        stageLoot();
        foreach(Text stat in allStats)
        {
            stat.text = "";
        }
        //StopCoroutine(CRRef);
       // CRRef = StartCoroutine(CombatTimer());
        wordsLeft--;
        //decide where to set a short timer to allow enemy entrance
        Debug.Log(wordsLeft + " words left");
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
        
        removeBubble();
        CombatWordManager.resetString();
        CombatWordManager.GameOverTrigger();//maybe don't need this line if nothing in that script needs to happen here
        vicDefPanel.SetActive(true);
        vicDefText.text = "Defeated!";
    }

    private void OnDisable()
    {
        //unsubs
        CombatWordManager.onIncorrectWord -= wrongWord;
        CombatWordManager.onCorrectWord -= spelledWord;
        onDamageEnemy -= enemyTakeDamage;
        onDamagePlayer -= playerTakeDamage;
        onEnemyKilled -= nextWord;
        onPlayerKilled -= playerKilled;
        onLevelComplete -= levelFinished;
        CharectorStats.leveledUp -= onLeveledUp;
    }

}
