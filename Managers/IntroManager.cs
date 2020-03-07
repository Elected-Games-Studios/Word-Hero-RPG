using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject regionBubbles;
    GameObject selectedDialogueSequence;
    public List<GameObject> whoYoureTalkingTo;
    public List<GameObject> dialogueBubbles;
    public List<AudioClip> bgMusic;
    public List<GameObject> backgrounds;

    PlayerStats stats;
    int tutorialProgress;
    int bubbleCount;

    // Start is called before the first frame update
    void Awake()
    {
        stats = PlayerStats.Instance;
        for(int i = 0; i < regionBubbles.transform.childCount; i++)
        {
            regionBubbles.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < whoYoureTalkingTo.Count; i++)
        {
            whoYoureTalkingTo[i].SetActive(false);
        }
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].SetActive(false);
        }
        whoYoureTalkingTo[stats.regionLoad-1].SetActive(true);
        backgrounds[stats.regionLoad - 1].SetActive(true);
        selectedDialogueSequence = regionBubbles.transform.GetChild(stats.regionLoad - 1).gameObject;
        selectedDialogueSequence.SetActive(true);
        bubbleCount = selectedDialogueSequence.transform.childCount;
        for(int i = 0; i < bubbleCount; i++)
        {
            dialogueBubbles.Add(selectedDialogueSequence.transform.GetChild(i).gameObject);
        }
        Camera.main.GetComponent<AudioSource>().clip = bgMusic[stats.regionLoad - 1];
        Camera.main.GetComponent<AudioSource>().Play();
        dialogueBubbles[0].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            NextDialogue();
        }
    }
    void NextDialogue()
    {
        tutorialProgress++;
        Dialogue();
    }

    void Dialogue()
    {
        if(tutorialProgress >= bubbleCount)
        {
            if(stats.regionProgress == stats.regionLoad && stats.levelProgress == 0)
            {
                stats.levelProgress++;
                stats.healthPU += 2;
                stats.secondWindPU += 1;
                stats.timePU += 2;
                stats.shieldPU += 5;
                stats.playerGold += 300;
            }
            stats.levelSelect = 1;
            PlayServices.Instance.SaveData();
            if(GameObject.FindGameObjectWithTag("Music"))
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().FadeIn();
            }
            SceneManager.LoadScene(2);
            return;
        }

        for (int i = 0; i < dialogueBubbles.Count; i++)
        {
            dialogueBubbles[i].SetActive(false);
        }
        dialogueBubbles[tutorialProgress].SetActive(true);
        if (dialogueBubbles[tutorialProgress].name.Contains("Who You're talking to"))
        {
            if(tutorialProgress < bubbleCount)
            {
                whoYoureTalkingTo[stats.regionLoad].GetComponent<Animator>().SetTrigger("talk");
            }
            else
            {
                whoYoureTalkingTo[stats.regionLoad].GetComponent<Animator>().SetTrigger("point");
            }

        }

    }
}
