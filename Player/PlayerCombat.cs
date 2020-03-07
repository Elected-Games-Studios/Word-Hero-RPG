using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public int playerHealth;
    public int givenDamage;
    public GameObject enemy;
    public Animator anim;
    public Slider hpBar;
    bool isDead;
    public GameObject dmgText;
    public GameObject critText;
    CombatManager comMan;
    bool crit;
    PlayerStats stats;
    AttackAnims attacks;
    bool isTutorial;

    public GameObject hero;

    private void Awake()
    {
        AdManager.instance.RequestDeathAd();
        stats = PlayerStats.Instance;
        comMan = GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>();
        if (comMan != null)
        {
            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>())
            {
                isTutorial = true;
                Debug.Log("This is the Tutorial");
            }

        }

        playerHealth = 10 + stats.playerHealth;

        isDead = false;

        hero = null;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        if(!isTutorial)
        {
            gameObject.transform.GetChild(stats.activeHero).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }


        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                hero = gameObject.transform.GetChild(i).gameObject;
            }
        }

        anim = hero.GetComponent<Animator>();
        attacks = hero.GetComponent<AttackAnims>();
        if (!isTutorial)
        {
            anim.SetBool("inCombat", true);
            SetHpBar();
        }

    }

    public void DeathAdShow()
    {
        AdManager.instance.deathAd.Show();
    }
    public void SetHpBar()
    {
        hpBar = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Slider>();
        hpBar.maxValue = playerHealth;
        isTutorial = false;
    }
    private void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (!isTutorial)
        {
            hpBar.value = playerHealth;
        }


        if (playerHealth <= 0 && !isDead)
        {
            Die();
            Invoke("DeathAdShow", 1.5f);
        }
    }

    public void Attack(int damage)
    {
        if (damage == 3)
        {
            anim.SetTrigger("attack1");
            damage *= 10;
            comMan.timer.value += 1;
            attacks.Attack1();
            enemy.GetComponent<EnemyCombat>().xpMulti += 0;
        }
        else if (damage == 4)
        {
            anim.SetTrigger("attack2");
            damage *= 12;
            comMan.timer.value += 2;
            attacks.Attack2();
            enemy.GetComponent<EnemyCombat>().xpMulti += 0.1f;
        }
        else if (damage == 5)
        {
            anim.SetTrigger("attack3");
            damage *= 15;
            comMan.timer.value += 3;
            attacks.Attack3();
            enemy.GetComponent<EnemyCombat>().xpMulti += 0.25f;
        }
        else if (damage == 6)
        {
            anim.SetTrigger("attack4");
            damage *= 18;
            comMan.timer.value += 4;
            attacks.Attack4();
            enemy.GetComponent<EnemyCombat>().xpMulti += 1;
        }
        else if (damage == 7)
        {
            anim.SetTrigger("attack5");
            damage *= 21;
            comMan.timer.value += 5;
            attacks.Attack5();
            enemy.GetComponent<EnemyCombat>().xpMulti += 2;
        }
        else if (damage == 8)
        {
            anim.SetTrigger("attack6");
            damage *= 25;
            comMan.timer.value += 6;
            attacks.Attack6();
            enemy.GetComponent<EnemyCombat>().xpMulti += 5;
        }
        else if (damage == 10)
        {
            anim.SetTrigger("specialAttack");
            damage = (10*stats.heroLevel) + (10*stats.playerDamage);
            comMan.timer.value += 10;
            attacks.LimitBreak();
            enemy.GetComponent<EnemyCombat>().xpMulti += 2.5f;
        }
        damage += PlayerStats.Instance.playerDamage;
        comMan.mana += damage;

        CheckCrit();

        if (crit)
        {
            damage *= 2;
        }

        givenDamage = damage;
        enemy.GetComponent<Animator>().SetTrigger("gotHit");

        enemy.GetComponent<EnemyCombat>().enemyHealth -= damage;
        if (enemy.GetComponent<EnemyCombat>().enemyHealth > 0)
        {
            if (crit)
            {
                Instantiate(critText, GameObject.FindGameObjectWithTag("Combat").GetComponent<Transform>());
            }
            else
            {
                Instantiate(dmgText, GameObject.FindGameObjectWithTag("Combat").GetComponent<Transform>());
            }
        }
    }

    void CheckCrit()
    {
        int i = UnityEngine.Random.Range(0, 100);
        if (i < PlayerStats.Instance.playerLuck)
        {
            crit = true;
            enemy.GetComponent<EnemyCombat>().xpMulti += .25f;
        }
        else
        {
            crit = false;
        }
    }
    private void Die()
    {
        Debug.Log("He's dead");
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>())
        {
            Debug.Log("He's dead and it's the tutorial");
            anim.SetTrigger("getYeeted");
            enemy.GetComponent<Animator>().SetTrigger("throw");

            Debug.Log("He's dead and he got yeeted");
            isDead = true;
            if(stats.regionProgress == 0)
            {
                stats.regionProgress = 1;
            }
            Invoke("NextScene", 3.5f);
            return;
            Debug.Log("He's dead but wait... this is after the return?");
        }
        else
        {
            Debug.Log("He's dead and this is not the tutorial");
            isDead = true;
            if (GameObject.FindGameObjectWithTag("Music"))
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().StopMusic();
                Debug.Log("Stopped Music AudioScource");
            }
            else
            {
                Debug.Log("Stopped Camera AudioScource");
                Camera.main.GetComponent<AudioSource>().Stop();
            }

            anim.SetTrigger("isDead");
            comMan.Die();
            comMan.gameObject.GetComponent<WordManager>().Die();
            enemy.gameObject.SetActive(false);

        }

    }

    private void NextScene()
    {
        stats.regionLoad = 1;
        stats.levelSelect = 0;
        SceneManager.LoadScene(5);
    }
}
