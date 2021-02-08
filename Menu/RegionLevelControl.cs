using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionLevelControl : MonoBehaviour
{
    public GameObject openLevels;
    public GameObject closedLevels;
    public int region;
    PlayerStats stats;

    private void Awake()
    {
        stats = PlayerStats.Instance;
        //stats.regionProgress = region;
        OpenLevels();
    }

    public void Intro()
    {
        stats.regionLoad = region;
        stats.levelSelect = 0;
        SceneManager.LoadScene(5);
    }

    public void OpenLevels()
    {
        for (int i = 0; i < openLevels.transform.childCount; i++)
        {
            openLevels.transform.GetChild(i).gameObject.SetActive(false);
            closedLevels.transform.GetChild(i).gameObject.SetActive(false);
        } //Clear all gridButtons
 
        if (stats.regionProgress > region)                           //if you have the next region unlocked, open all levels
        {
            for (int i = 0; i < openLevels.transform.childCount; i++)
            {
                openLevels.transform.GetChild(i).gameObject.SetActive(true);
                closedLevels.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        else                                                         //If not, open up to the level youre on
        {
            for (int i = 0; i < stats.levelProgress + 1; i++)
            {
                openLevels.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = stats.levelProgress + 1; i < closedLevels.transform.childCount; i++)
            {
                closedLevels.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        AssignStars();
    }

    public void AssignStars()
    {
        for(int i = 1; i< openLevels.transform.childCount; i++) //For each open level beyond Intro
        {
            int starLevel = ((region - 1) * 25) + i - 1; //Set level to start at index based on region and increment for every level

            for(int j = 0; j< stats.levelStars[starLevel]; j++) //For each star in that index, turn one on.
            {
                gameObject.transform.GetChild(0).GetChild(i).GetChild(2).GetChild(j).gameObject.SetActive(true);
            }
        }
    }
}
