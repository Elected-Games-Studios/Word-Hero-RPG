using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeltHeroGridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private MelterXPBar xpslide;
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
        MeltableHeros = CharectorStats.herosThatCanMelt(CharectorStats.getTempHero());
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

    public void MeltableButtonClicked(int buttonIdx, bool clicked)
    {
        Debug.Log("Melt btn clicked");
        if (clicked == false)
        {
            Debug.Log("clicked was false");
            SelectedToMelt.Add(MeltableHeros[buttonIdx][1]);//<<what number should this effing be?! my head hertz
                                                            //highlight, other image modifications
            RefreshMeltableHeros();
            xpslide.UpdateSlider();
        }
        else
        {
            Debug.Log("clicked was true");
            SelectedToMelt.Remove(MeltableHeros[buttonIdx][1]);
            //dehighlight
            RefreshMeltableHeros();
            xpslide.UpdateSlider();
        }

    }

    public void MeltHeros()
    {
        CharectorStats.meltHero(SelectedToMelt, CharectorStats.getTempHero());
    }
}
