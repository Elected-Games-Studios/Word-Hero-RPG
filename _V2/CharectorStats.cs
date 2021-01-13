using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


public static class CharectorStats
{
    //needed to temporarily make HeroList public to actually access it for stats bc LoadManagerData doesnt have a return value.
    private static List<int[]> HeroList = new List<int[]> { };//Mine!
    private static int[] HeroDefault = new int[10]
    { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }; // 0-Idx 1-Lvl 2-XP 3-Stars 4-dmg 5-health 6-crit 7-agi 8-def 9-XPtoNextLevel     ###MINE!!!!!
    private static int[] DifArr = new int[5]
        {15,12,10,8,6};//Mine!
    private static int[,] HeroDif = new int[49, 5]
    {{2,2,2,2,2},{2,1,3,2,2},{1,2,2,2,3},{2,3,2,1,2},{1,3,2,2,3},{3,1,2,2,2},{2,2,3,2,1},{2,3,2,1,2},{2,1,3,1,3},{2,1,3,3,1},{1,3,1,2,3},{3,1,1,3,2},{2,3,1,1,3},{3,2,1,3,1},{1,3,2,1,3},{2,3,1,1,3},{2,1,3,3,1},{3,1,3,1,2},{3,1,2,1,3},{0,3,2,1,4},{0,2,4,3,1},{2,1,4,3,0},{1,2,4,3,0},{2,1,3,1,2},{0,1,2,3,4},{2,1,3,2,1},{1,1,2,3,2},{1,2,2,1,3},{1,3,2,0,4},{2,2,1,1,3},{3,0,2,1,4},{0,4,1,2,3},{1,3,0,2,4},{1,3,2,0,4},{2,2,1,1,3},{1,2,2,3,1},{2,3,1,1,2},{3,2,1,1,2},{1,2,3,1,2},{1,2,2,3,1},{0,3,4,2,1},{2,1,4,3,0},{1,2,2,1,3},{0,4,1,2,3},{2,1,2,3,1},{1,3,1,0,4},{1,0,2,3,4},{2,3,4,0,1},{0,3,1,2,4}};//Mine!
    private static List<int> XPT0 = new List<int>
    { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500 };//Mine!
    private static List<int> XPT1 = new List<int>
    { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500, 5500, 6600, 7800, 9100, 10500, 12000, 13600, 15300, 17100, 19000, 21000, 23100, 25300, 27600, 30000, 32500, 35100, 37800, 40600, 43500, 46500, 49600, 52800, 56100, 59500, 63000, 66600, 70300, 74100, 78000, 82000, 86100, 90300, 94600, 99000, 103500, 108100, 112800, 117600, 122500 };//Mine!
    private static List<int> XPT2 = new List<int>
    {200,550,1050,1700,2500,3450,4550,5800,7200,8750,10450,12300,14300,16450,18750,21200,23800,26550,29450,32500,35700,39050,42550,46200,50000,53950,58050,62300,66700,71250,75950,80800,85800,90950,96250,101700,107300,113050,118950,125000,131200,137550,144050,150700,157500,164450,171550,178800,186200,193750,201450,209300,217300,225450,233750,242200,250800,259550,268450,277500,286700,296050,305550,315200,325000,334950,345050,355300,365700,376250,386950,397800,408800,419950};//Mine!
    private static List<int> XPT3 = new List<int>
    {500,1200,2100,3200,4500,6000,7700,9600,11700,14000,16500,19200,22100,25200,28500,32000,35700,39600,43700,48000,52500,57200,62100,67200,72500,78000,83700,89600,95700,102000,108500,115200,122100,129200,136500,144000,151700,159600,167700,176000,184500,193200,202100,211200,220500,230000,239700,249600,259700,270000,280500,291200,302100,313200,324500,336000,347700,359600,371700,384000,396500,409200,422100,435200,448500,462000,475700,489600,503700,518000,532500,547200,562100,577200,592500,608000,623700,639600,655700,672000,688500,705200,722100,739200,756500,774000,791700,809600,827700,846000,864500,883200,902100,921200,940500,960000,979700,999600,1019700};//Mine!
    private static List<int> XPT4 = new List<int>
    {750,1750,3000,4500,6250,8250,10500,13000,15750,18750,22000,25500,29250,33250,37500,42000,46750,51750,57000,62500,68250,74250,80500,87000,93750,100750,108000,115500,123250,131250,139500,148000,156750,165750,175000,184500,194250,204250,214500,225000,235750,246750,258000,269500,281250,293250,305500,318000,330750,343750,357000,370500,384250,398250,412500,427000,441750,456750,472000,487500,503250,519250,535500,552000,568750,585750,603000,620500,638250,656250,674500,693000,711750,730750,750000,769500,789250,809250,829500,850000,870750,891750,913000,934500,956250,978250,1000500,1023000,1045750,1068750,1092000,1115500,1139250,1163250,1187500,1212000,1236750,1261750,1287000,1312500,1338250,1364250,1390500,1417000,1443750,1470750,1498000,1525500,1553250,1581250,1609500,1638000,1666750,1695750,1725000,1754500,1784250,1814250,1844500,1875000,1905750,1936750,1968000,1999500,2031250,2063250,2095500,2128000,2160750,2193750,2227000,2260500,2294250,2328250,2362500,2397000,2431750,2466750,2502000,2537500,2573250,2609250,2645500,2682000,2718750,2755750,2793000,2830500,2868250};//Mine!
    private static List<int> XPT5 = new List<int>
    {1000,2500,4500,7000,10000,13500,17500,22000,27000,32500,38500,45000,52000,59500,67500,76000,85000,94500,104500,115000,126000,137500,149500,162000,175000,188500,202500,217000,232000,247500,263500,280000,297000,314500,332500,351000,370000,389500,409500,430000,451000,472500,494500,517000,540000,563500,587500,612000,637000,662500,688500,715000,742000,769500,797500,826000,855000,884500,914500,945000,976000,1007500,1039500,1072000,1105000,1138500,1172500,1207000,1242000,1277500,1313500,1350000,1387000,1424500,1462500,1501000,1540000,1579500,1619500,1660000,1701000,1742500,1784500,1827000,1870000,1913500,1957500,2002000,2047000,2092500,2138500,2185000,2232000,2279500,2327500,2376000,2425000,2474500,2524500,2575000,2626000,2677500,2729500,2782000,2835000,2888500,2942500,2997000,3052000,3107500,3163500,3220000,3277000,3334500,3392500,3451000,3510000,3569500,3629500,3690000,3751000,3812500,3874500,3937000,4000000,4063500,4127500,4192000,4257000,4322500,4388500,4455000,4522000,4589500,4657500,4726000,4795000,4864500,4934500,5005000,5076000,5147500,5219500,5292000,5365000,5438500,5512500,5587000,5662000,5737500,5813500,5890000,5967000,6044500,6122500,6201000,6280000,6359500,6439500,6520000,6601000,6682500,6764500,6847000,6930000,7013500,7097500,7182000,7267000,7352500,7438500,7525000,7612000,7699500,7787500,7876000,7965000,8054500,8144500,8235000,8326000,8417500,8509500,8602000,8695000,8788500,8882500,8977000,9072000,9167500,9263500,9360000,9457000,9554500,9652500,9751000,9850000,9949500,10049500,10150000,10251000,10352500,10454500,10557000,10660000,10763500,10867500,10972000,11077000,11182500,11288500,11395000,11502000,11609500,11717500,11826000,11935000,12044500,12154500,12265000,12376000,12487500,12599500,12712000,12825000,12938500,13052500,13167000,13282000,13397500,13513500,13630000,13747000,13864500,13982500,14101000,14220000,14339500,14459500,14580000,14701000,14822500,14944500,15067000,15190000,15313500,15437500,15562000,15687000};//Mine!
    private static List<int> T1Cost = new List<int>
    { 100, 250 };//Mine!
    private static List<int> T2Cost = new List<int>
    { 250, 500, 1000 };//Mine!
    private static List<int> T3Cost = new List<int>
    { 1000, 2500, 5000 };//Mine!
    private static List<int> T4Cost = new List<int>
    { 5000, 10000, 15000 };//Mine!
    private static List<int> T5Cost = new List<int>
    { 15000, 30000, 75000 };//Mine!
    private static int[,] T0Stats = new int[2, 6]
        { {1,10,100,100,100,0}, {10,20,150,110,200,10}};//Mine!
    private static int[,] T1Stats = new int[2, 6]
        { {1,15,125,100,150,5}, {50,115,600,500,400,75} };//Mine!
    private static int[,] T2Stats = new int[2, 6]
        { {1,25,365,300,200,10}, {75,250,1475,1000,1000,200} };//Mine!
    private static int[,] T3Stats = new int[2, 6]
        { {1,35,1100,500,350,25}, {100,431,3080,1500,2500,425} };//Mine!
    private static int[,] T4Stats = new int[2, 6]
        { {1,45,2580,750,500,50}, {150,790,6305,2000,5715,1100} };//Mine!
    private static int[,] T5Stats = new int[2, 6]
        { {1,100,5055,1000,750,100}, {250,2600,12525,3500,12000,3000} };//Mine!
    private static List<int> tierstartStar = new List<int>
    { 0, 0, 0, 1, 2, 3 };//Mine!
    private static List<int> TeirNumberStars = new List<int>
    { 0, 2, 3, 4, 5, 6 };//Mine!
    private static List<int> T0MaxLevel = new List<int>
    { 10 };//Mine!
    private static List<int> T1MaxLevel = new List<int>
    { 10, 25, 50 };//Mine!
    private static List<int> T2MaxLevel = new List<int>
    { 10, 25, 50, 75 };//Mine!
    private static List<int> T3MaxLevel = new List<int>
    { 25, 50, 75, 100 };//Mine!
    private static List<int> T4MaxLevel = new List<int>
    { 50, 75, 100, 150 };//Mine!
    private static List<int> T5MaxLevel = new List<int>
    { 75, 100, 150, 250 };//Mine!
    private static string[,] NamesAndtiers = new string[72, 2]
    { {"Folk","T0"},{"Fighter","T1"},{"Ranger","T1"},{"Rogue","T1"},{"Mage","T1"},{"Cleric","T1"},{"Barbarian","T1"},{"Monk","T1"},{"Hunter","T2"},{"Soldier","T2"},{"Wizard","T2"},{"Acolyte","T2"},{"Thief","T2"},{"Gambler","T2"},{"Archer","T2"},{"Duelist","T2"},{"Berserker","T2"},{"Bard","T2"},{"Trapper","T2"},{"Blade Master","T3"},{"War Mage","T3"},{"Knight","T3"},{"Lancer","T3"},{"Druid","T3"},{"Sorcerer","T3"},{"Priest","T3"},{"Warlock","T3"},{"Arcane Trickster","T3"},{"Assassin","T3"},{"Tactician","T3"},{"Sage (Bard+)","T3"},{"Spell Sniper","T3"},{"Grand Marksman","T3"},{"Dervish","T3"},{"Hightened Monk","T3"},{"Commander","T3"},{"Magician","T3"},{"Jester","T3"},{"Falconer","T3"},{"Tinkerer","T3"},{"Hex Blade","T4"},{"Paladin","T4"},{"Weapon Master","T4"},{"Spellblade","T4"},{"Death Knight","T4"},{"Shadow","T4"},{"Juggernaut","T4"},{"Dragoon","T4"},{"Archmage","T4"},{"Eldrich Knight","T4"},{"Elementalist","T4"},{"Archdruid","T4"},{"Invoker","T4"},{"Shadowmancer","T4"},{"Avatar","T4"},{"Avenger","T4"},{"Blue Mage","T4"},{"Technomancer","T4"},{"Beast Master","T4"},{"Vampire Hunter","T4"},{"Arcane Arrow","T4"},{"Slayer/Executioner","T4"},{"Horizon Walker","T4"},{"Holy Ranger","T4"},{"Lycanthrope","T4"},{"Pirate Lord","T4"},{"Bombardier","T4"},{"Spirit Guardian","T4"},{"Aspect of Gaia","T5"},{"Dragonmaster","T5"},{"Avatar of Vengeance","T5"},{"Chronomancer","T5"} };//Mine!
    private static int[] CurrentHero = new int[2] {0,0};//Mine! Joe edited to default at 0
    public static int[] ShardCounter = new int[2] 
    { 0, 0 };
    private static List<int> T1bh = new List<int> 
    { 1, 2, 3, 4, 5, 6, 7 };
    private static List<int> T2bh = new List<int> 
    { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
    private static List<int> T3bh = new List<int> 
    { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
    private static List<int> T4bh = new List<int> 
    { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67 };
    private static List<int> T5bh = new List<int> 
    { 68, 69, 70, 71 };
    private static List<List<int>> tiersByHero = new List<List<int>> {T1bh,T2bh,T3bh,T4bh,T5bh};

    //-------------------------End of Variables -----------------------------

    private static void NewGame()//Mine!
    {
        HeroList.Add(HeroDefault);
    }
    private static int canAddXP(int chosenCharecter)//Mine!
    {
            string tier = NamesAndtiers[(HeroList[chosenCharecter][0]),1];
            int tierNum;
            switch (tier)
            {
                case "T0":
                    tierNum = 0;
                    break;
                case "T1":
                    tierNum = 1;
                    break;
                case "T2":
                    tierNum = 2;
                    break;
                case "T3":
                    tierNum = 3;
                    break;
                case "T4":
                    tierNum = 4;
                    break;
                default:
                    tierNum = 5;
                    break;
        }
            int difStar = tierstartStar[tierNum] - HeroList[chosenCharecter][3];
            switch (tierNum)
            {
                case 0:
                    return (XPT0[(T0MaxLevel[difStar] - 1)]);
                case 1:
                    return (XPT1[(T1MaxLevel[difStar] - 1)]);
                case 2:
                    return (XPT2[(T2MaxLevel[difStar] - 1)]);
                case 3:
                    return (XPT3[(T3MaxLevel[difStar] - 1)]);
                case 4:
                    return (XPT4[(T4MaxLevel[difStar] - 1)]);
                default:
                    return (XPT5[(T5MaxLevel[difStar] - 1)]);
            }

    }
    private static int findCurrentMax(int chosenCharecter)//Mine!
        {
            string tier = NamesAndtiers[HeroList[chosenCharecter][0],1];
            int tierNum;
            switch (tier)
            {
                case "T0":
                    tierNum = 0;
                break;
                case "T1":
                    tierNum = 1;
                    break;
                case "T2":
                    tierNum = 2;
                    break;
                case "T3":
                    tierNum = 3;
                break;
                case "T4":
                    tierNum = 4;
                    break;
                default:
                    tierNum = 5;
                    break;
            }
            int difStar = tierstartStar[tierNum] - HeroList[chosenCharecter][3];
            switch (tierNum)
            {
                case 0:
                    return (T0MaxLevel[difStar]);
                case 1:
                    return (T1MaxLevel[difStar]);
                case 2:
                    return (T2MaxLevel[difStar]);
                case 3:
                    return (T3MaxLevel[difStar]);
                case 4:
                    return (T4MaxLevel[difStar]);
                default:
                    return (T5MaxLevel[difStar]);
            }
        }
    public static int[] GetCharecterStats(int chosenCharecter)//Mine! JOE: Had to set to public, couldn't find origin point.
    {
        int[] tempStats = new int[10] 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int heroNumber = HeroList[chosenCharecter][0];
        int heroLvl = HeroList[chosenCharecter][1];
        string tier = NamesAndtiers[heroNumber,1];

        tempStats[0] = heroNumber;
        tempStats[1] = heroLvl;
        tempStats[2] = HeroList[chosenCharecter][2];
        tempStats[2] = HeroList[chosenCharecter][3];

        switch (tier)
            //this is off by -1 Level -- Dylan and Joe <3
        {
            case "T0":
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T0Stats[1,1] - T0Stats[0,1]) / ((T0Stats[1,0] - T0Stats[0,0]))) + T0Stats[0,1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T0Stats[1,2] - T0Stats[0,2]) / ((T0Stats[1,0] - T0Stats[0,0]))) + T0Stats[0,2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T0Stats[1,3] - T0Stats[0,3]) / ((T0Stats[1,0] - T0Stats[0,0]))) + T0Stats[0,3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T0Stats[1,4] - T0Stats[0,4]) / ((T0Stats[1,0] - T0Stats[0,0]))) + T0Stats[0,4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T0Stats[1,5] - T0Stats[0,5]) / ((T0Stats[1,0] - T0Stats[0,0]))) + T0Stats[0,5]));
                break;
            case "T1":
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T1Stats[1, 1] - T1Stats[0, 1]) / ((T1Stats[1, 0] - T1Stats[0, 0]))) + T1Stats[0, 1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T1Stats[1, 2] - T1Stats[0, 2]) / ((T1Stats[1, 0] - T1Stats[0, 0]))) + T1Stats[0, 2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T1Stats[1, 3] - T1Stats[0, 3]) / ((T1Stats[1, 0] - T1Stats[0, 0]))) + T1Stats[0, 3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T1Stats[1, 4] - T1Stats[0, 4]) / ((T1Stats[1, 0] - T1Stats[0, 0]))) + T1Stats[0, 4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T1Stats[1, 5] - T1Stats[0, 5]) / ((T1Stats[1, 0] - T1Stats[0, 0]))) + T1Stats[0, 5]));
                break;
            case "T2":
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T2Stats[1, 1] - T2Stats[0, 1]) / ((T2Stats[1, 0] - T2Stats[0, 0]))) + T2Stats[0, 1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T2Stats[1, 2] - T2Stats[0, 2]) / ((T2Stats[1, 0] - T2Stats[0, 0]))) + T2Stats[0, 2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T2Stats[1, 3] - T2Stats[0, 3]) / ((T2Stats[1, 0] - T2Stats[0, 0]))) + T2Stats[0, 3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T2Stats[1, 4] - T2Stats[0, 4]) / ((T2Stats[1, 0] - T2Stats[0, 0]))) + T2Stats[0, 4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T2Stats[1, 5] - T2Stats[0, 5]) / ((T2Stats[1, 0] - T2Stats[0, 0]))) + T2Stats[0, 5]));
                break;
            case "T3":
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T3Stats[1, 1] - T3Stats[0, 1]) / ((T3Stats[1, 0] - T3Stats[0, 0]))) + T3Stats[0, 1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T3Stats[1, 2] - T3Stats[0, 2]) / ((T3Stats[1, 0] - T3Stats[0, 0]))) + T3Stats[0, 2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T3Stats[1, 3] - T3Stats[0, 3]) / ((T3Stats[1, 0] - T3Stats[0, 0]))) + T3Stats[0, 3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T3Stats[1, 4] - T3Stats[0, 4]) / ((T3Stats[1, 0] - T3Stats[0, 0]))) + T3Stats[0, 4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T3Stats[1, 5] - T3Stats[0, 5]) / ((T3Stats[1, 0] - T3Stats[0, 0]))) + T3Stats[0, 5]));
                break;
            case "T4":
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T4Stats[1, 1] - T4Stats[0, 1]) / ((T4Stats[1, 0] - T4Stats[0, 0]))) + T4Stats[0, 1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T4Stats[1, 2] - T4Stats[0, 2]) / ((T4Stats[1, 0] - T4Stats[0, 0]))) + T4Stats[0, 2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T4Stats[1, 3] - T4Stats[0, 3]) / ((T4Stats[1, 0] - T4Stats[0, 0]))) + T4Stats[0, 3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T4Stats[1, 4] - T4Stats[0, 4]) / ((T4Stats[1, 0] - T4Stats[0, 0]))) + T4Stats[0, 4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T4Stats[1, 5] - T4Stats[0, 5]) / ((T4Stats[1, 0] - T4Stats[0, 0]))) + T4Stats[0, 5]));
                break;
            default:
                tempStats[4] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 0]] / 10) * (heroLvl * ((T5Stats[1, 1] - T5Stats[0, 1]) / ((T5Stats[1, 0] - T5Stats[0, 0]))) + T5Stats[0, 1]));
                tempStats[5] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 1]] / 10) * (heroLvl * ((T5Stats[1, 2] - T5Stats[0, 2]) / ((T5Stats[1, 0] - T5Stats[0, 0]))) + T5Stats[0, 2]));
                tempStats[6] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 2]] / 10) * (heroLvl * ((T5Stats[1, 3] - T5Stats[0, 3]) / ((T5Stats[1, 0] - T5Stats[0, 0]))) + T5Stats[0, 3]));
                tempStats[7] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 3]] / 10) * (heroLvl * ((T5Stats[1, 4] - T5Stats[0, 4]) / ((T5Stats[1, 0] - T5Stats[0, 0]))) + T5Stats[0, 4]));
                tempStats[8] = Convert.ToInt32((DifArr[HeroDif[heroNumber, 4]] / 10) * (heroLvl * ((T5Stats[1, 5] - T5Stats[0, 5]) / ((T5Stats[1, 0] - T5Stats[0, 0]))) + T5Stats[0, 5]));
                break;
        }
        tempStats[9] = XPtoNextLvl(chosenCharecter);
        return tempStats;
    }
    private static void RemoveHero(int chosenCharecter)
    {
        HeroList.RemoveAt(chosenCharecter);
    }
    public static int XPtoNextLvl(int chosenCharecter) 
    {
        string tier = NamesAndtiers[HeroList[chosenCharecter][0],1];
        switch (tier)
        {
            case "T0":
                return (XPT0[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
            case "T1":
                return (XPT1[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
            case "T2":
                return (XPT2[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
            case "T3":
                return (XPT3[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
            case "T4":
                return (XPT4[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
            default:
                return (XPT5[(HeroList[chosenCharecter][1])] - HeroList[chosenCharecter][2]);
        }
    }
    public static int[] XPStartEnd(int chosenCharecter) 
    {
        string tier = NamesAndtiers[HeroList[chosenCharecter][0], 1];
        int[] temp;
        switch (tier)
        {
            case "T0":
                temp = new int[2] { XPT0[(HeroList[chosenCharecter][1])] - 1, XPT0[(HeroList[chosenCharecter][1])]};
                return temp;
            case "T1":
                temp = new int[2] { XPT1[(HeroList[chosenCharecter][1])] - 1, XPT1[(HeroList[chosenCharecter][1])] };
                return temp;
            case "T2":
                temp = new int[2] { XPT2[(HeroList[chosenCharecter][1])] - 1, XPT2[(HeroList[chosenCharecter][1])] };
                return temp;
            case "T3":
                temp = new int[2] { XPT3[(HeroList[chosenCharecter][1])] - 1, XPT3[(HeroList[chosenCharecter][1])] };
                return temp;
            case "T4":
                temp = new int[2] { XPT4[(HeroList[chosenCharecter][1])] - 1, XPT4[(HeroList[chosenCharecter][1])] };
                return temp;
            default:
                temp = new int[2] { XPT5[(HeroList[chosenCharecter][1])] - 1, XPT5[(HeroList[chosenCharecter][1])] };
                return temp;
        }
    }
    public static List<int[]> herosThatCanMelt(int chosenCharecter)//Must be run after each hero is placed in the melt chamber.  Returns same as heroes unlocked.
    {
        //Can we modify to exclude from the returned list the chosen character?? i.e. you can't melt what you're using by accident. From Joe
        int maxXp = findCurrentMax(chosenCharecter);
        List<int[]> Allhero = new List<int[]> { };
        int[] tempHero = new int[4] { 0, 0, 0, 0};
        for (int i = 0; i < HeroList.Count; i++)
        {
            if(HeroList[i][2] < maxXp)
            {
                tempHero[0] = i;
                tempHero[1] = HeroList[i][0];
                tempHero[2] = HeroList[i][1];
                tempHero[3] = HeroList[i][3];
                Allhero.Add(tempHero);
            }
        }
        return Allhero;
    }
    public static void meltHero(List<int> CharectersMelt,int TargetCharecter)//Send a List of heroes to be melted down must do herosThatCanMelt First to Verify xp is not more than hero can take. Send the ListID in LIST FORM.
    {
        for(int x = 0; x < CharectersMelt.Count; x++)
        {
            HeroList[TargetCharecter][2] += HeroList[CharectersMelt[x]][2];
            RemoveHero(CharectersMelt[x]);
        }
    }
    public static bool buyHeroUpgradeCheck(int chosenCharecter)//call this to set upgrade button to on
    {
        string tier = NamesAndtiers[HeroList[chosenCharecter][0],1];
        int maxXP = findCurrentMax(chosenCharecter);
        switch (tier)
        {
            case "T0":
                return false;
            case "T1":
                if (HeroList[chosenCharecter][2] == maxXP && TeirNumberStars[1] > HeroList[chosenCharecter][3])
                {
                    if(T1Cost[(tierstartStar[1] - HeroList[chosenCharecter][3])] < InvManager.GoldReturn())
                        return true;
                }
                break;
            case "T2":
                if (HeroList[chosenCharecter][2] == maxXP && TeirNumberStars[2] > HeroList[chosenCharecter][3])
                {
                    if (T1Cost[(tierstartStar[2] - HeroList[chosenCharecter][3])] < InvManager.GoldReturn())
                        return true;
                }
                break;
            case "T3":
                if (HeroList[chosenCharecter][2] == maxXP && TeirNumberStars[3] > HeroList[chosenCharecter][3])
                {
                    if (T1Cost[(tierstartStar[3] - HeroList[chosenCharecter][3])] < InvManager.GoldReturn())
                        return true;
                }
                break;
            case "T4":
                if (HeroList[chosenCharecter][2] == maxXP && TeirNumberStars[4] > HeroList[chosenCharecter][3])
                {
                    if (T1Cost[(tierstartStar[4] - HeroList[chosenCharecter][3])] < InvManager.GoldReturn())
                        return true;
                }
                break;
            default:
                if (HeroList[chosenCharecter][2] == maxXP && TeirNumberStars[5] > HeroList[chosenCharecter][3])
                {
                    if (T1Cost[(tierstartStar[5] - HeroList[chosenCharecter][3])] < InvManager.GoldReturn())
                        return true;
                }
                break;
        }
        return false;
    }
    public static void buyHeroUpgrade(int chosenCharecter)//call this to buy hero upgrade
    {
        string tier = NamesAndtiers[HeroList[chosenCharecter][0],1];
        switch (tier)
        {
            case "T1":
                InvManager.GoldAdd(T1Cost[(tierstartStar[1] - HeroList[chosenCharecter][3])]);
                HeroList[chosenCharecter][3]++;
                break;
            case "T2":
                InvManager.GoldAdd(T2Cost[(tierstartStar[2] - HeroList[chosenCharecter][3])]);
                HeroList[chosenCharecter][3]++;
                break;
            case "T3":
                InvManager.GoldAdd(T3Cost[(tierstartStar[3] - HeroList[chosenCharecter][3])]);
                HeroList[chosenCharecter][3]++;
                break;
            case "T4":
                InvManager.GoldAdd(T4Cost[(tierstartStar[4] - HeroList[chosenCharecter][3])]);
                HeroList[chosenCharecter][3]++;
                break;
            default:
                InvManager.GoldAdd(T5Cost[(tierstartStar[5] - HeroList[chosenCharecter][3])]);
                HeroList[chosenCharecter][3]++;
                break;
        }
    }
    public static string HeroName(int chosenCharecter)
    {
        return NamesAndtiers[chosenCharecter, 0];
    }
    public static void levelUp(int chosenCharecter)
    {
        if (HeroList[chosenCharecter][1] < findCurrentMax(chosenCharecter))
        {
            HeroList[chosenCharecter][1]++;
        }
    }
    public static int GetCurrentHero() => CurrentHero[1];
    public static int [] setTempHero(int Hero)
    {
        CurrentHero[0] = Hero;
        return HeroList[Hero].ToArray();
    }
    public static int getTempHero() => CurrentHero[0];
    public static int[] EndofLevel(int xpGained)
    {
        if (canAddXP(CurrentHero[1]) > (HeroList[CurrentHero[1]][2] + xpGained))
        {
            HeroList[CurrentHero[1]][2] += xpGained;
            levelUp(CurrentHero[1]);
            return HeroList[CurrentHero[1]].ToArray();
        }
        else
        {
            HeroList[CurrentHero[1]][2] = canAddXP(CurrentHero[1]);
            return HeroList[CurrentHero[1]].ToArray();
        }
    }
    public static int[] SetCurrentHero(int chosenCharecter)
    {
        CurrentHero[1] = chosenCharecter;
        return HeroList[chosenCharecter].ToArray();
    }
    public static int[] UnlockedCharector(int i) //This returns a list of All heroes with their values (ListID, HeroID, Level, StarCount)
    {
        int[] tempHero = new int[4] { 0, 0, 0, 0} ;
        tempHero[0] = i; //idx for sorting list becomes whatever index they are in herolist
        tempHero[1] = HeroList[i][0];//tmphero 1 is herolist 0--Number aka actual hero type
        tempHero[2] = HeroList[i][1];//level
        tempHero[3] = HeroList[i][3];//number of stars
        return tempHero;
    }
    public static int numOfHeroes() => HeroList.Count();
    public static void AddCharecter(int shardT)
    {
        int tempInt;
        int[] TempHero = HeroDefault;
        Random rand = new Random();
        switch (shardT)
        {
            case 1:
                tempInt = rand.Next(0, 100);
                if (ShardCounter[0] == 9)
                {
                    TempHero[0] = tiersByHero[2][rand.Next(0, tiersByHero[2].Count())];
                    ShardCounter[0] = 0;
                }
                else
                {
                    switch (tempInt)
                    {
                        case int n when (n < 40):
                            TempHero[0] = tiersByHero[0][rand.Next(0, tiersByHero[0].Count())];
                            ShardCounter[0]++;
                            break;
                        case int n when (n < 80):
                            TempHero[0] = tiersByHero[1][rand.Next(0, tiersByHero[1].Count())];
                            ShardCounter[0]++;
                            break;
                        case int n when (n < 95):                            
                            TempHero[0] = tiersByHero[2][UnityEngine.Random.Range(0, tiersByHero[2].Count())];
                            break;
                        default:
                            TempHero[0] = tiersByHero[3][rand.Next(0, tiersByHero[3].Count())];
                            break;

                    }
                }
                break;
            default:
                tempInt = rand.Next(0, 10000);
                if (ShardCounter[1] == 9)
                {
                    TempHero[0] = tiersByHero[3][rand.Next(0, tiersByHero[3].Count())];
                    ShardCounter[1] = 0;
                }
                else
                {
                    switch (tempInt)
                    {
                        case int n when (n < 8975):
                            TempHero[0] = tiersByHero[2][rand.Next(0, tiersByHero[2].Count())];
                            ShardCounter[1]++;
                            break;
                        case int n when (n < 9975):
                            TempHero[0] = tiersByHero[3][rand.Next(0, tiersByHero[3].Count())];
                            break;
                        default:
                            TempHero[0] = tiersByHero[4][rand.Next(0, tiersByHero[4].Count())];
                            break;
                    }
                }
                break;
        }
        HeroList.Add(TempHero);
        HeroList[HeroList.Count()-1] = GetCharecterStats(HeroList.Count() - 1);
    }
    public static string SaveManagerData()//Save Engine Do Not Call
    {
        string sendSave = "";
        for (int x = 0; x < HeroList.Count; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                sendSave += HeroList[x][y].ToString();
                sendSave += ",";
            }
            sendSave += "|";
        }
        return sendSave;
    }
    public static void LoadManagerData(string input)//Save Engine Do Not Call
    {
        if (input == "")
        {
            NewGame();
            HeroList[0] = GetCharecterStats(0);
        }
        else
        {
            int counter = 0;
            int counting = 0;
            while (input.IndexOf('|') > 0)
            {
                HeroList.Add(HeroDefault);
                while (input.IndexOf(',') < input.IndexOf('|'))
                {
                    HeroList[counter][counting] = Convert.ToInt32(input.Substring(0, (input.IndexOf(',') - 1)));
                    input.Remove(0);
                    counting++;
                }
                counting = 0;
                input.Remove(0);
                HeroList[counter] = GetCharecterStats(counter);
                counter++;
            }
        }
    }
}