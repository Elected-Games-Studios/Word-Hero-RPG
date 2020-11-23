using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManagerv2 : MonoBehaviour
{
    public delegate void LetterHit(int letterId); //SIGNATURE
    public static event LetterHit onLetterHit;

    public void FireHit(int letterId)
    {
        onLetterHit?.Invoke(letterId);
    }


}