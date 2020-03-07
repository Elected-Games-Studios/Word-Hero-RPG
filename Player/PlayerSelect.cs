using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{ 
    public List<GameObject> heroSelect;
    public PlayerStats stats;

    void Start()
    {
        stats = PlayerStats.Instance;
        for (int i = 0; i < heroSelect.Count; i++)
        {
            heroSelect[i].gameObject.SetActive(false);
        }
        heroSelect[stats.activeHero].gameObject.SetActive(true);
    }

}
