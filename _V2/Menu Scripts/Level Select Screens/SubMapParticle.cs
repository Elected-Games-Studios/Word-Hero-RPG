using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapParticle : MonoBehaviour
{
    public bool OpeningMap;
    public bool OpenFromLeft;
    private Vector2 startPosition;
    private float direction;

    void OnEnable()
    {
        //bottom right: screen.width/2f, -screen.height/2f
        //top right: screen.width/2f, screen.height/2f
        //bottom left: -screen.width/2f, -screen.height/2f
        //top left: -screen.width/2f, screen.height/2f

        if(OpeningMap && OpenFromLeft)
        {
            startPosition = new Vector2(-Screen.width / 2f, -Screen.height / 2f);
            direction = 1.5f;
        }
        else if (OpeningMap && !OpenFromLeft)
        {
            startPosition = new Vector2(Screen.width / 2f, -Screen.height / 2f);
            direction = 1.5f;
        }
        else if(!OpeningMap && OpenFromLeft)
        {
            startPosition = new Vector2(-Screen.width / 2f, Screen.height / 2f);
            direction = -1.5f;
        }
        else
        {
            startPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
            direction = -1.5f;
        }
        OpeningMap = !OpeningMap;
        transform.localPosition = startPosition;
        gameObject.LeanMoveLocalY(Screen.height * direction, .75f).setEaseInOutSine();
    }


    public void MapOnLeft()
    {
        OpenFromLeft = true;
    }

    public void MapOnRight()
    {
        OpenFromLeft = false;
    }
}
