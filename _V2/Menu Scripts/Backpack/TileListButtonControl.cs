﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileListButtonControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private TileListButton currentBtn;
    private List<GameObject> buttons = new List<GameObject>();
    private List<int[]> AllHeros = new List<int[]> { };
    [SerializeField]
    private SummonPanel sp;

    private void OnEnable()
    {
        sp.onHerosGenerated += GenButtons;
        GenButtons();
    }
    public void GenButtons()
    {
        AllHeros.Clear();
        
        for(int i=0;i< CharectorStats.numOfHeroes(); i++)
        {
            AllHeros.Add(CharectorStats.UnlockedCharector(i));
        }

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
            button.GetComponent<TileListButton>().SetIndex(i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    private void OnDisable()
    {
        sp.onHerosGenerated -= GenButtons;
    }

}
