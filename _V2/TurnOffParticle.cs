using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TurnOffParticle : MonoBehaviour
{
    [SerializeField]
    private float time;
    private float timeLeft;

    private void OnEnable()
    {
        timeLeft = time;
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

}
