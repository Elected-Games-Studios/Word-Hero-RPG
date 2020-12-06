using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerv2 : MonoBehaviour
{
    public delegate void LetterHit(string letterText); //SIGNATURE
    public static event LetterHit onLetterHit;

    

    public void HitLetter(string letterText)
    {
        onLetterHit?.Invoke(letterText);
    }


}