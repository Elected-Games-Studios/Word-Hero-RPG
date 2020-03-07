using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Background Manager Awake Running and you are on Level: " + PlayerStats.Instance.levelSelect);




        if (GameObject.FindGameObjectWithTag("Manager"))
        {
            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<IsEndless>())
            {
                Debug.Log("This is Endless Mode Bitches");
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Background " + transform.GetChild(0).gameObject.name + " Active");
            }
            else if (PlayerStats.Instance.levelSelect > 0)
            {
                Debug.Log("This is NOT Endless Mode and you are NOT Bitches");
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                transform.GetChild(PlayerStats.Instance.levelSelect - 1).gameObject.SetActive(true);
            }
        }




    }
}
