using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCombat : MonoBehaviour
{
    PlayerCombat player;
    Animator anim;
    public int enemyHealth;
    WordManager wordMan;
    Slider hpBar;
    bool isDead;
    bool justSpawned = true;
    public float xpMulti;
    public int xpGiven;
    public GameObject xpGivenGO;
    public GameObject evadeText;
    bool isABoss;

    private void Awake()
    {
        xpMulti = 1;
        anim = GetComponent<Animator>();
        isDead = true;
        wordMan = GameObject.FindGameObjectWithTag("Manager").GetComponent<WordManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        hpBar = GetComponentInChildren<Slider>();
        justSpawned = true;
    }

    private void Start()
    {
        if (wordMan!= null)
        {
            if (wordMan.GetComponent<TutorialManager>())
            {
                GetHealth(wordMan.wordList);
                if (enemyHealth <= 0)
                    enemyHealth = 30;
            }
            else
            {
                GetHealth(wordMan.wordList);
            }
        }
        else
        {
            enemyHealth = 4000;
        }

        xpGiven = enemyHealth;
        hpBar.maxValue = enemyHealth;
        isDead = false;
    }

    private void Update()
    {
        if(justSpawned)
        {
            anim.SetBool("isRunning", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1.3f, 0));
        }
        else
        {
            anim.SetBool("isRunning", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0));
        }

        hpBar.value = enemyHealth;
        if(enemyHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
        if(isABoss)
        {
            GetComponent<BossEnemy>().Attack1();
        }
        if (gameObject.name.Contains("YeetClops"))
        {
            player.playerHealth -= 4;
        }
        else
        {
            player.playerHealth -= 1;
        }

        player.anim.SetTrigger("gotHit");
        wordMan.damageTaken++;
        Camera.main.GetComponent<Animator>().SetTrigger("gotHit");
    }

    public void AttackDodged()
    {
        anim.SetTrigger("attack");
        if (isABoss)
        {
            GetComponent<BossEnemy>().Attack1();
        }
        if (!GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>())
        {
            Instantiate(evadeText, player.gameObject.transform);
        }
        xpMulti += .5f;
    }

    public void Shielded()
    {
        anim.SetTrigger("attack");
        if (isABoss)
        {
            GetComponent<BossEnemy>().Attack1();
        }
        player.anim.SetTrigger("gotHit");
        Instantiate(evadeText, player.gameObject.transform);
    }
    public void Death()
    {
        if(!gameObject.name.Contains("YeetClops"))
        {
            Destroy(gameObject, 1.5f);
        }
        justSpawned = false;
        isDead = true;
        hpBar.gameObject.SetActive(false);
        if (!GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>())
        {
            xpGiven = (int)(xpGiven * xpMulti);
            Instantiate(xpGivenGO, GameObject.FindGameObjectWithTag("Combat").transform);
        }
        else
        {
            xpGiven = 0;
            GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>().ProgressTutorial();
        }
        xpGiven = (int)(xpGiven * xpMulti);
        StartCoroutine("WaitForDeath");
        PlayerStats.Instance.heroXP += xpGiven;
        CombatManager.Instance.timersUsed = 0;
        PlayServices.Instance.SaveData();
        wordMan.enemiesBeaten++;
        wordMan.GetComponent<WordManager>().CheckVictory();

        if(wordMan.gameObject.GetComponent<TutorialManager>())
        {
            TutorialManager.isFighting = false;
            wordMan.gameObject.GetComponent<TutorialManager>().ProgressTutorial();
            return;
        }
        if(WordManager.youHaveWon)
        {
            return;
        }
        else
        {
            CombatManager.Instance.EnemySpawn();
        }
        //GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>().timer.value += GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>().timer.maxValue;

    }
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("isDead");
    }
    
    public void GetHealth(string[] words)
    {
        enemyHealth = 1;
        for(int i = 1; i < words.Length; i++)
        {
            enemyHealth += words[i].Length;
            Debug.Log(words[i]);
        }
        enemyHealth -= 1;
        enemyHealth *= 3;
        enemyHealth /= 4;
        enemyHealth *= 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Combat")
        {
            justSpawned = false;
        }
    }
}