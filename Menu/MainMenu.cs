using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Text levelText;
    public GameObject skillPointText;
    public GameObject dailyText;
    public Slider xpBar;
    public Text xpText;
    public Text gold;
    public Text gold2;
    public List<Animator> windows = new List<Animator>();
    private Animator closeWindow;
    public AudioMixer music;
    public AudioMixer sfx;
    bool musicMuted;
    bool sfxMuted;
    PlayerStats stats;

    public GameObject endlessButtonLock;


    public void Awake()
    {
        
        stats = PlayerStats.Instance;
        if(stats.date != System.DateTime.Today.Day)
        {
            stats.dailyActive = true;
        }
        else
        {
            stats.dailyActive = false;
        }
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Music"));
        }
    }


    private void Update()
    {
        //if you have skill points to spend, show a shiney bubble
        if(stats.skillPoints >= 1)
        {
            skillPointText.SetActive(true);
            skillPointText.GetComponentInChildren<Text>().text = stats.skillPoints.ToString();
        }
        else
        {
            skillPointText.SetActive(false);
        }

        if (stats.dailyActive && stats.regionProgress > 1)
            dailyText.SetActive(true);
        else
            dailyText.SetActive(false);

        //set the XP bar to follow the value of the current XP
        xpBar.value = stats.heroXP;
        xpBar.maxValue = stats.xpToLevel;
        xpText.text = (stats.heroXP.ToString() + "/" + stats.xpToLevel.ToString());
        levelText.text = ("Level: " + stats.heroLevel);
        gold.text = stats.playerGold.ToString() + "g";
        gold2.text = stats.playerGold.ToString() + "g";

        if (stats.regionProgress >= 2)
        {
            endlessButtonLock.SetActive(false);
        }
        else
        {
            endlessButtonLock.SetActive(true);
        }
    }

    #region WindowOpenButtons

    public void ButtonEndlessDungeon()
    {
        stats.regionLoad = 2;
        SceneManager.LoadScene(3);
    }

    public void OpenTimeOffer()
    {
        windows[0].SetBool("open", true);
        closeWindow = windows[0];
    }

    public void OpenHealthOffer()
    {
        windows[1].SetBool("open", true);
        closeWindow = windows[1];
    }

    public void OpenShackleOffer()
    {
        windows[2].SetBool("open", true);
        closeWindow = windows[2];
    }

    public void OpenShieldOffer()
    {
        windows[3].SetBool("open", true);
        closeWindow = windows[3];
    }

    public void OpenShop()
    {
        windows[4].SetBool("open", true);
        closeWindow = windows[4];
    }

    public void OpenSettings()
    {
        windows[5].SetBool("open", true);
        closeWindow = windows[5];
    }

    public void OpenPlayerStats()
    {
       
        windows[6].SetBool("open", true);
        closeWindow = windows[6];
    }

    public void CloseWindow()
    {
        if(closeWindow.GetComponent<WorldMapSelect>())
        {
            for (int i = 0; i < closeWindow.GetComponent<WorldMapSelect>().glowLevel.Count; i++)
            {
                closeWindow.GetComponent<WorldMapSelect>().glowLevel[i].SetActive(false);
            }
        }
        closeWindow.SetBool("open", false);
        closeWindow = null;
    }
    #endregion

    #region Settings

    public void MuteMusic()
    {
        if(musicMuted)
        {
            music.SetFloat("musicVol", 0);
            musicMuted = false;
        }
        else
        {
            music.SetFloat("musicVol", -80);
            musicMuted = true;
        }
    }

    public void MuteSFX()
    {
        if(sfxMuted)
        {
            sfx.SetFloat("sfxVol", 0);
            sfxMuted = false;
        }
        {
            sfx.SetFloat("sfxVol", -80);
            sfxMuted = true;
        }
    }
    #endregion
}