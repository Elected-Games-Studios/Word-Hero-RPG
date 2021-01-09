using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileListButtonControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private List<GameObject> buttons;
    private List<int[]> AllHeros;


    private void Awake()
    {
        buttons = new List<GameObject>();
    }
    public void GenButtons()
    {
        AllHeros = CharectorStats.UnlockedCharectors();
        if(buttons.Count > 0)
        {

            foreach(GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        for (int i = 0; i < AllHeros.Count; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(button);
            button.SetActive(true);

            button.GetComponent<TileListButton>().SetText(CharectorStats.HeroName(AllHeros[i][1]));
           
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    public void ButtonClicked(int buttonIdx)
    {
        CharectorStats.tempChosen = buttonIdx;       
    }

}
