﻿using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : MonoBehaviour
{
    public static int Region, Level, Difficulty;
    public static string[] userIds = new string[2];
    [SerializeField]
    private List<TextAsset> dicList;
    public static GameMaster instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Region = 0;
        Level = 0;
        Difficulty = 0;
        //Need to add check for if logged in.
        //userIds[0] = (Social.Active.localUser.id);
        if(instance == null)
        {
            instance = this;
        }
    }
    public void CallLocalSave()
    {
        SaveManager.SaveParse();
        Debug.Log("called Local Save");
    }

    public void CallSave()
    {
        PlayServices.Instance.SaveData();
    }

    public string GetWord(int dicNum, int index)
    {
        dicNum -= 5;
        string strDic = dicList[dicNum].text.ToString();
        int temp = 0;
        int end;

        for(int i = 0; i < index; i++)
        {
            temp = strDic.IndexOf('|', temp);
            temp++;
        }
        
        end = strDic.IndexOf('|', temp) - temp;
        return (strDic.Substring(temp, end));
    }
    //Set level data BE4ORE scene load/ on level select

    //
}