using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class SaveManager
{
    public static string SaveParse()
    {
        string tempSave = "";
        tempSave += WordDatav2.SaveManagerData();
        tempSave += '#';
        tempSave += InvManager.SaveManager();
        tempSave += '#';
        tempSave += CharectorStats.SaveManagerData();
        return (tempSave);
    }
    public static void LoadSplit(string Loadstr)
    {
        string[] tempLoad = Loadstr.Split('#');
        if (tempLoad.Length > 0)
        {
            WordDatav2.LoadManagerData(tempLoad[0]);
            InvManager.LoadManager(tempLoad[1]);
            CharectorStats.LoadManagerData(tempLoad[2]);
        }
    }
}