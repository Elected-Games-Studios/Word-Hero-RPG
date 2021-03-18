using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectorButton : MonoBehaviour
{
    public void SetEasy()
    {
        GameMaster.Difficulty = 0;
        Debug.Log("easy set");
    }
    public void SetMedium()
    {
        GameMaster.Difficulty = 1;
        Debug.Log("medium set");
    }
    public void SetHard()
    {
        GameMaster.Difficulty = 2;
        Debug.Log("hard set");
    }
}
