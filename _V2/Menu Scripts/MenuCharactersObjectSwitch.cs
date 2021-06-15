using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharactersObjectSwitch : MonoBehaviour
{
    [SerializeField]
    private CanvasDisplayManager cdm;
    private void Awake()
    {
        cdm.CurrentCanvasChanged += SetNewCharacterModel;
    }

    private void SetNewCharacterModel(CanvasController targetCanvas)
    {
        var typeID = targetCanvas.canvasType;
        //gameObject.SetActive(true);
        if (typeID == CanvasType.Multiplayer || typeID == CanvasType.PvP || typeID == CanvasType.MainMenu || typeID == CanvasType.HeroSelected)
        {
            SetToCurrent();          
            if (typeID == CanvasType.HeroSelected)
            {
                SetToTemp();
            }
        }
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    //these methods can also contain positional logic for displays
    private void SetToTemp()
    {
        disableObjects();
        transform.GetChild(CharectorStats.setTempHero(CharectorStats.getTempHero())[0]).gameObject.SetActive(true);
    }

    private void SetToCurrent()
    {
        disableObjects();
        transform.GetChild(CharectorStats.SetCurrentHero(CharectorStats.GetCurrentHero())[0]).gameObject.SetActive(true);
    }
    private void disableObjects()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        cdm.CurrentCanvasChanged -= SetNewCharacterModel;
    }
}
