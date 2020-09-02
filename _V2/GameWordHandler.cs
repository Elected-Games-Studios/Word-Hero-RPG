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
        WordsIndex = WordDatav2.GetDataLevel();
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
            Words = WordDatav2.GetWords(WordsIndex[0], WordsIndex[1+x*2]);
        }
    }
}
