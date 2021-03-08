using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : MonoBehaviour
{
    public static int Region, Level, Difficulty;
    public static string[] userIds = new string[2];
    [Serializable]
    private List<TextAsset> dicList = new List<TextAsset>();

    private void Awake()
    {
        Region = 0;
        Difficulty = 0;
        userIds[0] = (Social.Active.localUser.id);
    }

    public static void CallSave()
    {
        PlayServices.Instance.SaveData();
    }

    public static string GetWord(int dicNum, int index)
    {
        dicNum -= 5;
        string strDic = dicList[dicNum].text.ToString();
        int temp;
        int end;

        for(int i = 0; i < index; i++)
        {
            temp = strDic.IndexOf('|', temp);
        }
        
        end = strDic.IndexOf('|', temp);
        temp++;
        end--;
        return (strDic.Substring(temp, end));
    }
    //Set level data BE4ORE scene load/ on level select

    //
}