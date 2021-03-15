using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VicPanelFunc : MonoBehaviour
{
    public float yinput;

    [SerializeField]
    private CombatLogic combatLogic;
    private void OnEnable()
    {
        yinput = 2000f;
        combatLogic.isGameplay = false;
        PlayServices.Instance.SaveData();
        transform.localPosition = new Vector3(0, yinput, 0);
        LeanTween.moveY(gameObject, 1f, 2f);
    }
    public void StartNextLevel()
    {
        GameMaster.Level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        this.gameObject.SetActive(false);

    }
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void OpenMenuScene()
    {
        SceneManager.LoadScene(1);

    }

}
