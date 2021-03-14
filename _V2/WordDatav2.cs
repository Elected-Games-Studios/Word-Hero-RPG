using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

public static class WordDatav2
{
    #region Data
    private static List<int[,]> regionsSaves = new List<int[,]>(); //stacked score over star repeats by region.
    private static List<int> NumEnemies = new List<int>
        {3,3,3,3,4,3,3,3,3,4,4,4,4,4,1,4,4,4,4,5,4,4,4,4,1};
    private static int[,] regionDataScore = new int[3, 25]
    { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    private static int[,] regionDataStars = new int[3, 25]
    { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
    private static int numberOfRegions = 3;
    private static List<int> DifMods = new List<int> { 30, 70, 140, 500 };
    #endregion
    public static int[,,] RegionUnlocks(int region)
    {
        int[,,] tempregion = new int[2,3,25] 
        { { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } } };
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 25; y++)
            {
                tempregion[0, x, y] = regionsSaves[(region * 2)][x, y];
                tempregion[1, x, y] = regionsSaves[((region * 2) + 1)][x, y]; 
            }
        }
        return tempregion;
    }
    public static void endOfLevelStats(int region, int dif, int level, int score, int stars)
    {
        if (score > regionsSaves[region * 2][dif, level])
        {
            regionsSaves[region * 2][dif, level] = score;
            regionsSaves[region * 2 + 1][dif, level] = stars;
        }
        
    }
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
            for (int x = 0; x < numberOfRegions; x++)
            {
                MakeNewRegion();
            }
        }
    public static void LoadManagerData(string input)
    {
        if (input == " ")
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
                        if (y < 24)
                        {
                            regionsSaves[counter][x, y] = Convert.ToInt32(input.Substring(0, (input.IndexOf(','))));
                            input.Remove(0, (input.IndexOf(',')));
                        }
                        else if(y==24 && x==2)
                        {
                            regionsSaves[counter][x, y] = Convert.ToInt32(input.Substring(0, (input.IndexOf('|'))));
                            input.Remove(0,(input.IndexOf('|')));
                        }
                        else
                        {
                            regionsSaves[counter][x, y] = Convert.ToInt32(input.Substring(0, (input.IndexOf(','))));
                            input.Remove(0, (input.IndexOf(',')));
                        }
                        
                    }
                }
                counter++;
            }
        }
    }
    public static List<int> GetDataLevel(int region, int level, int difficulty /*all data starts at 0 for these ints*/)
        {
            List<int> reValue = new List<int> { };
        Random rand = new Random();
        int dicNum = rand.Next(0, 1);//JS--moved this up here bc couldn't use before declared
        
        int checkVal, numRand, tempVal;
            dicNum = dicNum + 5 + difficulty;
            if (region==50)
            {
                if (level < 25)
                {
                    region = difficulty;
                    difficulty = 0;
                
                }
                else if (level < 50)
                {
                    region = difficulty;
                    difficulty = 1;
                    level = level - 25;
                }
                else if (level < 75)
                {
                    region = difficulty;
                    difficulty = 2;
                    level = level - 50;
                }
                else {
                    region = difficulty;
                    difficulty = 3;
                    level = 24;
                }
            }
            
            for (int x = 0; x < NumEnemies[level]; x++)
            {
                switch (dicNum)
                {
                    case 5:
                        checkVal = DifMods[difficulty] + region * 10 - Convert.ToInt32(rand.Next(0, 9));
                        while (FiveLetter.fiveList.Contains(checkVal) == false) { checkVal = DifMods[difficulty] + region * 10 - rand.Next(0, 9); }
                        reValue.Add(dicNum);
                        numRand = FiveLetter.fiveArray[FiveLetter.fiveList.FindIndex(t => t == checkVal), 0];
                        tempVal = Convert.ToInt32(rand.Next(0, numRand)) + Convert.ToInt32(FiveLetter.fiveArray[Convert.ToInt32(FiveLetter.fiveList.FindIndex(t => t == checkVal)), 1]);
                        reValue.Add(tempVal);
                    UnityEngine.Debug.Log(dicNum + " letter word generated");
                        break;
                    case 6:
                        checkVal = DifMods[difficulty] + region * 10 - Convert.ToInt32(rand.Next(0, 9));
                        while (SixLetter.sixList.Contains(checkVal) == false) { checkVal = DifMods[difficulty] + region * 10 - rand.Next(0, 9); }
                        reValue.Add(dicNum);
                        numRand = SixLetter.sixArray[SixLetter.sixList.FindIndex(t => t == checkVal), 0];
                        tempVal = Convert.ToInt32(rand.Next(0, numRand)) + Convert.ToInt32(SixLetter.sixArray[Convert.ToInt32(SixLetter.sixList.FindIndex(t => t == checkVal)), 1]);
                        reValue.Add(tempVal);
                    UnityEngine.Debug.Log(dicNum + " letter word generated");
                    break;
                    case 7:
                        checkVal = DifMods[difficulty] + region * 10 - Convert.ToInt32(rand.Next(0, 9));
                        while (SevenLetter.sevenList.Contains(checkVal) == false) { checkVal = DifMods[difficulty] + region * 10 - rand.Next(0, 9); }
                        reValue.Add(dicNum);
                        numRand = SevenLetter.sevenArray[SevenLetter.sevenList.FindIndex(t => t == checkVal), 0];
                        tempVal = Convert.ToInt32(rand.Next(0, numRand)) + Convert.ToInt32(SevenLetter.sevenArray[Convert.ToInt32(SevenLetter.sevenList.FindIndex(t => t == checkVal)), 1]);
                        reValue.Add(tempVal);
                    UnityEngine.Debug.Log(dicNum + " letter word generated");
                    break;
                    default:
                        checkVal = DifMods[difficulty] + region * 10 - Convert.ToInt32(rand.Next(0, 9));
                        while (EightLetter.eightList.Contains(checkVal) == false) { checkVal = DifMods[difficulty] + region * 10 - rand.Next(0, 9); }
                        reValue.Add(dicNum);
                        numRand = EightLetter.eightArray[EightLetter.eightList.FindIndex(t => t == checkVal), 0];
                        tempVal = Convert.ToInt32(rand.Next(0, numRand)) + Convert.ToInt32(EightLetter.eightArray[Convert.ToInt32(EightLetter.eightList.FindIndex(t => t == checkVal)), 1]);
                        reValue.Add(tempVal);
                    UnityEngine.Debug.Log(dicNum + " letter word generated");
                    break;
                }
            }
            return reValue;
        }
    //public static string GetWords(int book, int word)
    //    {
    //        string temp = "";
    //        switch (book)
    //        {
    //            case 5:
    //                temp = Convert.ToString(FiveLetter.fiveLetter[word]);
    //                break;
    //        case 6:
    //                temp = Convert.ToString(SixLetter.sixLetter[word]);
    //                break;
    //        case 7:
    //                temp = Convert.ToString(SevenLetter.sevenLetter[word]);
    //                break;
    //        default:
    //                temp = Convert.ToString(EightLetter.eightLetter[word]);
    //                break;
    //        }
    //        return temp;
    //    }
    public static void DataToSave(int score, int starCount, int region, int level, int dificulty)
        {
            int tempReg = region * 2;
            regionsSaves[tempReg][dificulty, level] = score;
            tempReg += 1;
            regionsSaves[tempReg][dificulty, level] = starCount;
        }
}
