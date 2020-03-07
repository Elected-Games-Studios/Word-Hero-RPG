using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingXPText : MonoBehaviour
{
    Rect myRect;

    void Awake()
    {
        //myRect = gameObject.GetComponent<Rect>();
        //myRect.position = new Vector2(0, 10f);
        GetComponent<TextMesh>().text = ("+" + GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCombat>().xpGiven.ToString() + " XP");
        GetComponent<Rigidbody2D>().velocity = (new Vector2 (0, 1f));
        Destroy(gameObject, 1);
    }
}
