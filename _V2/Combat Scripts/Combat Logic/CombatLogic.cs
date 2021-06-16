using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogic : MonoBehaviour
{
    public static CombatLogic instance;
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
    private bool damageIsCritical;
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
    private TextMeshProUGUI vicDefText;
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
    private List<GameObject> allEnemyPlayerClasses;
    [SerializeField]
    private TextMeshProUGUI xpText;
    [SerializeField]
    private GameObject ContinueButton;
    //temporary
    [SerializeField]
    private Text toSpell;
    private Slider HPSlider;
    private TextMeshProUGUI HPText;
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

    private bool isPvP = false;

    public event Action<int> onDamageEnemy, onDamagePlayer;
    public event Action onLevelComplete, onPlayerKilled, onEnemyKilled;
   // Coroutine CRRef;

    void Awake()
    {
        if (instance == null)
            instance = this;
        bgTween.AssignBackgroundsForRegion(region);
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
        //onDamageEnemy += enemyGotHitAnim;
        onDamagePlayer += playerTakeDamage;
        onDamagePlayer += enemyAttackAnim;
        onEnemyKilled += nextWord;
        onEnemyKilled += stageLoot;
        //onEnemyKilled += enemyIsDeadAnim;
        onLevelComplete += levelFinished;
        onLevelComplete += heroCelebrateAnim;
        onPlayerKilled += gameOverSequence;
        onPlayerKilled += removeBubble;
        onLevelComplete += enableContinueButton;
        onPlayerKilled += disableContinueButton;
        InitializePlayer();
        InitializeEnemy();
        displayNewEnemyPrefab();
        InitializeTimer(); 

        isGameplay = true;
        //StartCoroutine("enemyWalk");
        CombatWordManager.StartLevel();
        wordsLeft = CombatWordManager.Words.Count;
    }

    private void InitializeTimer()
    {
        if (!isPvP)
        {
            timerMax = 5 + (pAgi - eAgi) / 100;
            timer = timerMax;
            timeSlide.maxValue = timerMax;
            timeSlide.value = timerMax;
        }
        if (isPvP)
        {
            //Change timer color for pvp?
            timerMax = 10; //if no words are spelled by timer value between either player, new word called
            timer = timerMax;
            timeSlide.maxValue = timerMax;
            timeSlide.value = timerMax;
        }
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
        if (timer <= 0)
        {
            if (!isPvP)
                onDamagePlayer?.Invoke(eDmg);
            else { 
            //SELECT NEW WORD FOR PVP
            }
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
        TextMeshProUGUI [] lettersArr = currentBubble.GetComponentsInChildren<TextMeshProUGUI>();
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
        if (!isPvP)
        {
            //displayNewEnemyPrefab();
            eHealth = (region * 25 + level) + Convert.ToInt32(100 * Math.Pow(2, difficulty));
            initialEHealth = Convert.ToInt32(eHealth);
            eDmg = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(5, difficulty));
            eAgi = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(2, difficulty));//needs changed
            eDef = 0;//needs changed
        }
        if (isPvP)
        {
            //displayEnemyPlayer();
            //RETRIEVE SERVER VALS
            eHealth = (region * 25 + level) + Convert.ToInt32(100 * Math.Pow(2, difficulty));
            initialEHealth = Convert.ToInt32(eHealth);
            eDmg = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(5, difficulty));
            eAgi = (region * 25 + level) + Convert.ToInt32(10 * Math.Pow(2, difficulty));//needs changed
            eDef = 0;
        }    
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

    public void displayNewEnemyPrefab()
    {
        isGameplay = true;
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
        enemyAnimator = enemyHolder.GetComponentInChildren<Animator>();
    }

    private void displayEnemyPlayer()
    {
        //GET ENEMY CLASS FROM SERVER
        int enemyClassIndex = 0; //< From server
        SelectedEnemy = allEnemyPlayerClasses[enemyClassIndex];
    }

    private void InitializePlayer()
    {
        allStats[4].text += pHealth;
        allStats[5].text += pDmg;
        allStats[6].text += pAgi;
        allStats[7].text += pCrit;
        allStats[8].text += pDef;

        HPSlider = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Slider>();
        HPText = HPSlider.GetComponentInChildren<TextMeshProUGUI>();
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
    void enemyGotHitAnim()
    {
        enemyAnimator.SetTrigger("gotHit");
    }
    void enemyIsDeadAnim()
    {
        Debug.Log("Enemy is fucking ded");
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
        DamagePopup.Create(enemyHolder.transform.position, totalDmg, damageIsCritical);
        
        if(eHealth <= 0)
        {
            enemyIsDeadAnim();
            isGameplay = false;
        }
        else
        {
            enemyGotHitAnim();
        }
        eHealthSlider.value = eHealth;
        //temp
        toSpell.text = string.Join(" ", CombatWordManager.currentUsableWords);
    }

    int checkCrit()
    {
        int result;
        int roll = UnityEngine.Random.Range(0, 10000);
        if (pCrit >= roll) {
            result = 2;
            damageIsCritical = true;
        } else {
            result = 1;
            damageIsCritical = true;
        }
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
        GameMaster.lastCompletedLevel = GameMaster.Level;
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
    public void enemyIsDying()
    {
        onEnemyKilled?.Invoke();
    }
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

    void enableContinueButton()
    {
        ContinueButton.GetComponent<Button>().interactable = true;
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
        vicDefPanel.SetActive(true);
        vicDefText.text = "Defeated!";
        CombatWordManager.resetString();
        CombatWordManager.GameOverTrigger();//maybe don't need this line if nothing in that script needs to happen here

    }
    void disableContinueButton()
    {
        ContinueButton.GetComponent<Button>().interactable = false;
    }
    #endregion

    private void OnDisable()
    {
        //unsubs
        CombatWordManager.onIncorrectWord -= wrongWord;
        CombatWordManager.onCorrectWord -= spelledWord;
        onDamageEnemy -= enemyTakeDamage;
        //onDamageEnemy -= enemyGotHitAnim;
        onDamagePlayer -= playerTakeDamage;
        onDamagePlayer -= enemyAttackAnim;
        onEnemyKilled -= nextWord;
        onEnemyKilled -= stageLoot;
        //onEnemyKilled -= enemyIsDeadAnim;
        onPlayerKilled -= gameOverSequence;
        onPlayerKilled -= removeBubble;
        onLevelComplete -= enableContinueButton;
        onPlayerKilled -= disableContinueButton;
        onLevelComplete -= levelFinished;
        onLevelComplete -= heroCelebrateAnim;
        CharectorStats.leveledUp -= onLeveledUp;
    }

}
