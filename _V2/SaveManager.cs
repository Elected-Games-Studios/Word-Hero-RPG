using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class SaveManager
{
    public static byte[] SaveParse()
    {
        string tempSave = new string();
        tempSave += WordDatav2.LoadManagerData(tempLoad);
        tempSave += '#';
        tempSave += InvManager.LoadManager(tempLoad);
        tempSave += '#';
        tempSave += CharectorStats.LoadManagerData(tempLoad);
        return (Encoding.UTF8.GetBytes(tempSave));
    }
    public static void LoadSplit(string Loadstr)
    {
        string[] tempLoad = Loadstr.Split('#');
        WordDatav2.LoadManagerData(tempLoad[0]);
        InvManager.LoadManager(tempLoad[1]);
        CharectorStats.LoadManagerData(tempLoad[2]);
    }
}