using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    //for simplicity
    int region = GameMaster.Region;
    int level = GameMaster.Level;
    int difficulty = GameMaster.Difficulty;

    //references to player, enemy, letter nodes
    private int pDmg, pHealth, pCrit, pAgi, pDef;
    private int eDmg, eAgi, eHealth, eDef, initialEHealth;
    private double lengthMultiplier;
    public bool isGameplay = true;
    
    public int stagedXP{ get; private set; }
    public int stagedShard1 { get; private set; }
    public int stagedShard2 { get; private set; }
    public int stagedGold { get; private set; }

    private double[] lengthBonus = { .1, .25, .5, .75, 1.5, 3 };
    private double[] diffBonus = { .8, 1, 1.2 };
    private List<string> words = CombatWordManager.Words;
    int wordsLeft;

    //UI Stuff
    [SerializeField]
    private BackgroundTweener bgTween;
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
    [SerializeField]
    private Text xpText;
    //temporary
    [SerializeField]
    private Text toSpell;
    private Slider HPSlider;
    private Text HPText;
    private Slider eHealthSlider;
    int[] updatedHero;

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
        bgTween.changeBackground(level);
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
        SelectedEnemy = allEnemies[0];
        characterAnimator = characterHolder.GetComponentInChildren<Animator>();
        enemyAnimator = enemyHolder.GetComponentInChildren<Animator>();
        heroParticles = SelectedHero.transform.Find("HeroAttackParticles").gameObject;
        characterAnimator.SetBool("inCombat", true);
        bubbles = GetComponentsInChildren<Image>();
        foreach (Image bubble in bubbles) { bubble.gameObject.SetActive(false); }

        int[] heroStats = CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero());
        SetXPText();
        pDmg = heroStats[4];
        pHealth = heroStats[5];
        pCrit = heroStats[6];
        pAgi = heroStats[7];
        pDef = heroStats[8];
        lengthMultiplier = 0;
        
        stagedGold = 0;
        stagedShard1 = 0;
        stagedShard2 = 0;
 
        //event subs
        CombatWordManager.onMaxLengthFound += generateBubble;
        CombatWordManager.onCorrectWord += spelledWord;
        CombatWordManager.onIncorrectWord += wrongWord;
        CharectorStats.leveledUp += onLeveledUp;
        onDamageEnemy += enemyTakeDamage;
        onDamageEnemy += enemyGotHitAnim;
        onDamagePlayer += playerTakeDamage;
        onDamagePlayer += enemyAttackAnim;
        onEnemyKilled += nextWord;
        onEnemyKilled += stageLoot;
        onEnemyKilled += enemyIsDeadAnim;
        onLevelComplete += levelFinished;
        onLevelComplete += heroCelebrateAnim;
        onPlayerKilled += gameOverSequence;
        onPlayerKilled += removeBubble;
        InitializePlayer();
        InitializeEnemy();
        InitializeTimer();
        isGameplay = true;
        //StartCoroutine("enemyWalk");
        CombatWordManager.StartLevel();
        wordsLeft = CombatWordManager.Words.Count;
        Debug.Log(wordsLeft + " words left");

    }
    
    private void InitializeTimer()
    {
        timerMax = 5 + (pAgi - eAgi) / 100;
        timer = timerMax;
        timeSlide.maxValue = timerMax;
        timeSlide.value = timerMax;
    }
    private void SetXPText()
    {
        if (updatedHero != null )
        {
            xpText.text = updatedHero[2].ToString();
        }
        else
        {
            xpText.text = "";
        }
      
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

    #region Bubbles
    public void generateBubble(int length) //called once to choose bubble of longest word size, don't need to re-render
    {

        bubbles[length - 4].gameObject.SetActive(true);
        populateBubble();
        CombatWordManager.onMaxLengthFound -= generateBubble;
    }
    void removeBubble()//Activated by onPlayerKilled
    {
        currentBubble.SetActive(false);
    }
    public void populateBubble()
    {
        int length = CombatWordManager.longestWord.Length;
        currentBubble = bubbles[length - 4].gameObject;
        Text [] lettersArr = currentBubble.GetComponentsInChildren<Text>();
        for(int i = 0; i < length; i++)
        {
            lettersArr[i].text = CombatWordManager.shuffledWord[i].ToUpper();
        }

        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }
    #endregion

    #region Enemy And Player Initialization
    private void InitializeEnemy()//must be called after WordBreak()
    {
        displayNewEnemyPrefab();
        
        eHealth = (region * 25 + level) + Convert.ToInt32(100 * Math.Pow(2, difficulty));
        initialEHealth = Convert.ToInt32(eHealth);
        eDmg = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(5, difficulty));
        eAgi = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(2, difficulty));//needs changed
        eDef = 0;//needs changed
        allStats[0].text += eHealth;
        allStats[1].text += eDmg;
        allStats[2].text += eAgi;
        allStats[3].text += eDef;
        //temp
        eHealthSlider = GameObject.FindGameObjectWithTag("EnemyHP").GetComponent<Slider>();
        eHealthSlider.maxValue = eHealth;
        eHealthSlider.value = eHealth;
        //^ end temp
    }
    private void displayNewEnemyPrefab()
    {       
        int prevIdx = allEnemies.IndexOf(SelectedEnemy);
        SelectedEnemy.SetActive(false);
        int random = UnityEngine.Random.Range(0, allEnemies.Count);
        while(random == prevIdx) { random = UnityEngine.Random.Range(0, allEnemies.Count); }
        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (i != random && i != prevIdx)
            {
                allEnemies[i].SetActive(false);
            }                               
        }
        SelectedEnemy = allEnemies[random];
        SelectedEnemy.SetActive(true);
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
    #endregion

    #region Animators and Particles
    //ALL ANIMS ACTIVATED BY APPROPRIATE EVENT TRIGGERS
    void enemyAttackAnim(int damage)//int satisfies delegate
    {
        enemyAnimator.SetTrigger("attack");
    }
    void enemyGotHitAnim(int damage)//int satisfies delegate
    {
        enemyAnimator.SetTrigger("gotHit");
    }
    void enemyIsDeadAnim()
    {
        enemyAnimator.SetTrigger("isDead");
    }
    void playerGotHitAnim()
    {
        if (isGameplay){characterAnimator.SetTrigger("gotHit"); } else { characterAnimator.SetTrigger("isDead"); }
    }
    void heroCelebrateAnim()
    {
        characterAnimator.SetBool("celebrate", true);
    }
    void StartSpawnParticles() {
        StartCoroutine("SpawnParticles");
    }
    private IEnumerator SpawnParticles()
    {
        for (int i = 0; i < 10; i++) //num of coins
        {
            lootPooler.SpawnFromPool("coin", enemyHolder.transform.position, enemyHolder.transform.rotation);
            yield return new WaitForSeconds(.05f);
        }
    }
    #endregion

    #region Logic Methods
    void spelledWord(int length) //Activated by onCorrectWord
    {
        timer += length * 3 / 10;
        characterAnimator.SetTrigger(length + "letter");
        onDamageEnemy?.Invoke(length);
    }

    void wrongWord()
    {
        timer -= 1f;
    }

    void enemyTakeDamage(int length)
    {       
        heroParticles.transform.GetChild(length - 3).gameObject.SetActive(true);
        int totalDmg = (pDmg * length * checkCrit()) - eDef;  
        lengthMultiplier += lengthBonus[length-3];
        if (totalDmg >= 2) {eHealth -= totalDmg;} else { eHealth -= 1; }
        if(eHealth <= 0)
        {
            onEnemyKilled?.Invoke();          
        }
        eHealthSlider.value = eHealth;
        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }

    int checkCrit()
    {
        int result;
        int roll = UnityEngine.Random.Range(0, 10000);
        if (pCrit >= roll) {result = 2;} else {result = 1;}
        return result;
    }

    void playerTakeDamage(int damage)
    {
        if (damage - pDef > 0)
        {
            pHealth -= (damage - pDef);
            HPSlider.value -= (damage - pDef);
        }
        else if (damage - pDef <= 0)
        {
            pHealth -= 1;
            HPSlider.value -= 1;
        }
        HPText.text = HPSlider.value.ToString() + "/" + HPSlider.maxValue.ToString() + "  ";
        if (pHealth <= 0)
        {
            onPlayerKilled?.Invoke();
        }
    }
    #endregion

    void onLeveledUp()//Panel Display functions should be rewritten on Panel Script
    {
        levelUpText.SetActive(true);
    }

    #region Activated by onLevelComplete
    void levelFinished()
    {         
        InvManager.GoldAdd(stagedGold);
        stagedGold = 0;
        InvManager.T1ShardAdd(stagedShard1);
        stagedShard1 = 0;
        InvManager.T2ShardAdd(stagedShard2);
        stagedShard2 = 0;
        WordDatav2.endOfLevelStats(region, difficulty, level, 10, 3);
        vicDefPanel.SetActive(true);
    }
    #endregion

    #region Activated by onEnemyKilled
    void nextWord()
    {
        timer = timerMax;
        foreach(Text stat in allStats)
        {
            stat.text = "";
        }
        wordsLeft--;
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
    void stageLoot()
    {
        stagedGold += 10;
        stagedShard1 += 1;
        stagedShard2 += 2;
        gainXP();//XP will carry between rounds if defeated--not actually staged per function name
    }
    void gainXP()
    {
        int xpToAdd = Convert.ToInt32(initialEHealth * (1 + lengthMultiplier) * diffBonus[difficulty]);
        //stagedXP += xpToAdd;
        updatedHero = CharectorStats.GainXPFromKill(xpToAdd);
        SetXPText();
        lengthMultiplier = 0;
    }
    #endregion

    #region Activated by onPlayerKilled
    void gameOverSequence()
    {      
        CombatWordManager.resetString();
        CombatWordManager.GameOverTrigger();//maybe don't need this line if nothing in that script needs to happen here
        vicDefPanel.SetActive(true);
        vicDefText.text = "Defeated!";
    }
    #endregion

    private void OnDisable()
    {
        //unsubs
        CombatWordManager.onIncorrectWord -= wrongWord;
        CombatWordManager.onCorrectWord -= spelledWord;
        onDamageEnemy -= enemyTakeDamage;
        onDamageEnemy -= enemyGotHitAnim;
        onDamagePlayer -= playerTakeDamage;
        onDamagePlayer -= enemyAttackAnim;
        onEnemyKilled -= nextWord;
        onEnemyKilled -= stageLoot;
        onEnemyKilled -= enemyIsDeadAnim;
        onPlayerKilled -= gameOverSequence;
        onPlayerKilled -= removeBubble;
        onLevelComplete -= levelFinished;
        onLevelComplete -= heroCelebrateAnim;
        CharectorStats.leveledUp -= onLeveledUp;
    }

}
