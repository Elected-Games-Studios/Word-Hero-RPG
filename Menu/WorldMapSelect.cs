using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class WorldMapSelect : MonoBehaviour
{
    public List<GameObject> regionButtons;
    public List<GameObject> glowLevel;
    public GameObject fadeToBlack;
    public GameObject regionOneSelect;
    public GameObject regionTwoSelect;
    public GameObject regionThreeSelect;
    public GameObject worldMap;
    public GameObject titleBackButton;
    public AudioClip mapOpen;
    public AudioClip mapClose;
    public AudioClip levelStart;
    public AudioSource mapMusic;
    public GameObject map;
    AudioSource mapAudio;
    Animator openRegionAnim;
    Animator worldMapAnim;

    private void Start()
    {
        worldMapAnim = worldMap.GetComponent<Animator>();
        mapAudio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        //if (PlayerStats.Instance.regionProgress == 0)
        //    PlayerStats.Instance.regionProgress = 1;
        for (int i = 0; i<regionButtons.Count; i++)
        {  
            if(i<PlayerStats.Instance.regionProgress)
            {
                regionButtons[i].SetActive(true);
            }
            else
            {
                regionButtons[i].SetActive(false);
            }
            glowLevel[i].SetActive(false);
        }
        OpenRegions();
    }
    #region buttons
    public void WorldMapOpen()
    {
        //Debug.Log("I want the map at " + mapSpots[PlayerStats.Instance.regionProgress - 1].ToString());
        if(PlayerStats.Instance.regionProgress > 0)
        {
            worldMapAnim.SetBool("open", true);
            titleBackButton.SetActive(true);
            mapAudio.clip = mapOpen;
            mapAudio.Play();
        } 
        else
        {
            fadeToBlack.SetActive(true);
            Invoke("LaunchTutorial", 1.5f);
            return;
        }
        glowLevel[PlayerStats.Instance.regionProgress - 1].SetActive(true);
    }

    public void LaunchTutorial()
    {

        SceneManager.LoadScene(4);
    }
    public void WorldMapClose()
    {
        for (int i = 0; i < regionButtons.Count; i++)
        {
            glowLevel[i].SetActive(false);
        }
        worldMapAnim.SetBool("open", false);
        mapAudio.clip = mapClose;
        mapAudio.Play();
    }

    public void Region1()
    {
        regionOneSelect.SetActive(true);
        openRegionAnim = regionOneSelect.GetComponent<Animator>();
        openRegionAnim.SetBool("open", true);
        PlayerStats.Instance.regionLoad = 1;
        titleBackButton.SetActive(false);
        mapAudio.clip = mapOpen;
        mapAudio.Play();
    }

    public void Region2()
    {
        regionTwoSelect.SetActive(true);
        openRegionAnim = regionTwoSelect.GetComponent<Animator>();
        openRegionAnim.SetBool("open", true);
        PlayerStats.Instance.regionLoad = 2;
        titleBackButton.SetActive(false);
        mapAudio.clip = mapOpen;
        mapAudio.Play();
    }

    public void Region3()
    {
        regionThreeSelect.SetActive(true);
        openRegionAnim = regionThreeSelect.GetComponent<Animator>();
        openRegionAnim.SetBool("open", true);
        PlayerStats.Instance.regionLoad = 3;
        titleBackButton.SetActive(false);
        mapAudio.clip = mapOpen;
        mapAudio.Play();
    }

    public void CloseRegion()
    {
        regionOneSelect.SetActive(false);
        regionTwoSelect.SetActive(false);
        regionThreeSelect.SetActive(false);
        openRegionAnim.SetBool("open", false);
        titleBackButton.SetActive(true);
        mapAudio.clip = mapClose;
        mapAudio.Play();
    }

    public void LevelSelect(int level)
    {
        fadeToBlack.SetActive(true);
        PlayerStats.Instance.levelSelect = level;
        Camera.main.gameObject.GetComponent<AudioSource>().Stop();
        mapAudio.clip = levelStart;
        mapAudio.Play();
        SceneManager.LoadScene(2);
    }

    public void OpenRegions()
    {
        for (int i = 0; i < regionButtons.Count; i++)
        {
            if (i < PlayerStats.Instance.regionProgress)
            {
                regionButtons[i].SetActive(true);
            }
            else
            {
                regionButtons[i].SetActive(false);
            }
        }
    }
    #endregion
    
}
