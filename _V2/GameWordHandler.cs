using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class GameWordHandler
{
    private static int Region, Level, Dificulty;
    private static List<int> WordsIndex;
    public static List<string> Words;
    public static List<string> wordsUsed;

    public static void StartLevel() //called on awake for 
    {
        WordsIndex = WordData.GetDataLevel(Region, Level, Dificulty);
        GetActualWords();
    }

    public static void
        SetLevelData(int region, int level,
            int dificulty) // called at beginning of level, call from Mono around the scene
    {
        Region = region;
        Level = level;
        Dificulty = dificulty;
    }

    private static void GetActualWords()
    {
        int tempcount = WordsIndex.Count();
        tempcount = tempcount / 2;
        for (int x = 0; x < tempcount; x++)
        {
            Words.Add(Convert.ToString(WordData.GetWords(WordsIndex[0], WordsIndex[1 + x * 2])));
        }
    }

    public static int GetPlayerAttack()
    {
        return CharectorStats.CurrentHero[0];
    }

    public static int GetPlayerDefence()
    {
        return CharectorStats.CurrentHero[1];
    }

    public static int GetPlayerSpeed()
    {
        return CharectorStats.CurrentHero[2];
    }

    public static int GetPlayerLuck()
    {
        return CharectorStats.CurrentHero[3];
    }

    public static int GetPlayerCrit()
    {
        return CharectorStats.CurrentHero[4];
    }

    public static void Shuffle()
    {

        List<string> tempList = wordsUsed;
        string tempWord = tempList[(tempList.Count - 1)];
        tempList = new List<string>();
        for (int x = 0; x < tempWord.Count(); x++) tempList.Add(tempWord[x].ToString());
        int counter = wordsUsed.Count;
        for (int x = 0; x < counter; x++)
        {
            int remover = UnityEngine.Random.Range(0, tempList.Count);
            string tempLetter = tempList[remover];
            tempList.RemoveAt(remover);
            //your code here
        }
    }

    public static void WordBreak(int currentword)
    {
        wordsUsed = new List<string> { };
        string[] tempArrString = Words[currentword].Split(',');
        wordsUsed = tempArrString.ToList();
        wordsUsed.RemoveAt(0);
    }
}

