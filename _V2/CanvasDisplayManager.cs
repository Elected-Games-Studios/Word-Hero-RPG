using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum CanvasType
{
    MainMenu,
    WorldMap,
    RegionMap,
    HeroHome,
    HeroSelected,
    LoadingScreen,
    Gameplay,
    ClassUp,
    Enhance,
    Multiplayer,
    PvP
}

public class CanvasDisplayManager : MonoBehaviour
{
    public static event Action Rise;
    public static event Action Revert;
    public static event Action GenerateHeroTiles;
    public static CanvasDisplayManager instance;
    List<CanvasController> canvasControllerList;
    CanvasController lastActiveCanvas;
    CanvasController desiredCanvas;

    public CanvasController GetLastActiveCanvas()
    {
        return lastActiveCanvas;
    }
    protected void Awake()
    {
        //base.Awake();
        instance = this;
        lastActiveCanvas = new CanvasController();
        desiredCanvas = new CanvasController();
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.MainMenu);
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }
        
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            // fire events based on canvas rendered
            switch (desiredCanvas.canvasType)
            {
                case CanvasType.HeroSelected:
                    Rise?.Invoke();
                    break;
                case CanvasType.MainMenu:
                    Revert?.Invoke();
                    break;
                case CanvasType.HeroHome:
                    GenerateHeroTiles?.Invoke();
                    Revert?.Invoke();
                    break;
          
                default:
                    break;
            }
            lastActiveCanvas = desiredCanvas;
        }
        else { Debug.LogWarning("The desired canvas was not found!"); }
    }
}
