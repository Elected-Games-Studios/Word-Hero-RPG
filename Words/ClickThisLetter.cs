using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class ClickThisLetter : MonoBehaviour
{

    bool hasBeenClicked;                          //checks to see if this letter has been selected yet. If it has, you can't select it again until "currentWord" is cleared on check.
    LineRenderer line;
    AudioSource lineAudio;
    LineSounds dings;
    public bool hasLeftLetter;

    private void Awake()
    {
        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineRenderer>();
        lineAudio = line.gameObject.GetComponent<AudioSource>();
        dings = line.gameObject.GetComponent<LineSounds>();
    }
    void Update()
    {
        //if currentWord does not have a value yet, you know this letter has not been clicked. set bool to false.
        if(WordManager.currentWord == "")
        {
            hasBeenClicked = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    public void Clicked()
    {
        if (!hasBeenClicked) //if you have not clicked this letter yet...
        {
            lineAudio.clip = dings.dings[WordManager.lettersUsed];
            lineAudio.Play();
            line.positionCount = WordManager.lettersUsed + 1;
            line.SetPosition(WordManager.lettersUsed, gameObject.transform.position - new Vector3(0, -1, 4.5f));
            WordManager.lettersUsed++;
            
            WordManager.currentWord += gameObject.GetComponent<Text>().text;        //add it to the currentWord you are spelling
            hasBeenClicked = true;       //this letter has been selected and will be available again once currentWord is cleared/checked by the button.

            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            WordManager.lastLetterClicked.Add(gameObject);
            hasLeftLetter = false;
        }
        else if (hasBeenClicked && WordManager.lastLetterClicked[WordManager.lastLetterClicked.Count-1] == gameObject && hasLeftLetter)
        {
            hasBeenClicked = false;
            line.positionCount -= 1;
            WordManager.currentWord = WordManager.currentWord.Substring(0, WordManager.currentWord.Length - 1);
            WordManager.lettersUsed -= 1;
            lineAudio.clip = dings.dings[WordManager.lettersUsed];
            lineAudio.Play();

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            WordManager.lastLetterClicked.Remove(gameObject);
        }

    }
}
