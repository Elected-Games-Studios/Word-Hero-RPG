using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> backgroundMusic;
    [SerializeField]
    private AudioClip victoryFanfare;
    [SerializeField]
    private AudioClip defeatDirge;

    public static CombatAudioManager instance;
    private AudioSource bgAudio;
    private AudioSource altAudio;
    private float currentMusicTime;
    public bool victory;
    private bool fadein;
    private bool fadeout;
    private float fadeTime;
    private void Awake()
    {
        if (!instance)
            instance = this;
        AudioSource[] tempSources = gameObject.GetComponents<AudioSource>();
        for(int i = 0; i < tempSources.Length; i++)
        {
            if (i == 0)
                bgAudio = tempSources[i];
            else
                altAudio = tempSources[i];           
        }
    }

    private void Update()
    {

        if (fadein)
        {
            if(fadeTime <= 1)
            {
                fadeTime += Time.deltaTime;
                bgAudio.volume = fadeTime;
            }
            else
            {
                altAudio.Stop();
                fadein = false;
            }
        }

        if (fadeout)
        {
            if(fadeTime <= 1)
            {
                fadeTime += Time.deltaTime;
                bgAudio.volume = 1 - fadeTime;
                altAudio.volume = fadeTime;
            }
            else
            {
                bgAudio.Stop();
                fadeout = false;
            }

        }
    }
    
    
    public void AudioFadeIn()
    {
        if (bgAudio.clip != backgroundMusic[GameMaster.Region])
        {
            bgAudio.clip = backgroundMusic[GameMaster.Region];
            currentMusicTime = 0;
        }
        bgAudio.time = currentMusicTime;
        bgAudio.volume = 0;        
        altAudio.Stop();
        bgAudio.Play();
        fadeTime = 0;
        fadein = true;
    }

    public void LevelEndMusic()
    {        
        if(victory)
        {
            altAudio.clip = victoryFanfare;
        }
        else
        {
            altAudio.clip = defeatDirge;
        }

        altAudio.volume = 0;
        altAudio.Play();
        fadeTime = 0;
        fadeout = true;

        currentMusicTime = bgAudio.time + 1f;
    }

    public void StopMusic()
    {
        bgAudio.Stop();
        altAudio.Stop();
        currentMusicTime = 0;
    }
}
