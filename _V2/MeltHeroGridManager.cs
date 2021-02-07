using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MeltHeroGridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private MelterXPBar xpslide;
    private List<GameObject> buttons;
    private List<int> MeltableHeros;
    private List<int> SelectedToMelt;
    private int xpToBeAdded;
    private List<int[]> allHeroStats;


    private void Awake()
    {
        allHeroStats = new List<int[]>() {};
        xpToBeAdded = 0;
        SelectedToMelt = new List<int>() { };
        buttons = new List<GameObject>();
        MeltableHeros = new List<int>();
    }
    public void InitializeMeltGrid()
    {
        int[] tempStats;
        MeltableHeros.Clear();
        MeltableHeros = CharectorStats.heroesThatCanMelt(CharectorStats.getTempHero(), xpToBeAdded, SelectedToMelt);
        
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        for (int i = 0; i < MeltableHeros.Count; i++)
        {
            tempStats = CharectorStats.UnlockedCharector(MeltableHeros[i]);

            GameObject button = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(button);
            button.SetActive(true);

            button.GetComponent<EnhanceListButton>().SetText(CharectorStats.HeroName(tempStats[1]));
            button.GetComponent<EnhanceListButton>().SetIndex(i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }
  
    public void MeltableButtonClicked(int buttonIdx, bool clicked)
    {
        if (clicked == false)
        {          
            SelectedToMelt.Add(MeltableHeros[buttonIdx]);
            Debug.Log("highlighting");
            xpslide.UpdateSlider();
            clicked = true;
        }
        else
        {
            SelectedToMelt.Remove(MeltableHeros[buttonIdx]);
            Debug.Log("Dehighlighting");          
            xpslide.UpdateSlider();
            clicked = false;
        }

    }

    public void MeltHeros()
    {
        CharectorStats.meltHero(SelectedToMelt, CharectorStats.getTempHero());
    }
}
