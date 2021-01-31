using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Panels;
    private GameObject chosenPanel;

   public void OpenPanel()
    {
        int[] temp = CharectorStats.setTempHero(CharectorStats.getTempHero());
        if (CharectorStats.HeroIsMaxLvl(temp))
        {
            chosenPanel = Panels[1]; //1 is tier up panel
        }
        else
        {
            chosenPanel = Panels[0]; //0 is melt panel
        }
        if(chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(true);
        }
    }
}
