using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : MonoBehaviour
{
    public static int Region, Level, Difficulty;
    public static string[] userIds = new string[2];

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


    //Set level data BE4ORE scene load/ on level select

    //
}