using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Spellcaster : MonoBehaviour
{
    Rect myRect;
    void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0.5f));
        Destroy(gameObject, 4);
    }

}