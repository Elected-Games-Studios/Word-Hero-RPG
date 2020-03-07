using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(100 * Time.deltaTime, 0);
    }
}
