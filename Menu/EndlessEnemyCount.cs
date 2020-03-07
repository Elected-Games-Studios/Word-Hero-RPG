using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessEnemyCount : MonoBehaviour
{
    public Text defeatCount;
    public Text recordGoldText;
    public GameObject newRecord;
    public GameObject dailyCanvas;
    public WordManager wordMan;
    public List<GameObject> rewardDisplays;
    public List<Sprite> icons;

    int recordGold;

    int rewardGold;
    int rewardTime;
    int rewardWind;
    int rewardHP;
    int rewardShield;
    int rewards;
    bool dailyReward;

    PlayerStats stats;
    private void Awake()
    {
        // dailyReward = true;
        stats = PlayerStats.Instance;
        defeatCount.text = "Enemies Defeated: " + wordMan.enemiesBeaten.ToString();
        if(wordMan.enemiesBeaten > stats.endlessRecord)
        {
            recordGold = (wordMan.enemiesBeaten - stats.endlessRecord) * 10;
            stats.endlessRecord = wordMan.enemiesBeaten;
            newRecord.SetActive(true);
            recordGoldText.text = "+" + recordGold.ToString();
        }

        if(stats.date != DateTime.Today.Day)
        {
            stats.date = DateTime.Today.Day;
            rewardGold += 50;
            dailyCanvas.SetActive(true);
            for(int i = 0; i < (wordMan.enemiesBeaten/10); i++)
            {
                int powerupReward = UnityEngine.Random.Range(0, 3);
                if (powerupReward == 0)
                {
                    rewardTime++;
                    rewardGold += 50;
                }
                if (powerupReward == 1)
                {
                    rewardWind++;
                    rewardGold += 50;
                }
                if (powerupReward == 2)
                {
                    rewardHP++;
                    rewardGold += 50;
                }
                if (powerupReward == 3)
                {
                    rewardShield++;
                    rewardGold += 50;
                }
            }

            if(rewardTime>0)
            {
                rewardDisplays[rewards].SetActive(true);
                rewardDisplays[rewards].transform.GetChild(0).GetComponent<Text>().text = "+" + rewardTime.ToString();
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Image>().sprite = icons[0];
                rewards++;
            }
            if (rewardWind > 0)
            {
                rewardDisplays[rewards].SetActive(true);
                rewardDisplays[rewards].transform.GetChild(0).GetComponent<Text>().text = "+" + rewardWind.ToString();
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Image>().sprite = icons[1];
                rewards++;
            }
            if (rewardHP > 0)
            {
                rewardDisplays[rewards].SetActive(true);
                rewardDisplays[rewards].transform.GetChild(0).GetComponent<Text>().text = "+" + rewardHP.ToString();
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Image>().sprite = icons[2];
                rewards++;
            }
            if (rewardShield > 0)
            {
                rewardDisplays[rewards].SetActive(true);
                rewardDisplays[rewards].transform.GetChild(0).GetComponent<Text>().text = "+" + rewardShield.ToString();
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Image>().sprite = icons[3];
                rewards++;
            }
            rewardDisplays[rewards].SetActive(true);
            rewardDisplays[rewards].transform.GetChild(0).GetComponent<Text>().text = "+" + rewardGold.ToString();
            rewardDisplays[rewards].transform.GetChild(1).GetComponent<Image>().sprite = icons[4];
        }

        stats.playerGold += recordGold;
        stats.playerGold += rewardGold;
        stats.timePU += rewardTime;
        stats.secondWindPU += rewardWind;
        stats.healthPU += rewardHP;
        stats.shieldPU += rewardShield;
        PlayServices.Instance.SaveData();
    }

    public void CloseDaily()
    {
        dailyCanvas.SetActive(false);
    }
}
