using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void SetRegion(int region)
    {
        GameMaster.Region = region;
    }
    public void SetLevel(int level)
    {
        GameMaster.Level = level;
        Debug.Log("Level set to " + GameMaster.Level);
    }
    public void SetDifficulty(int diff)
    {
        GameMaster.Difficulty = diff;
    }
}
