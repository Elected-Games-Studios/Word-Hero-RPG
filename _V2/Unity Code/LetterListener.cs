using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LetterListener : MonoBehaviour
{
    [SerializeField]
    private eventManagerv2 em;
    void OnEnable()
    {
        eventManagerv2.onLetterHit += AddLetterToCurrent;
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
        eventManagerv2.onLetterHit -= AddLetterToCurrent;
    }
}