using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;

   public void OpenPanel()
    {
        if(Panel != null)
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
