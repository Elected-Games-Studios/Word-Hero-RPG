using UnityEngine;

public class ClassSelectPanels : MonoBehaviour
{
    public GameObject classMenus;

    public void OpenPanel()
    {
        classMenus.SetActive(true);
    }
    public void ClosePanel()
    {
        classMenus.SetActive(false);
    }

}
