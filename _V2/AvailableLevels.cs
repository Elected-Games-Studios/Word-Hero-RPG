using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableLevels : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelNodes;
    private void Awake()
    {
        //int[,,] tempRegion = WordDatav2.RegionUnlocks(GameMaster.Region);
        int[,,] tempRegion = new int[2, 3, 25]
        { { { 10, 10, 10, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
        { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } } };


        int levelCount = 1;
        levelNodes[0].SetActive(true);
        while (tempRegion[0, GameMaster.Difficulty, levelCount] > 0)
        {
            levelNodes[levelCount].SetActive(true);
            levelCount++;
            if (levelCount > 24)//region fully unlocked, maybe do something here?
            if (levelCount == 25) break;
        }
        
    }


}
