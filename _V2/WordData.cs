using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Management.Instrumentation;
using System.Linq.Expressions;

class WordData
{
    private static List<int[,]> regionsSaves; //stacked score over star repeats by region.
    private static int[,] regionDataScore = new int[3, 25]
    { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    private static int[,] regionDataStars = new int[3, 25]
    { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    private static int numberOfRegions = 3;
    private static List<int> DifMods = new List<int> { 30, 70, 140 };
    public static string SaveManagerData()
    {
        string sendSave = "";
        for (int x = 0; x < regionsSaves.Count; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    sendSave += regionsSaves[x][y, z].ToString();
                    sendSave += ",";
                }
            }
            sendSave += "|";
        }
        return sendSave;
    }
    public static void MakeNewRegion()
    {
        regionsSaves.Add(regionDataScore);
        regionsSaves.Add(regionDataStars);
    }
    private static void MakeAllRegions()
    {
        int number = (Region.AllRegions.Count / 3);
        for (int x = 0; x < number; x++)
        {
            MakeNewRegion();
        }
    }
    public static void LoadManagerData(string input)
    {
        if (input == "")
        {
            MakeAllRegions();
        }
        else
        {
            MakeAllRegions();
            int counter = 0;
            while (input.IndexOf('|') > 0)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        regionsSaves[counter][x, y] = Convert.ToInt32(input.Substring(0, (input.IndexOf(',') - 1)));
                        input.Remove(0);
                    }
                }
                input.Remove(0);
                counter++;
            }
        }
    }
    public static string GetDataLevel(int region, int level, int Dificulty /*all data starts at 0 for these ints*/)
    {
        string levelData;
        int location = region * 3 + Dificulty;
        levelData = Region.AllRegions[location][level];
        return levelData;
    }
    public static string GetWords(int book, int word)
    {
        switch (book)
        {
            case 5:
                return (WordMaster.fiveLetter[word]);
            case 6:
                return (WordMaster.sixLetter[word]);
            case 7:
                return (WordMaster.sevenLetter[word]);
            default:
                return (WordMaster.eightLetter[word]);
        }
    }
    public static void DataToSave(int score, int starCount, int region, int level, int dificulty)
    {
        int tempReg = region * 2;
        regionsSaves[tempReg][dificulty, level] = score;
        tempReg += 1;
        regionsSaves[tempReg][dificulty, level] = starCount;
    }
}