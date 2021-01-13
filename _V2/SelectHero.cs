using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviour
{
    Button selectBtn;
    private void Start()
    {
        selectBtn = GetComponent<Button>();
        selectBtn.onClick.AddListener(SetHero);
    }
    void SetHero()
    {
        CharectorStats.SetCurrentHero(CharectorStats.getTempHero());
    }
}
