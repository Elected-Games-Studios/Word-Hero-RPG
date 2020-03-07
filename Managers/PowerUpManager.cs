using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public Slider timer;
    public PlayerCombat player;
    public WordManager wordManager;
    public static bool timePower;
    public static bool shacklePower;
    public static bool shieldPower;
    public GameObject playerCache;

    public GameObject healParticle;
    public GameObject timeStopBorder;
    public GameObject secondWind;
    public GameObject shieldEffect;

    public GameObject canvas;

    public Text timeCount;
    public Text healthCount;
    public Text sWindCount;
    public Text shieldCount;

    public AudioClip healAudio;
    public AudioClip slowAudio;
    public AudioClip secondWindAudio;
    public AudioClip shieldAudio;

    public AudioSource powerupSound;

    PlayerStats stats;

    private void Awake()
    {
        stats = PlayerStats.Instance;
    }

    private void Update()
    {
        if(stats.timePU >= 1)
        {
            timeCount.color = new Color32(255, 255, 255, 255);
            timeCount.text = stats.timePU.ToString();
        }
        else
        {
            timeCount.color = new Color32(255, 216, 0, 255);
            timeCount.text = ("100g");
        }
        if (stats.healthPU >= 1)
        {
            healthCount.color = new Color32(255, 255, 255, 255);
            healthCount.text = stats.healthPU.ToString();
        }
        else
        {
            healthCount.color = new Color32(255, 216, 0, 255);
            healthCount.text = ("100g");
        }
        if (stats.secondWindPU >= 1)
        {
            sWindCount.color = new Color32(255, 255, 255, 255);
            sWindCount.text = stats.secondWindPU.ToString();
        }
        else
        {
            sWindCount.color = new Color32(255, 216, 0, 255);
            sWindCount.text = ("200g");
        }
        if (stats.shieldPU >= 1)
        {
            shieldCount.color = new Color32(255, 255, 255, 255);
            shieldCount.text = stats.shieldPU.ToString();
        }
        else
        {
            shieldCount.color = new Color32(255, 216, 0, 255);
            shieldCount.text = ("10g");
        }
    }

    public void TimePowerUp()
    {
        if (stats.timePU >= 1)
        {
            CombatManager.Instance.timersUsed++;
            stats.timePU -= 1;
            timeStopBorder.SetActive(true);
            powerupSound.clip = slowAudio;
            powerupSound.Play();
            timePower = true;
        }
        else if(stats.playerGold >= 100)
        {
            stats.playerGold -= 100;
            CombatManager.Instance.timersUsed++;
            timeStopBorder.SetActive(true);
            powerupSound.clip = slowAudio;
            powerupSound.Play();
            timePower = true;
        }
        PlayServices.Instance.SaveData();
    }

    public void HealthPowerUp()
    {
        if (stats.healthPU >= 1)
        {
            player.playerHealth = 10 + stats.playerHealth;
            Instantiate(healParticle, playerCache.transform);
            powerupSound.clip = healAudio;
            powerupSound.Play();
            stats.healthPU -= 1;
        }
        else if (stats.playerGold >= 100)
        {
            stats.playerGold -= 100;
            Instantiate(healParticle, playerCache.transform);
            player.playerHealth = 10 + stats.playerHealth;
            powerupSound.clip = healAudio;
            powerupSound.Play();
        }
        PlayServices.Instance.SaveData();
    }

    public void SecondWindPowerUp()
    {
        Transform spawnPoint = GetComponent<WordManager>().foundWords.transform;
        if (stats.secondWindPU >= 1)
        {
            wordManager.ClearUsedWords();
            Instantiate(secondWind, spawnPoint);
            powerupSound.clip = secondWindAudio;
            powerupSound.Play();
            stats.secondWindPU -= 1;
        }
        else if (stats.playerGold >= 200)
        {
            stats.playerGold -= 200;
            Instantiate(secondWind, spawnPoint);
            wordManager.ClearUsedWords();
            powerupSound.clip = secondWindAudio;
            powerupSound.Play();
        }
        PlayServices.Instance.SaveData();
    }

    public void ShieldPowerUp()
    {
        if (stats.shieldPU >= 1)
        {
            stats.shieldPU -= 1;
            powerupSound.clip = shieldAudio;
            powerupSound.Play();
            Instantiate(shieldEffect, canvas.transform);
            shieldPower = true;
        }
        else if (stats.playerGold >= 10)
        {
            stats.playerGold -= 10;
            powerupSound.clip = shieldAudio;
            powerupSound.Play();
            Instantiate(shieldEffect, canvas.transform);
            shieldPower = true;
        }
    }
}
