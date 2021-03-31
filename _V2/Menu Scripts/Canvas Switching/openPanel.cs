using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Panels; //Panels will be populated differently depending on what the script is on. Can be further optimized.
    private GameObject chosenPanel;

   public void OpenPanel()
    {
        int tempHero = CharectorStats.getTempHero();
        if (CharectorStats.HeroIsMaxLvl(tempHero))
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
    public void OpenLevelStartPanel()
    {
        chosenPanel = Panels[2]; 
        if (chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(true);
        }
    }
    public void OpenVictoryDefeat()
    {
        chosenPanel = Panels[0];
        if (chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(true);
        }
    }
    public void OpenConfirmExit()
    {
        chosenPanel = Panels[0];
        if (chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(true);
        }
    }
    public void OpenAscendSelection()
    {
        chosenPanel = Panels[0];
        if (chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(true);
        }
    }
    public void CloseAnyPanel()
    {
        if (chosenPanel != null)
        {
            chosenPanel.gameObject.SetActive(false);
        }
    }

}
