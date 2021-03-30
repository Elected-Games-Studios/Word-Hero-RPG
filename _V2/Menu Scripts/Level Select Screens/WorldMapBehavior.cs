using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapBehavior : MonoBehaviour
{
    private int[] RegionIntroduction = new int[] {1, 2, 3, 4, 5, 6, 7, 8 };  
    private int currentRegionIntro;
    [SerializeField]
    private List<GameObject> regionNodes = new List<GameObject>();
    [SerializeField]
    private GameObject worldMap;

    private void OnEnable()
    {
        foreach (GameObject node in regionNodes)
        {
            node.SetActive(false);
        }
        for (int i = 0; i < regionNodes.Count; i++)
        {
            if (GameMaster.completedRegionIntros[i] == true)
            {
                regionNodes[i].SetActive(true);
            }
            else { break; }
        }
        if(GameMaster.lastCompletedLevel == 24 && GameMaster.completedRegionIntros[GameMaster.Region + 1] == false){
            GameMaster.completedRegionIntros[GameMaster.Region + 1] = true;
            GameMaster.Region++;
            StartCoroutine(AnimateRegion(RegionIntroduction[GameMaster.Region]));
        }
    }

    private IEnumerator AnimateRegion(int regionIntro)
    {
        //Do stuff to show new region
        worldMap.SetActive(true);
        regionNodes[regionIntro -1].SetActive(true);
        Debug.Log("unlocked region: " + (regionIntro -1));
        yield return null;
    }

}
