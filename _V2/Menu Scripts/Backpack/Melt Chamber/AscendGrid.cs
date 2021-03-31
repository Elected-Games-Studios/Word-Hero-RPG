using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendGrid: MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private TileListButton currentBtn;
    private List<GameObject> buttons = new List<GameObject>();
    private List<int[]> AvailableHeros = new List<int[]> { };

    public void GenButtons()
    {
        AvailableHeros.Clear();
       //populate available heros who match the tier requirements

        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        for (int i = 0; i < AvailableHeros.Count; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(button);
            button.SetActive(true);

            button.GetComponent<TileListButton>().SetText(CharectorStats.HeroName(AvailableHeros[i][1]));
            button.GetComponent<TileListButton>().SetIndex(i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }
}
