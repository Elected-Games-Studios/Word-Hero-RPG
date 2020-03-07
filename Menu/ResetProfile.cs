using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetProfile : MonoBehaviour
{
    PlayerStats stats;
    public GameObject nani;

    private void Awake()
    {
        stats = PlayerStats.Instance;
    }

    public void EatShit()
    {
        stats.playerGold = 0;
        stats.heroLevel = 0;
        stats.heroXP = 0;
        stats.playerDamage = 0;
        stats.pDmgLvl = 0;
        stats.playerHealth = 0;
        stats.pHpLvl = 0;
        stats.playerSpeed = 0;
        stats.pSpLvl = 0;
        stats.playerEvade = 0;
        stats.pEvLvl = 0;
        stats.playerLuck = 0;
        stats.pLkLvl = 0;

        stats.healthPU = 0;
        stats.secondWindPU = 0;
        stats.shieldPU = 0;
        stats.timePU = 0;

        stats.xpToLevel = 100;
        stats.skillPoints = 0;
        stats.xpIncrease = 150;

        stats.isClassFour = false;
        stats.isClassThree = false;
        stats.isClassTwo = false;
        stats.isClassOne = false;

        stats.activeHero = 0;

        stats.regionProgress = 0;
        stats.levelProgress = 0;
        stats.endlessRecord = 0;
        stats.date = 0;
        stats.dailyActive = false;
        for (int i = 0; i < stats.levelStars.Length; i++)
        {
            stats.levelStars[i] = 0;
        }
        Camera.main.gameObject.GetComponent<AudioSource>().Stop();
        nani.SetActive(true);
        
        PlayServices.Instance.SaveData();
        Invoke("ResetToStart", 5f);
    }

    void ResetToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void GetXP()
    {
        stats.heroXP += 10000;
        PlayServices.Instance.SaveData();
    }
}
