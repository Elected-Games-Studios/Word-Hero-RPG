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
    private static List<string> Words;
    public static void StartLevel()
    {
        WordsIndex = WordData.GetDataLevel(Region, Level, Dificulty);
        GetActualWords();
    } 
    public static void SetLevelData(int region, int level, int dificulty)
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
            Words = WordData.GetWords(WordsIndex[0], WordsIndex[1+x*2]);
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

}
