using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VicPanelFunc : MonoBehaviour
{
    public float yinput;
    private void OnEnable()
    {
        yinput = 2000f;
        CombatLogic.isGameplay = false;
        transform.localPosition = new Vector3(0, yinput, 0);
        LeanTween.moveY(gameObject, 1f, 2f);
    }
    public void StartNextLevel()
    {
        GameMaster.Level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CombatLogic.isGameplay = true;
    }
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CombatLogic.isGameplay = true;
    }
    public void OpenMenuScene()
    {
        SceneManager.LoadScene(1);
        CombatLogic.isGameplay = true;
    }

}
