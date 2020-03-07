using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Rect myRect;

    void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1f));
        Destroy(gameObject, 1);
    }
}
