using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFinger : MonoBehaviour
{
    bool isSwiping = true;

    Rigidbody2D rb;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopLine();
        }
        if (isSwiping)
        {
            UpdateLine();
        }
    }

    void UpdateLine()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;
    }

    void StartLine()
    {
        isSwiping = true;
    }

    void StopLine()
    {
        isSwiping = false;
    }
}