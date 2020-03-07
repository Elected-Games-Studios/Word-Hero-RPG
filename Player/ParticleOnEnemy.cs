using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnEnemy : MonoBehaviour
{
    private void Awake()
    {
        transform.position = GameObject.FindGameObjectWithTag("Enemy").transform.position+ new Vector3(0, 1, 0);
    }
}
