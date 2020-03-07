using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnThisOff : MonoBehaviour
{
    public int region;
    public int level;

    private void Awake()
    {
        if(PlayerStats.Instance.regionLoad == region && PlayerStats.Instance.levelProgress == level)
        {
            gameObject.SetActive(false);
        }
    }
}
