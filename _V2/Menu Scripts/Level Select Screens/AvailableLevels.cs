using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableLevels : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelNodes;
    private void OnEnable()
    {
        Debug.Log("Enabled Sub Map");
        levelNodes.ForEach(node => node.SetActive(false));
        int[,,] tempRegion = new int[2,3,25];
        tempRegion = WordDatav2.RegionUnlocks(GameMaster.Region);


        int levelCount = 0;
        levelNodes[0].SetActive(true);
        while (tempRegion[0, GameMaster.Difficulty, levelCount] > 0)
        {
            levelCount++;
            if (levelCount == 25)
            {//region fully unlocked, maybe do something here?
                break;
            }
            if (levelCount <= 24) { levelNodes[levelCount].SetActive(true); }
            
        }
        
    }


}
