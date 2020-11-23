using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LetterListener : MonoBehaviour
{
    [SerializeField]
    private EventManagerv2 em; // inspector assignement can be mitigated through onEnable assignment
    void OnEnable()
    {
        em = new EventManagerv2();
        EventManagerv2.onLetterHit += AddLetterToCurrent;
    }

    void AddLetterToCurrent(int letterId) //PARAMS HAVE TO MATCH THE DELEGATE SIG
    {
        if(letterId == Int32.Parse(gameObject.tag))
            Debug.Log(letterId);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        em.FireHit(int.Parse(gameObject.tag));
    }

    void OnDisable()
    {
        EventManagerv2.onLetterHit -= AddLetterToCurrent;
    }
}