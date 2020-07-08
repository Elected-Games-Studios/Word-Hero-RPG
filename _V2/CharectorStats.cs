using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class CharectorStats
{
    private static List<List<int>> HeroList;
    private static List<int> heroDefault = new List<int> { 0, 0, 0, 0, 0 };
    public static int SkillPoints;
    public static string SaveManagerData()
    {
        string sendSave = "";
        sendSave += SkillPoints.ToString();
        sendSave += "|";
        for (int x = 0; x < HeroList.Count; x++)
        {
            for (int y = 0; y < HeroList[x].Count; y++)
            {
                sendSave += HeroList[x][y].ToString();
                sendSave += ",";
            }
            sendSave += "|";
        }
        return sendSave;
    }
    public static void LoadManagerData(string input)
    {
        if (input == "")
        {
            NewGame();
            SkillPoints = 0;
        }
        else
        {
            SkillPoints = Convert.ToInt32(input.Substring(0, (input.IndexOf(',') - 1)));
            input.Remove(0, 1);

            int counter = 0;
            int counting = 0;
            while (input.IndexOf('|') > 0)
            {
                HeroList.Add(heroDefault);
                while (input.IndexOf(',') < input.IndexOf('|'))
                {
                    HeroList[counter][counting] = Convert.ToInt32(input.Substring(0, (input.IndexOf(',') - 1)));
                    input.Remove(0);
                    counting++;
                }
                counting = 0;
                input.Remove(0);
                counter++;
            }
        }
    }
    public static void NewGame()
    {
        List<int> tempList = heroDefault;
        tempList.Insert(0, 0);
        HeroList.Add(tempList);
    }
    public static List<int> UnlockedCharectors()
    {
        List<int> Allhero = new List<int> { };
        for (int i = 0; i < HeroList.Count; i++)
        {
            Allhero.Add(HeroList[i][0]);
        }
        return Allhero;
    }
    public static List<int> CharStats(int charNum) { return (HeroList[charNum]); }
    public static void AutoSkill(int CharUsed)
    {
        int temp = HeroList[CharUsed][1];
        if (HeroList[CharUsed][1] > HeroList[CharUsed][2]) HeroList[CharUsed][2]++;
        else if (HeroList[CharUsed][1] > HeroList[CharUsed][3]) HeroList[CharUsed][3]++;
        else if (HeroList[CharUsed][1] > HeroList[CharUsed][4]) HeroList[CharUsed][4]++;
        else if (HeroList[CharUsed][1] > HeroList[CharUsed][5]) HeroList[CharUsed][5]++;
        else HeroList[CharUsed][1]++;

    }
    public static void ManualSkill(int skill, int CharUsed) => HeroList[CharUsed][skill]++;
    public static void LevelUP() => SkillPoints++;
}
