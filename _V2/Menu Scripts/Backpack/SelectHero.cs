﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviour
{
    [SerializeField]
    private GameObject CharactersObject;
    Button selectBtn;

    private void Start()
    {
        selectBtn = GetComponent<Button>();
        selectBtn.onClick.AddListener(SetHero);
    }
    void SetHero()
    {
        CharectorStats.SetCurrentHero(CharectorStats.getTempHero());
        Debug.Log("Hero is now: " + CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero())[0] + CharectorStats.HeroName(CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero())[0]));
        PlayServices.Instance.SaveData();
    }
}
