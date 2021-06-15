using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum CanvasType //ctype
{
    MainMenu,
    WorldMap,
    RegionMap,
    HeroHome,
    HeroSelected,
    LoadingScreen,
    Gameplay,
    Enhance,
    Multiplayer,
    RankedSearch,
    PvpBackpack,
    PvP
}

public class CanvasDisplayManager : MonoBehaviour
{
    [SerializeField]
    private MenuCharactersObjectSwitch charaSwitch;
    public static event Action Rise, Revert, GoAway, ComeBack, GenerateHeroTiles;
    private float wait = GameMaster.GlobalScreenTransitionTimer;
    public static CanvasDisplayManager instance;
    [SerializeField]
    private List<CanvasController> canvasControllerList;
    [SerializeField]
    private Canvas blackCanvas;
    private static CanvasController lastActiveCanvas;
    private static CanvasController desiredCanvas;
    public event Action<CanvasController> CurrentCanvasChanged;
    [SerializeField]
    private GameObject characterObject;

    public CanvasController GetLastActiveCanvas()
    {
        return lastActiveCanvas;
    }
    protected void Awake()
    {
        instance = this;
        lastActiveCanvas = new CanvasController();
        desiredCanvas = new CanvasController();
        SwitchCanvas(CanvasType.MainMenu);

    }

    public void SwitchCanvas(CanvasType _type)
    {
        desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
       
        if (desiredCanvas != null)
        {
            var dType = desiredCanvas.canvasType;
            var lType = lastActiveCanvas.canvasType;

            //  World Map to Main Menu, both directions fade to black
            if ((dType == CanvasType.WorldMap && lType == CanvasType.MainMenu)
                || (dType == CanvasType.MainMenu && lType == CanvasType.WorldMap))
            {
                StartCoroutine("FadeCanvasOutBlack", lastActiveCanvas);
                StartCoroutine("FadeCanvasInFromBlack", desiredCanvas as CanvasController);
            }

            //Main Menu to Backpack, both directions fade to black
            if ((dType == CanvasType.MainMenu && lType == CanvasType.HeroHome) ||
                (dType == CanvasType.HeroHome && lType == CanvasType.MainMenu))
            {
                StartCoroutine("FadeCanvasOutBlack", lastActiveCanvas);
                StartCoroutine("FadeCanvasInFromBlack", desiredCanvas as CanvasController);
            }

            //Backpack to Hero Selected Screen, shift both left or shift both right to reverse
            if (dType == CanvasType.HeroSelected && lType == CanvasType.HeroHome)
            {
                StartCoroutine("SweepCanvasOutLeft", lastActiveCanvas);
                StartCoroutine("SweepCanvasInFromRight", desiredCanvas as CanvasController);
            }
            if (dType == CanvasType.HeroHome && lType == CanvasType.HeroSelected)
            {
                StartCoroutine("SweepCanvasOutRight", lastActiveCanvas);
                StartCoroutine("SweepCanvasInFromLeft", desiredCanvas as CanvasController);
            }

            //Hero Selected to Enhance, shift both down, both up to reverse
            if (dType == CanvasType.Enhance && lType == CanvasType.HeroSelected)
            {
                StartCoroutine("SweepCanvasOutBottom", lastActiveCanvas);
                StartCoroutine("SweepCanvasInFromTop", desiredCanvas as CanvasController);
            }
            if (dType == CanvasType.HeroSelected && lType == CanvasType.Enhance)
            {
                StartCoroutine("SweepCanvasOutTop", lastActiveCanvas);
                StartCoroutine("SweepCanvasInFromBottom", desiredCanvas as CanvasController);
            }
            CurrentCanvasChanged?.Invoke(desiredCanvas);
            lastActiveCanvas = desiredCanvas;
            
        }
        else { Debug.LogWarning("The desired canvas was not found!"); }
    }


    void SetLastCanvasFalse(CanvasController canvas)
    {
        canvas.gameObject.SetActive(false);
    }

    IEnumerator FadeCanvasOutBlack(CanvasController canvas)
    {
        blackCanvas.gameObject.SetActive(true);
        blackCanvas.GetComponent<CanvasGroup>().LeanAlpha(1, .3f);
        yield return new WaitForSeconds(.3f);
        SetLastCanvasFalse(canvas);
    }
    IEnumerator FadeCanvasInFromBlack(CanvasController canvas)
    {
        yield return new WaitForSeconds(.3f);
        canvas.gameObject.SetActive(true);
        blackCanvas.GetComponent<CanvasGroup>().LeanAlpha(0, .3f);      
        yield return new WaitForSeconds(.3f);
        blackCanvas.gameObject.SetActive(false);
    }

    IEnumerator SweepCanvasOutLeft(CanvasController canvas)
    {
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject,-Screen.width * 2, .5f);
        yield return new WaitForSeconds(.5f);
        SetLastCanvasFalse(canvas);
    }
    IEnumerator SweepCanvasInFromRight(CanvasController canvas)
    {
        canvas.gameObject.SetActive(true);
        var endX = canvas.transform.localPosition.x;
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject, Screen.width * 2, 0f);
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject, endX, .5f);
        yield return null;
    }

    IEnumerator SweepCanvasOutRight(CanvasController canvas)
    {
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject, Screen.width * 2, .5f);
        yield return new WaitForSeconds(.5f);
        SetLastCanvasFalse(canvas);
    }
    IEnumerator SweepCanvasInFromLeft(CanvasController canvas)
    {
        canvas.gameObject.SetActive(true);
        var endX = canvas.transform.localPosition.x;
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject, -Screen.width * 2, 0f);
        LeanTween.moveLocalX(canvas.gameObject.transform.GetChild(0).gameObject, endX, .5f);
        yield return null;
    }

    IEnumerator SweepCanvasOutTop(CanvasController canvas)
    {
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, Screen.height * 2, .5f);
        yield return new WaitForSeconds(.5f);
        SetLastCanvasFalse(canvas);
    }
    IEnumerator SweepCanvasInFromBottom(CanvasController canvas)
    {
        canvas.gameObject.SetActive(true);
        var endX = canvas.transform.localPosition.x;
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, -Screen.height * 2, 0f);
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, endX, .5f);
        yield return null;
    }

    IEnumerator SweepCanvasOutBottom(CanvasController canvas)
    {
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, -Screen.height * 2, .5f);
        yield return new WaitForSeconds(.5f);
        SetLastCanvasFalse(canvas);
    }
    IEnumerator SweepCanvasInFromTop(CanvasController canvas)
    {
        canvas.gameObject.SetActive(true);
        var endX = canvas.transform.localPosition.x;
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, Screen.height * 2, 0f);
        LeanTween.moveLocalY(canvas.gameObject.transform.GetChild(0).gameObject, endX, .5f);
        yield return null;
    }


    IEnumerator GrowCanvasFromBottom(CanvasController canvas)
    {
        yield return new WaitForSeconds(wait);

    }

    IEnumerator ShrinkCanvasToBottom(CanvasController canvas)
    {
        yield return new WaitForSeconds(wait);

    }


}
