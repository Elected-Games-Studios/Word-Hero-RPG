using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeltHeroGridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private List<GameObject> buttons;
    private List<int[]> MeltableHeros;
    private List<int> SelectedToMelt;


    private void Awake()
    {
        buttons = new List<GameObject>();
        SelectedToMelt = new List<int>();
    }
    public void RefreshMeltableHeros()
    {
        MeltableHeros = CharectorStats.herosThatCanMelt(CharectorStats.tempChosen);
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
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(button);
            button.SetActive(true);

            button.GetComponent<EnhanceListButton>().SetText(CharectorStats.HeroName(MeltableHeros[i][1]));

            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    public void MeltableButtonClicked(int buttonIdx)
    {
        SelectedToMelt.Add(MeltableHeros[buttonIdx][0]);//<<what number should this effing be?! my head hertz
        //highlight, other image modifications
        RefreshMeltableHeros();
    }
    public void MeltHeros()
    {
        CharectorStats.meltHero(SelectedToMelt, CharectorStats.tempChosen);
    }
}
