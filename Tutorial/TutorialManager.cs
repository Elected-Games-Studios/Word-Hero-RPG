using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static bool isFighting;
    public bool gotHit;
    public bool dontFight;
    public GameObject hero;
    public GameObject senpai;
    public GameObject cloud;
    public GameObject wordBubble;
    public GameObject timer;
    public GameObject wordsFound;
    public GameObject hpMana;
    public GameObject directionsAtTheBottonOfTheScreen;
    public GameObject bubbleCover;
    public GameObject cinematicCombat;
    public GameObject ghostSenpai;
    public GameObject explosionParticle;
    public Animator mage;
    public Animator knight;
    public Animator paladin;
    public GameObject magePart;
    public GameObject knightPart;
    public GameObject paladinPart;
    public AudioSource tutSFX;
    public AudioClip explosion;
    public AudioClip combatMusicIntro;
    public AudioClip combatMusicLoop;
    public AudioClip bossMusicIntro;
    public AudioClip bossMusicLoop;
    public AudioClip cinematicLoop;
    public GameObject deathRay;
    public GameObject deathHit;
    public GameObject aura;
    public GameObject poof;
    public AudioSource click;
    public List<GameObject> dialogueBubbles;
    public List<GameObject> backgrounds;
    public EnemySpawn enemySpawn;
    WordManager wordMan;
    CombatManager comMan;
    int tutorialProgress;
    int backgroundNumber;
    private void Awake()
    {
        wordMan = GetComponent<WordManager>();
        comMan = GetComponent<CombatManager>();
        tutorialProgress = 0;
        Dialogue();
        ChangeBackground();
    }

    private void Update()
    {
        if(!isFighting)
        {
            directionsAtTheBottonOfTheScreen.SetActive(true);
        }
        else
        {
            directionsAtTheBottonOfTheScreen.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ProgressTutorial();
        }
    }
    public void ProgressTutorial()
    {
        if(!isFighting)
        {
            tutorialProgress++;
            Dialogue();
            if(tutorialProgress == 7)       //Explosion, Spawn Enemy
            {
                Instantiate(explosionParticle, enemySpawn.gameObject.transform);
                bubbleCover.SetActive(false);
                Camera.main.gameObject.GetComponent<AudioSource>().Stop();
                tutSFX.clip = explosion;
                tutSFX.Play();
                enemySpawn.SpawnEnemy();
                comMan.enemySpawning = true;
                dontFight = true;
                StartCoroutine("BossMusic");
            }
            if(tutorialProgress == 10)      //enable word bubble, move senpai and spawn a new word.
            {
                wordBubble.SetActive(true);

                senpai.GetComponent<Animator>().SetTrigger("inCombat");
                cloud.SetActive(true);
                wordMan.NewWord();
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCombat>().GetHealth(wordMan.wordList);
                dontFight = false;
                isFighting = true;
                comMan.enemySpawning = false;
                hero.GetComponent<PlayerCombat>().anim.SetBool("inCombat", true);
            }
            if(tutorialProgress == 11)
            {
                wordBubble.SetActive(false);
            }
            if(tutorialProgress == 12)
            {
                wordBubble.SetActive(true);
                wordMan.NewWord();
                enemySpawn.SpawnEnemy();
                wordsFound.SetActive(true);
                isFighting = true;
                timer.SetActive(true);
                comMan.timer.value = comMan.timer.maxValue;
                comMan.enemySpawning = false;
            }
            if(tutorialProgress == 13)
            {
                wordBubble.SetActive(false);
                timer.SetActive(true);
            }
            if (tutorialProgress == 16)
            {
                hpMana.SetActive(true);
                hero.GetComponent<PlayerCombat>().SetHpBar();
            }
            if(tutorialProgress == 17)
            {
                comMan.mana = 997;
            }
            if (tutorialProgress == 18)
            {
                wordBubble.SetActive(true);
                wordMan.NewWord();
                enemySpawn.SpawnEnemy();
                isFighting = true;
            }
            if(tutorialProgress == 19)
            {
                wordBubble.SetActive(false);
            }
            if(tutorialProgress == 20)
            {
                wordsFound.SetActive(false);
                hero.GetComponent<PlayerCombat>().anim.SetBool("inCombat", false);
                bubbleCover.SetActive(true);
                backgroundNumber = 1;
                ChangeBackground();
            }
            if(tutorialProgress == 22)
            {
                Camera.main.GetComponent<Animator>().SetTrigger("cinematic");
                isFighting = true;
                StartCoroutine("WaitForCinematic");
            }
            if(tutorialProgress == 24)
            {
                cloud.SetActive(false);
                backgroundNumber++;
                Destroy(cinematicCombat, .5f);
                ChangeBackground();
                senpai.GetComponent<Animator>().SetTrigger("getGrabbed");
                wordBubble.SetActive(true);
                wordMan.NewWord();
                enemySpawn.SpawnSpecific(0);
                comMan.enemySpawning = true;
            }
            if(tutorialProgress == 25)
            {
                wordsFound.SetActive(true);
                hero.GetComponent<PlayerCombat>().anim.SetBool("inCombat", true);

            }
            if(tutorialProgress == 26)
            {
                isFighting = true;
                comMan.enemySpawning = false;
                bubbleCover.SetActive(false);
            }
            if (tutorialProgress == 27)
            {
                bubbleCover.SetActive(true);
                StartCoroutine("BlackKnightScene1");
            }
            if (tutorialProgress == 28)
            {
                StartCoroutine("BlackKnightScene2");
            }
            if (tutorialProgress == 30)
            {
                StartCoroutine("BlackKnightScene3");
            }
            if (tutorialProgress == 31)
            {
                isFighting = true;
                StartCoroutine("BlackKnightScene4");
                wordMan.NewWord();
                bubbleCover.SetActive(false);
                GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>().isTutorial = false;
                isFighting = true;
            }
            click.Play();
        }
    }

    void Dialogue()
    {
        if(tutorialProgress == 14 && !gotHit)
        {
            tutorialProgress = 15;
        }
        if(tutorialProgress == 13 && gotHit)
        {
            tutorialProgress = 14;
        }
        for (int i = 0; i < dialogueBubbles.Count; i++)
        {
            dialogueBubbles[i].SetActive(false);
        }

        dialogueBubbles[tutorialProgress].SetActive(true);

        if(tutorialProgress == 14 && gotHit)
        {
            tutorialProgress = 15;
        }
    }

    void ChangeBackground()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].SetActive(false);
        }
        backgrounds[backgroundNumber].SetActive(true);
    }

    IEnumerator BossMusic()  //if it's the boss music there's an intro before the loop
    {
        yield return new WaitForSeconds(2f);
        AudioSource audio = Camera.main.gameObject.GetComponent<AudioSource>();
        audio.clip = combatMusicIntro;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = combatMusicLoop;
        audio.Play();
    }
    IEnumerator WaitForCinematic() 
    {
        tutSFX.clip = cinematicLoop;
        tutSFX.Play();
        yield return new WaitForSeconds(2f);
        mage.SetTrigger("attack");
        magePart.SetActive(true);
        yield return new WaitForSeconds(2.4f);
        knight.SetTrigger("attack");
        knightPart.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        paladin.SetTrigger("attack");
        paladinPart.SetActive(true);
        yield return new WaitForSeconds(2f);
        senpai.GetComponent<Animator>().SetTrigger("getGrabbed");
        yield return new WaitForSeconds(.3f);

        isFighting = false;
        Invoke("ProgressTutorial", 2);
    }

    IEnumerator BlackKnightScene1() //black knight comes on screen, says something
    {
        isFighting = true;
        enemySpawn.SpawnSpecific(1);
        Camera.main.GetComponent<Animator>().SetTrigger("sidePan");
        yield return new WaitForSeconds(2);
        isFighting = false;
        ProgressTutorial();
    }
    IEnumerator BlackKnightScene2() //fires laser, laser holds senpai until click
    {
        isFighting = true;
        GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>().SetBool("based", true);
        Instantiate(deathRay, GameObject.FindGameObjectWithTag("enemy").transform);
        Instantiate(deathHit, GameObject.FindGameObjectWithTag("CinemaStop").transform);
        yield return new WaitForSeconds(2);
        isFighting = false;
        ProgressTutorial();
    }
    IEnumerator BlackKnightScene3() //poof! Senpai is ghosty. click for comment
    {
        isFighting = true;
        GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>().SetBool("based", false);
        GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>().SetTrigger("boost");
        //senpai.GetComponent<Animator>().SetTrigger("isDead");
        Destroy(senpai);
        Instantiate(poof, GameObject.FindGameObjectWithTag("CinemaStop").transform);
        Instantiate(ghostSenpai, GameObject.FindGameObjectWithTag("CinemaStop").transform);
        yield return new WaitForSeconds(1);
        ProgressTutorial();
        isFighting = false;
    }
    IEnumerator BlackKnightScene4() //grow yeetclops, black knight dips.
    {
        isFighting = true;
        Camera.main.GetComponent<Animator>().SetTrigger("panBack");
        GameObject.FindGameObjectWithTag("enemy").GetComponent<Animator>().SetTrigger("boost");
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>().SetTrigger("getBig");
        Instantiate(aura, GameObject.FindGameObjectWithTag("enemy").transform);
        wordMan.NewWord();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCombat>().enemyHealth = 10000;
        hero.GetComponent<PlayerCombat>().anim.SetTrigger("wtf");
        yield return new WaitForSeconds(1);
        ProgressTutorial();

        //isFighting = false;
    }
}
