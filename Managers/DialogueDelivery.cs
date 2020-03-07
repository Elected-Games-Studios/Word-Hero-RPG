using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDelivery : MonoBehaviour
{
    Text dialogue;
    string fullMessage;
    string displayedMessage;
    float countdown;
    int characters;

    private void Awake()
    {
        dialogue = gameObject.GetComponent<Text>();
        fullMessage = dialogue.text;
        dialogue.text = "";
        Debug.Log("Full Message: " + dialogue.text.Length);
    }

    private void Update()
    {
        //if(countdown <= 0)
        //{
            //countdown = .05f;
        if(characters < fullMessage.Length -1)
            {
                displayedMessage += fullMessage.Substring(characters, 2);
                characters += 2;
                dialogue.text = displayedMessage;
            }
        else if (characters < fullMessage.Length)
        {
            displayedMessage += fullMessage.Substring(characters, 1);
            characters += 1;
            dialogue.text = displayedMessage;
        }

        //}
        //countdown -= Time.deltaTime;
    }
}
