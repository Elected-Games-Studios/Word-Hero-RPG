using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AscendGrid : MonoBehaviour
{
    [SerializeField]
    private MeltHeroGridManager MHGM;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private Ascend ascend;
    [SerializeField]
    private GameObject chooseButton;
    [SerializeField]
    private GameObject cancelButton;
    private TileListButton currentBtn;
    private List<GameObject> gridButtons = new List<GameObject>();
    private List<int> AvailableHeros = new List<int> { };
    public List<int> HeroesChosen = new List<int> { };
    HeroRequirements heroRequirements = new HeroRequirements();

    private void OnEnable()
    {
        chooseButton.GetComponent<Button>().onClick.AddListener(onChoose);
        cancelButton.GetComponent<Button>().onClick.AddListener(onCancel);
    }

    public void setHeroRequirements(HeroRequirements data)
    {
        heroRequirements = data;               
    }

    public void GenButtons()
    {
        int[] tempStats;
        AvailableHeros.Clear();
        HeroesChosen.Clear();
        //populate available heros who match the tier requirements
        AvailableHeros = CharectorStats.heroesThatCanBeSacrificed(heroRequirements.heroType, heroRequirements.stars);

        if (gridButtons.Count > 0)
        {
            foreach (GameObject button in gridButtons)
            {
                Destroy(button.gameObject);
            }
            gridButtons.Clear();
        }
        for (int i = 0; i < AvailableHeros.Count; i++)
        {
            tempStats = CharectorStats.UnlockedCharector(AvailableHeros[i]);
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            gridButtons.Add(button);
            button.SetActive(true);

            button.GetComponent<TileListButton>().SetText(CharectorStats.HeroName(tempStats[1]));
            button.GetComponent<TileListButton>().SetSacrificeIndex(i, tempStats[0]);//also sets toggle functionality and backpackindex to the button
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    private void Update()
    {
        if (HeroesChosen.Count == heroRequirements.amount)
        {
            chooseButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            chooseButton.GetComponent<Button>().interactable = false;
        }
    }
    public void updateHeroesChosen()
    {
        foreach (GameObject button in gridButtons)
        {
            if (button.GetComponent<TileListButton>().toggleSacrificeSelect == true)
            {
                HeroesChosen.Add(button.GetComponent<TileListButton>().backpackIdx);
            }
        }
    }
    public void onCancel()
    {
        gameObject.SetActive(false);
    }
    public void onChoose()
    {    
        ascend.fillIconsWithChosen(HeroesChosen);
        MHGM.isMaxedActually = false;
        gameObject.SetActive(false);
    }

}
