using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionary : MonoBehaviour
{
    public int enemyHP;
    string line;
    int lineCount;
    string path;
    public List<TextAsset> dictionaries = new List<TextAsset>();
    TextAsset dics;
    public int totalEnemies;
    private PlayerStats stats;
    bool isEndless;
    WordManager wordMan;
    public Text enemyCount;
    bool isTutorial;

    private void Awake()
    {
        isTutorial = false;
        isEndless = false;
        wordMan = GetComponent<WordManager>();
        if (GetComponent<IsEndless>())
        {
            isEndless = true;
            enemyCount.text = "Enemies Defeated: " + enemyCount;
        }
        if(GetComponent<TutorialManager>())
        {
            isTutorial = true;
        }
        stats = PlayerStats.Instance;
        if (!isEndless && !isTutorial)
        {
            totalEnemies = 0;
            dics = dictionaries[stats.levelSelect - 1];
            line = dics.text.ToString();
            string lineCheck = line.Replace("|", "");
            totalEnemies = line.Length - lineCheck.Length;
            Debug.Log("There are " + totalEnemies + " enemies");
            enemyCount.text = "Enemies Left: " + totalEnemies;
        }
        if(isTutorial)
        {
            dics = dictionaries[0];
        }

    }

    public string[] GetWord(int length)
    {
        UpdateEnemyCount();
        //Debug.Log("Getting new Word");
        if (isEndless)
        {
            dics = dictionaries[length - 4];
        }

        line = dics.text;
        string lineCheck = line.Replace("|", "");
        lineCount = line.Length - lineCheck.Length;
        int cap = 0;
        line = dics.text.ToString();
        int randomNumber = UnityEngine.Random.Range(0, lineCount);

        for (int i = 0; i < randomNumber; i++)
        {
            cap = line.IndexOf("|", (cap + 1));
        }
        int stringLength = (line.IndexOf(("|"), cap + 1) - cap);

        line = (dics.text.Substring(cap + 1, stringLength - 1));
        line = line.Replace(" ", "");
        Debug.Log(Int32.Parse(line.Substring(0, line.IndexOf("/"))));
        enemyHP = Int32.Parse(line.Substring(0, line.IndexOf("/")));

        string[] words = line.Split(new string[] { "," }, StringSplitOptions.None);

        Debug.Log("=====================================================");
        Debug.Log("There are " + words.Length + " Words in this list");
        for (int i = 0; i < words.Length; i++)
        {
            Debug.Log(words[i]);
        }
        Debug.Log("=====================================================");

        return words;
    }

    public string[] GetWordsForWorlds(int level)
    {

        UpdateEnemyCount();
        //Debug.Log("Getting new Word");


        if(isTutorial)
        {
            dics = dictionaries[0];
        }
        else
        {
            dics = dictionaries[level - 1];
        }
        int cap = 0;
        line = dics.text.ToString();

        for (int i = 0; i < wordMan.enemiesBeaten; i++)
        {
            cap = line.IndexOf("|", (cap + 1));
        }
        int stringLength = (line.IndexOf(("|"), cap + 1) - cap);

        line = (dics.text.Substring(cap + 1, stringLength - 1));
        line = line.Replace(" ", "");

        Int32.TryParse(line.Substring(0, line.IndexOf("/")), out enemyHP);

        string[] words = line.Split(new string[] { "," }, StringSplitOptions.None);


        for (int i = 0; i < words.Length; i++)
        {
            //Debug.Log(words[i]);
        }


        return words;
    }

    public void UpdateEnemyCount()
    {
        if (!isEndless)
        {
            int display = totalEnemies - wordMan.enemiesBeaten;
            enemyCount.text = "Enemies Left: " + display;
        }
        else
        {
            enemyCount.text = "Enemies defeated: " + wordMan.enemiesBeaten;
        }

    }
}
