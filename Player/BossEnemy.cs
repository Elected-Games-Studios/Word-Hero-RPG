using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    public AudioClip roar;
    public AudioClip dead;
    AudioSource enemyAudio;
    public GameObject enemy;

    private void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.clip = roar;
        enemyAudio.Play();
    }

    public void Attack1()
    {
        Instantiate(attack1, enemy.transform);

    }

    public void Attack2()
    {
        Instantiate(attack2, enemy.transform);

    }

    public void Attack3()
    {
        Instantiate(attack3, enemy.transform);
;
    }

    public void Attack4()
    {
        Instantiate(attack4, enemy.transform);

    }
}
