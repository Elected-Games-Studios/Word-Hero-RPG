using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public GameObject statImages;
    public Animator redFlash;
    public Text heroDescription;
    public Text levelUpdate;
    public Slider xpBar;
    public string heroName;
    public GameObject bubbleCover;
    public static CombatManager Instance;
    public Slider timer;
    public Slider manaBar;
    public GameObject enemy;
    public float mana;
    float timerMax;
    PlayerStats stats;
    public GameObject shieldText;
    public GameObject deathMenu;
    bool isDead;
    public bool enemySpawning;
    public int timersUsed;
    public bool isTutorial;

    PlayServices cloud;
    public AdManager adManager;

    


    private void Awake()
    {
        InvokeRepeating("CallRequestBanner", 0, 30);
        if (AdManager.instance.mainBanner != null)
        {
            Debug.Log("Invoke repeating worked");
        }
        isDead = false;
        stats = PlayerStats.Instance;
        Instance = this;
        timerMax = 5 + stats.playerSpeed;
        timer.maxValue = timerMax;
        timer.value = timerMax;
        if(GetComponent<TutorialManager>())
        {
            enemySpawning = true;
            isTutorial = true;
        }
        cloud = PlayServices.Instance;
        UpdateLevel();
        mana = stats.heroMana;
    }

    void Update()
    {
        if(!isDead && !enemySpawning)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            timer.value -= (1 * Time.deltaTime)/(timersUsed+1);
        }
        if(timer.value <= 0)
        {
            if(enemy != null)
            {
                if (PowerUpManager.shieldPower)
                {
                    PowerUpManager.shieldPower = false;
                    Instantiate(shieldText, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
                }
                else
                {
                    int i = Random.Range(0, 100);
                    if (stats.playerEvade >= i || isTutorial)
                    {
                        enemy.GetComponent<EnemyCombat>().AttackDodged();
                    }
                    else
                    {
                        enemy.GetComponent<EnemyCombat>().Attack();
                        redFlash.SetTrigger("gotHit");
                    }
                }
            }
            timer.value = timerMax;
        }
        if(mana < 2000 & !isTutorial)
        {
            mana += Time.deltaTime;
            manaBar.value = mana;
        }
        xpBar.value = stats.heroXP;
        xpBar.maxValue = stats.xpToLevel;
        LevelUp();
        heroDescription.text = "Lvl " + stats.heroLevel + " " + heroName;
    }


    public void CallRequestBanner()
    {
        AdManager.instance.RequestBanner();
    }
    public void Die()
    {
        isDead = true;
        if(!gameObject.GetComponent<TutorialManager>())
        {
            deathMenu.SetActive(true);
        }

    }

    public void EnemySpawn()
    {
        enemySpawning = true;
        StartCoroutine("LetThemCome");
    }

    IEnumerator LetThemCome()
    {
        bubbleCover.SetActive(true);
        GetComponent<WordManager>().NewWord();
        GameObject.FindGameObjectWithTag("Spawn").GetComponent<EnemySpawn>().SpawnEnemy();

        yield return new WaitForSeconds(2.5f);

        enemySpawning = false;
        timer.value = timer.maxValue;
        bubbleCover.SetActive(false);
    }

    public void LevelUp()
    {
        if (stats.heroXP >= stats.xpToLevel)
        {
            stats.skillPoints++;
            stats.heroXP -= stats.xpToLevel;
            stats.xpToLevel += stats.xpIncrease;
            stats.xpIncrease += 150;
            stats.heroLevel++;
            statImages.GetComponent<Animator>().SetTrigger("levelUp");
            cloud.SaveData();
        }
    }

    void UpdateLevel()
    {
        if(gameObject.GetComponent<IsEndless>())
        {
            levelUpdate.text = "Endless";
        }
        else if(levelUpdate != null)
        {
            levelUpdate.text = "Level: " + stats.levelSelect;
        }

    }
}
