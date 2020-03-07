using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRewards : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Awake()
    {
        stats = PlayerStats.Instance;
        //Reward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reward(int stars)
    {
        int starLevel = (((stats.regionLoad - 1) * 25) - 1) + (stats.levelSelect);
        int rewardCount = stars-stats.levelStars[starLevel];
        Debug.Log("Current Stars = " + stats.levelStars[starLevel].ToString());
        Debug.Log("Reward Count = " + rewardCount.ToString());
        if (rewardCount > 0)
        {
            for (int i = 0; i < rewardCount; i++)
            {
                int powerupReward = Random.Range(0, 4);
                if (powerupReward == 0)
                {
                    rewardTime += 2*stats.regionLoad;
                    rewardGold += 50 + (50 * stats.regionLoad);
                }
                if (powerupReward == 1)
                {
                    rewardWind += 1 * stats.regionLoad;
                    rewardGold += 50 + (50 * stats.regionLoad);
                }
                if (powerupReward == 2)
                {
                    rewardHP+= 2 * stats.regionLoad;
                    rewardGold += 50 + (50 * stats.regionLoad);
                }
                if (powerupReward == 3)
                {
                    rewardShield += 5 * stats.regionLoad;
                    rewardGold += 50 + (50 * stats.regionLoad);
                }
            }

            if (rewardTime > 0)
            {
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardTime.ToString();
                rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[0];
                rewards++;
            }
            if (rewardWind > 0)
            {
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardWind.ToString();
                rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[1];
                rewards++;
            }
            if (rewardHP > 0)
            {
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardHP.ToString();
                rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[2];
                rewards++;
            }
            if (rewardShield > 0)
            {
                rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardShield.ToString();
                rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[3];
                rewards++;
            }
            rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardGold.ToString();
            rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[4];
            rewards++;
        }
        else
        {
            rewardGold = Random.Range(0, 11);
            rewardDisplays[rewards].transform.GetChild(1).GetComponent<Text>().text = "+" + rewardGold.ToString();
            rewardDisplays[rewards].transform.GetChild(3).GetComponent<Image>().sprite = icons[4];
            rewards++;
        }
        StartCoroutine("GiveItems");


        stats.playerGold += recordGold;
        stats.playerGold += rewardGold;
        stats.timePU += rewardTime;
        stats.secondWindPU += rewardWind;
        stats.healthPU += rewardHP;
        stats.shieldPU += rewardShield;
        PlayServices.Instance.SaveData();
    }
    IEnumerator GiveItems()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < rewards; i++)
        {
            rewardDisplays[i].SetActive(true);
            string trigger = ((rewards).ToString() + "-" + (i + 1).ToString());
            rewardDisplays[i].GetComponent<Animator>().SetTrigger(trigger);
        }
    }
}
