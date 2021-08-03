using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> backgroundMusic;
    private AudioSource audio;
    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = backgroundMusic[GameMaster.Region];
        audio.Play();
    }
}
