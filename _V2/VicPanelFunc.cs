using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VicPanelFunc : MonoBehaviour
{

    private void OnEnable()
    {
        CombatLogic.isGameplay = false;

    }
    public void StartNextLevel()
    {
        GameMaster.Level++;
        Debug.Log("level set to: " + GameMaster.Level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CombatLogic.isGameplay = true;
    }
    public void RestartCurrentLevel()
    {
        Debug.Log("level still: " + GameMaster.Level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CombatLogic.isGameplay = true;
    }
    public void OpenMenuScene()
    {
        SceneManager.LoadScene(1);
        CombatLogic.isGameplay = true;
    }

}
