using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerv2 : MonoBehaviour
{
    public delegate void LetterHit(int letterId); //SIGNATURE
    public static event LetterHit onLetterHit;

    

    public void HitLetter(int letterId)
    {
        onLetterHit?.Invoke(letterId);
    }


}