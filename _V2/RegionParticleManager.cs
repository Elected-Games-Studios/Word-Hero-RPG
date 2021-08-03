using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionParticleManager : MonoBehaviour
{

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (i == GameMaster.Region)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(GameMaster.Region).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
