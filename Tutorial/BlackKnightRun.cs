using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightRun : MonoBehaviour
{
    Animator anim;
    bool justSpawned = true;
    bool isABoss;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        justSpawned = true;
    }

    private void Update()
    {
        if (justSpawned)
        {
            anim.SetBool("isRunning", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1.3f, 0));
        }
        else
        {
            anim.SetBool("isRunning", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CinemaStop")
        {
            justSpawned = false;
        }
    }


}
