using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MeltHeroGridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GameObject selectedBtnTemplate;
    [SerializeField]
    private MelterXPBar xpslide;

    public event Action lowerEnhBtnIndexes;
    private List<GameObject> GridButtonGameObjs;
    private List<GameObject> MeltingButtonGameObjs;
    private List<int> MeltableList;
    private List<int> SelectedToMeltList;
    private int xpToBeAdded;
    private int lvlToPass = 0;

    private void Awake()
    {
        //ints used to generate buttons
        MeltableList = new List<int>() { };
        SelectedToMeltList = new List<int>() { 250 };

        //actual button lists
        GridButtonGameObjs = new List<GameObject>() { };
        MeltingButtonGameObjs = new List<GameObject>() { };
    }
    private void OnEnable()
    {
        xpslide.CloneHero();
        xpslide.UpdateSlider(0);
        xpslide.SetCurrentAndBoundText();
        xpToBeAdded = 0;        
        InitializeMeltGrid();
        xpslide.SetHeroNameText();
        //need to remove selectedButtons if refreshing panel
        ClearSelectedGameObjs();
    }
 
    public void InitializeMeltGrid()
    {
        int[] tempStats;       
        MeltableList.Clear();
        SelectedToMeltList.Clear();
        MeltableList = CharectorStats.heroesThatCanMelt(CharectorStats.getTempHero(), xpToBeAdded, SelectedToMeltList); 
        
        if (GridButtonGameObjs.Count > 0)
        {
            foreach (GameObject button in GridButtonGameObjs)
            {
                Destroy(button.gameObject);
            }
            GridButtonGameObjs.Clear();
        }
        for (int i = 0; i < MeltableList.Count; i++)
        {
            tempStats = CharectorStats.UnlockedCharector(MeltableList[i]);

            GameObject button = Instantiate(buttonTemplate) as GameObject;
            GridButtonGameObjs.Add(button);
            button.SetActive(true);

            button.GetComponent<EnhanceListButton>().SetText(CharectorStats.HeroName(tempStats[1]));
            button.GetComponent<EnhanceListButton>().SetHeroNum(tempStats[0]);
            button.GetComponent<EnhanceListButton>().SetIndex(i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }
    public void TransferToSelected()
    {
        
        int[] tempStats;
        int tempxp = 0;
       
        //Insert function to gray out tile on grid
        ClearSelectedGameObjs();
        for (int i=0; i < SelectedToMeltList.Count; i++)
        {
            tempStats = CharectorStats.UnlockedCharector(SelectedToMeltList[i]);
            tempxp = tempStats[4];
            GameObject button = Instantiate(selectedBtnTemplate) as GameObject;
            MeltingButtonGameObjs.Add(button);
            button.SetActive(true);

            button.GetComponent<SelectedToMeltBtn>().SetText(CharectorStats.HeroName(tempStats[1]));
            button.GetComponent<SelectedToMeltBtn>().SetIndex(i);
            button.GetComponent<SelectedToMeltBtn>().SetHeroNum(tempStats[0]);
            button.transform.SetParent(selectedBtnTemplate.transform.parent, false);
        }

        xpToBeAdded += tempxp;
        


    }
    public void RemoveFromSelectedRow(int idx)
    {
        Destroy(MeltingButtonGameObjs[idx]);
        MeltingButtonGameObjs.RemoveAt(idx);
        
        
        foreach(GameObject button in MeltingButtonGameObjs)
        {
            
            if(button.GetComponent<SelectedToMeltBtn>().ThisButtonIndex > idx)
            {
                Debug.Log("calling LowerIndex");
                button.GetComponent<SelectedToMeltBtn>().LowerIndex();
            } 
        }
    }

    void ClearSelectedGameObjs()
    {
        if (MeltingButtonGameObjs.Count > 0)
        {
            foreach (GameObject button in MeltingButtonGameObjs)
            {
                Destroy(button.gameObject);
            }
            MeltingButtonGameObjs.Clear();
        }
    }
  
    public bool MeltableButtonClicked(int buttonIdx, bool clicked, int BPIndex)
    {
        if (clicked == false)
        {    
            SelectedToMeltList.Add(BPIndex);
            TransferToSelected();
            int[] tempStats = CharectorStats.UnlockedCharector(BPIndex);
            xpslide.UpdateSlider(tempStats[4]);
            xpslide.SetCurrentAndBoundText();
            clicked = true;
            return clicked;
        }
        else
        {
            clicked = false;
            int[] tempStats = CharectorStats.UnlockedCharector(BPIndex);
            xpToBeAdded -= tempStats[4];
            GameObject temp = new GameObject();
            for (int i = 0; i < GridButtonGameObjs.Count; i++)
            {
                if(GridButtonGameObjs[i].GetComponent<EnhanceListButton>().heroNum == BPIndex)
                {
                    temp = GridButtonGameObjs[i];
                    break;
                }
            }
            temp.GetComponent<EnhanceListButton>().clicked = false;

            SelectedToMeltList.RemoveAt(buttonIdx);
            RemoveFromSelectedRow(buttonIdx);
            
            
            xpslide.UpdateSlider(-tempStats[4]);
            xpslide.SetCurrentAndBoundText();
            return clicked;
        }

    }

    public void MeltHeros()
    {
        CharectorStats.meltHero(SelectedToMeltList, xpToBeAdded);
        OnEnable();
    }
}
