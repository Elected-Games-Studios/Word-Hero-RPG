using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharactersObjectSwitch : MonoBehaviour
{
    public void SetToTemp()
    {
        disableObjects();
        transform.GetChild(CharectorStats.setTempHero(CharectorStats.getTempHero())[0]).gameObject.SetActive(true);
    }

    public void SetToCurrent()
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
}
