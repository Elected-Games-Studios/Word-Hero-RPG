using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<GameObject> conversation;
    public GameObject world;
    public GameObject senpai;
    public GameObject player;
    public GameObject background;
    public GameObject skeleton;
    public GameObject combatCanvas;
    public GameObject manager;

    public bool hold;
    int scene;

    private void Awake()
    {
        
    }

    public void NextScene()
    {
        scene++;
        conversation[scene].SetActive(true);
        conversation[scene-1].SetActive(false);

        if(scene == 9)
        {
            world.GetComponent<AudioSource>().Play();
            Camera.main.GetComponent<AudioSource>().Stop();
            world.GetComponent<Animator>().SetTrigger("explosion");
        }

        if(scene == 11)
        {
            background.GetComponent<AudioSource>().Play();
            senpai.GetComponent<Animator>().SetBool("inCombat", true);
            player.GetComponent<Animator>().SetBool("inCombat", true);
            background.GetComponent<Animator>().SetBool("inCombat", true);
            skeleton.GetComponent<Animator>().SetTrigger("runIn");
            skeleton.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        }

        if(scene == 12)
        {
            combatCanvas.SetActive(true);
            manager.SetActive(true);
            //hold = true;
        }
        if(scene == 13)
        {
            hold = true;
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && !hold)
        {
            NextScene();
        }

    }
}
