using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onParticleLeaveScreen : MonoBehaviour
{
    public static event Action<GameObject> particleLeftScreen;


    private void OnParticleCollision(GameObject go)
    {
        go.SetActive(false);
        particleLeftScreen?.Invoke(go);
    }

}
