using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusic : MonoBehaviour
{
    public List<AudioClip> music;
    AudioSource source;
    WordManager wordMan;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        source = gameObject.GetComponent<AudioSource>();
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            foreach(GameObject musicObj in GameObject.FindGameObjectsWithTag("Music"))
            {
                if(PlayerStats.Instance.levelSelect == 1)
                {
                    source.clip = music[PlayerStats.Instance.regionLoad - 1];
                }
                musicObj.GetComponent<CombatMusic>().StartMusic();
            }
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (wordMan == null)
        {
            if(GameObject.FindGameObjectWithTag("Manager"))
            {
                GetWordMan();
            }
        }
        if(source.clip == null)
        {
            source.clip = music[PlayerStats.Instance.regionLoad - 1];
            source.Play();
        }
        if(PlayerStats.Instance.levelSelect == 25 || PlayerStats.Instance.levelSelect == 0)
        {
            source.Stop();
        }
    }

    public IEnumerator FadeOut()
    {
        while (source.volume > 0)
        {
            source.volume -= 1 * Time.deltaTime;

            yield return null;
        }

        source.Pause();
    }

    public IEnumerator FadeIn()
    {
        float startVolume = 1;
        source.volume = 0;
        source.Play();
        while (source.volume < startVolume)
        {
            source.volume += startVolume * Time.deltaTime / 2;

            yield return null;
        }
        source.volume = startVolume;

    }

    public void StopMusic()
    {
        StartCoroutine("FadeOut");
    }

    public void StartMusic()
    {
        StartCoroutine("FadeIn");
    }

    void GetWordMan()
    {
        wordMan = GameObject.FindGameObjectWithTag("Manager").GetComponent<WordManager>();
    }
}
