using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> specificMonster;
    public static EnemySpawn Instance;

    public AudioClip bossMusicIntro;
    public AudioClip bossMusicLoop;

    private void Awake()
    {
        Instance = this;
        if (!GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>())
        {
            SpawnEnemy();
        }

    }

    public void SpawnEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<IsEndless>())
        {
            Instantiate(enemy[Random.Range(0, enemy.Count)], transform);
        }
        else
        {
            if (PlayerStats.Instance.levelSelect == 15)
            {
                Instantiate(specificMonster[0], transform);
            }
            else if (PlayerStats.Instance.levelSelect == 25)
            {
                Instantiate(specificMonster[1], transform);
                StartCoroutine("BossMusic");
            }
            else
            {
                Instantiate(enemy[Random.Range(0, enemy.Count)], transform);
            }
        }
    }

    public void SpawnSpecific(int enemy)
    {
        Instantiate(specificMonster[enemy], transform);
    }

    IEnumerator BossMusic()  //if it's the boss music there's an intro before the loop
    {
        AudioSource audio = Camera.main.gameObject.GetComponent<AudioSource>();
        audio.clip = bossMusicIntro;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = bossMusicLoop;
        audio.Play();
    }

    
}
