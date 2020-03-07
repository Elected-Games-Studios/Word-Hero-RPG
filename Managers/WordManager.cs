using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class WordManager : MonoBehaviour
{
    #region DeclaredVariables

    public GameObject victoryChest;
    public static List<GameObject> lastLetterClicked = new List<GameObject>();
    public static bool youHaveWon;
    public static string givenWord;
    public string[] wordList;
    public int length;
    public int enemiesBeaten;
    public static string currentWord;                               //the word you are spelling and adding letters to
    public GameObject foundWords;                                   //a list of the words you have found appearing in the box on the left of the screen
    public static int wordsFound;                                   //the number of words you have found. If you find all of them you win.
    public Text textBox;                                            //a text box in the middle of the screen showing the word you are spelling as you spell it
    public GameObject player;                                       //your player, used to reference his animator in case you find a word
    public static int lettersUsed;
    public LineRenderer line;
    public PlayerCombat combat;
    public Animator currentWordAnim;
    public List<string> wordsUsed = new List<string>();
    public List<GameObject> circles = new List<GameObject>();
    public GraphicRaycaster gRay;
    public EventSystem eventSystem;
    PointerEventData m_PointerEventData;
    public GameObject wordAttack;
    public GameObject limitBreakButton;
    public static GameObject lastLetter;
    public GameObject turnOffOnVictory;
    public GameObject turnOffOnVictory2;
    PlayerStats stats;
    bool isTutorial;
    bool wordIsUsed;
    bool IsSwipeTyping;
    bool isDead;
    bool isEndless;
    bool isBoss;
    private Dictionary dictionary;
    public AudioClip victoryFanfare;
    public AudioClip victoryLoop;
    public List<Animator> victoryAnims;
    public List<GameObject> victoryObjects;
    public int damageTaken;
    public GameObject tipBanner;
    public Text tip;
    int victoryThreshhold1;
    int victoryThreshhold2;
    int starsGiven;
    #endregion
    private void Start()
    {
        damageTaken = 0;
        stats = PlayerStats.Instance;
        victoryThreshhold1 = 5 + (stats.levelSelect / 9) + (stats.regionLoad - 1);
        victoryThreshhold2 = 6 + (stats.levelSelect / 7) + (stats.regionLoad);
        isEndless = false;
        isDead = false;
        enemiesBeaten = 0;
        IsSwipeTyping = false;
        currentWord = "";
        combat = player.GetComponent<PlayerCombat>();
        lastLetterClicked = new List<GameObject>();
        dictionary = GetComponent<Dictionary>();
        if (GetComponent<IsEndless>())
        {
            isEndless = true;
        }
        if (GetComponent<TutorialManager>())
        {
            isTutorial = true;
        }
        if (!isTutorial)
        {
            NewWord();
        }
    }
    private void Awake()
    {
        youHaveWon = false;
        dictionary = GetComponent<Dictionary>();
    }
    private void Update()
    {
        if (gameObject.GetComponent<CombatManager>().mana < 2000)
        {
            limitBreakButton.SetActive(false);
        }
        else
        {
            limitBreakButton.SetActive(true);
        }
        textBox.text = currentWord;
        if (!isDead && !youHaveWon && !CombatManager.Instance.enemySpawning)
        {
            if (!isTutorial)
            {
                ControlTouchInput();
            }
            else
            {
                if (!GetComponent<TutorialManager>().dontFight)
                {
                    ControlTouchInput();
                }
            }

        }
    }
    public void CheckWord()
    {
        bool isAWord = false;
        for (int i = 0; i < wordList.Length; i++)
        {
            if (currentWord.ToLower() == wordList[i])
            {
                isAWord = true;
            }
        }
        if (isAWord)
        {
            Instantiate(wordAttack, textBox.transform);
            GameObject attackWord = GameObject.FindGameObjectWithTag("WordAttack");
            attackWord.GetComponent<Text>().text = currentWord;
            attackWord.GetComponent<Animator>().SetTrigger("hit");
            foundWords.transform.GetChild(wordsFound).GetComponent<Text>().text = currentWord;
            Rect wordBox = foundWords.GetComponent<RectTransform>().rect;
            wordBox.height = (wordsFound / 2 + 1) * 75;
            wordsFound++;
            combat.Attack(currentWord.Length);
            wordsUsed.Add(currentWord);
            currentWord = "";
            lettersUsed = 0;
            line.positionCount = 0;
        }
        else
        {
            Instantiate(wordAttack, textBox.transform);
            GameObject attackWord = GameObject.FindGameObjectWithTag("WordAttack");
            attackWord.GetComponent<Text>().text = currentWord;
            attackWord.GetComponent<Animator>().SetTrigger("break");
            CombatManager.Instance.timer.value -= 1;
            currentWord = "";
            lettersUsed = 0;
            line.positionCount = 0;
        }
        lastLetterClicked.Clear();
    } //Checks the word you input against the array of potentially spelled words. 
    public void ControlTouchInput()
    {
        m_PointerEventData = new PointerEventData(eventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gRay.Raycast(m_PointerEventData, results);
        if (Input.GetButtonDown("Fire1"))
        {
            foreach (RaycastResult hit in results)
            {
                if (hit.gameObject.tag == "Letter")
                {
                    hit.gameObject.GetComponent<ClickThisLetter>().Clicked();
                    IsSwipeTyping = true;
                }
                else
                {
                    //if(lastLetterClicked[lettersUsed-1])
                    //{
                    //    lastLetterClicked[lettersUsed-1].GetComponent<ClickThisLetter>().hasLeftLetter = true;
                    //}
                    //Debug.Log("Has Left Letter");
                }
            }
        }
        if (IsSwipeTyping)
        {
            foreach (RaycastResult hit in results)
            {
                if (hit.gameObject.tag == "Letter")
                {
                    if (IsSwipeTyping)
                    {
                        hit.gameObject.GetComponent<ClickThisLetter>().Clicked();
                        IsSwipeTyping = true;
                    }
                }

            }
        }
        if (Input.GetButtonUp("Fire1") && IsSwipeTyping)
        {
            IsSwipeTyping = false;
            if (currentWord.Length >= 1)
            {
                wordIsUsed = false;
                for (int i = 0; i < wordsUsed.Count; i++)
                {
                    if (currentWord == wordsUsed[i])
                    {
                        wordIsUsed = true;
                    }
                }
                if (!wordIsUsed)
                {
                    CheckWord();
                }
                else
                {
                    CombatManager.Instance.timer.value -= 1;
                    currentWord = "";
                    lettersUsed = 0;
                    line.positionCount = 0;
                }
            }
        }
    } //Runs the events upon touching the screen.
    public void ClearUsedWords()
    {
        for (int i = 0; i < wordsFound; i++)
        {
            foundWords.transform.GetChild(i).GetComponent<Text>().text = "";
        }
        wordsFound = 0;
        wordsUsed.RemoveRange(0, wordsUsed.Count);
        //Debug.Log("Words Cleared ***manager"); 
    } //Clears the words you've already spelled. Used upon beating an enemy or when you use the Second Wind Powerup.
    public void Shuffle()
    {
        int wordLength = givenWord.Length - 4;
        for (int i = 0; i < circles.Count; i++)     //erase the letters in all circles to clean the display
        {
            for (int j = 0; j < circles[i].transform.childCount; j++)
            {
                circles[i].transform.GetChild(j).gameObject.GetComponent<Text>().text = "";
            }
            circles[i].SetActive(false);
        }
        char[] letters = givenWord.ToUpper().ToCharArray();                                            //convert the given word to an array of uppercase letters
        int circleSize = wordLength;
        List<Transform> locations = new List<Transform>();
        circles[circleSize].SetActive(true);
        for (int j = 0; j < circles[circleSize].transform.childCount; j++)
        {
            circles[circleSize].transform.GetChild(j).gameObject.GetComponent<Text>().text = "";
            locations.Add(circles[circleSize].transform.GetChild(j));
        }
        for (int i = 0; i < givenWord.Length; i++)                                                       //assign each letter to a random empty position in the circle
        {
            int j = UnityEngine.Random.Range(0, circles.Count - 1);
            while (locations[j].GetComponent<Text>().text != "")
            {
                j -= 1;
                if (j < 0)
                    j = givenWord.Length - 1;
            }
            locations[j].GetComponent<Text>().text = letters[i].ToString();
        }
    } //takes the letters in the given word and shuffes them around the circle of the correct size. 
    public string BiggestWord(string[] list)
    {
        string biggestWord;
        biggestWord = list[1];
        for (int i = 1; i < list.Length; i++)
        {
            if (list[i].Length > biggestWord.Length)
            {
                biggestWord = list[i];
            }
        }
        Debug.Log("The biggest word is " + biggestWord);
        return biggestWord;
    } //Checks which word in the array taken from the dictionary is biggest to set as the given word for the level. 
    public void CheckVictory()
    {
        if (enemiesBeaten == dictionary.totalEnemies)
        {
            youHaveWon = true;
            if (stats.levelSelect == 25)
            {
                BossVictory();
            }
            else
            {
                Victory();
            }
        }
    }
    public void NewWord()
    {
        if (!isTutorial)
        {
            GetComponent<PowerUpManager>().timeStopBorder.SetActive(false);
        }
        IsSwipeTyping = false;

        //set word length for endless based on progress

        if (!isEndless)
        {
            wordList = dictionary.GetWordsForWorlds(stats.levelSelect);
        }
        else
        {
            //Debug.Log("This is not endless Mode");
            length = (enemiesBeaten / 25) + 4;
            wordList = dictionary.GetWord(length);
        }
        givenWord = BiggestWord(wordList);
        Shuffle();

        ClearUsedWords();
    } //Checks if this is endless or not and gets a new word from the appropriate list
    public void Die()
    {
        isDead = true;
    } //Sets a local variable for other functions that check if the player is dead.
    public void LimitBreak()
    {
        combat.Attack(10);
    } //The command the limit break button references to trigger the Player's super attack. 
    void Victory()
    {
        stats.heroMana = GetComponent<CombatManager>().mana;
        if (!isTutorial)
        {
            GetComponent<PowerUpManager>().timeStopBorder.SetActive(false);
        }
        combat.anim.SetBool("inCombat", false);
        combat.anim.SetTrigger("victory");

        StartCoroutine("VictoryMusic");
        starsGiven = 1;
        if (damageTaken < victoryThreshhold2)
        {
            starsGiven = 2;
        }
        if (damageTaken < victoryThreshhold1)
        {
            starsGiven = 3;
        }
        if (starsGiven < 3)
        {
            tipBanner.SetActive(true);
            tip.text = "Next time try taking less than " + victoryThreshhold1 + " damage to get three stars.";
        }
        SetStars(starsGiven);
        Debug.Log("______VICTORY!!______");
        if (stats.regionProgress == stats.regionLoad && stats.levelProgress == stats.levelSelect)
        {
            stats.levelProgress++;
            if (stats.levelProgress > 25 * stats.regionProgress)
            {
                stats.levelProgress = 0;
                stats.regionProgress++;
            }
        }
        dictionary.UpdateEnemyCount();
        for (int i = 0; i < 4; i++)
        {
            victoryObjects[i].SetActive(true);
            victoryAnims[i].SetTrigger("victory");
        }
        for (int i = 0; i < starsGiven; i++)
        {
            victoryObjects[0].transform.GetChild(i).gameObject.SetActive(true);
        }
        PlayServices.Instance.SaveData();
        turnOffOnVictory.SetActive(false);
        turnOffOnVictory2.SetActive(false);
    } //Runs events upon victory conditions.
    void BossVictory()
    {
        isBoss = true;
        combat.anim.SetBool("inCombat", false);
        combat.anim.SetTrigger("victory");
        StartCoroutine("VictoryMusic");
        starsGiven = 1;
        if (damageTaken < victoryThreshhold2)
        {
            starsGiven = 2;
        }
        if (damageTaken < victoryThreshhold1)
        {
            starsGiven = 3;
        }
        if (starsGiven < 3)
        {
            tipBanner.SetActive(true);
            tip.text = "Get 3 stars on the boss level for maximum rewards! Next time, take less than " + victoryThreshhold1 + " damage to get 3 stars.";
        }
        SetStars(starsGiven);
        //Debug.Log("______VICTORY!!______");
        if (stats.regionProgress == stats.regionLoad && stats.levelProgress == stats.levelSelect)
        {
            stats.levelProgress = 0;
            stats.regionProgress++;
        }
        dictionary.UpdateEnemyCount();
        for (int i = 0; i < 4; i++)
        {
            victoryObjects[i].SetActive(true);
            victoryAnims[i].SetTrigger("victory");
        }
        for (int i = 0; i < starsGiven; i++)
        {
            victoryObjects[0].transform.GetChild(i).gameObject.SetActive(true);
        }
        PlayServices.Instance.SaveData();
        turnOffOnVictory.SetActive(false);
        turnOffOnVictory2.SetActive(false);
    } //Runs events upon victory conditions.
    IEnumerator VictoryMusic()  //Changes the music to victory fanfare then loop upon victory conditions.
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().StopMusic();
        AudioSource audio = Camera.main.gameObject.GetComponent<AudioSource>();
        audio.clip = victoryFanfare;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = victoryLoop;
        audio.Play();
    }
    private void SetStars(int stars)  //checks by name of string for the stars count you've gotten before, checks against current stars, and if greater, sets and saves.
    {
        int starLevel = (((stats.regionLoad - 1) * 25) - 1) + (stats.levelSelect);
        if (isBoss)
        {
            victoryChest.SetActive(true);
            victoryChest.GetComponent<BossRewards>().Reward(stars);
        }
        if (stats.levelStars[starLevel] < stars)
        {
            stats.levelStars[starLevel] = stars;
        }
        PlayServices.Instance.SaveData();
    }
}