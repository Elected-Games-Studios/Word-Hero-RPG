using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public void Reset()
    {
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().FadeIn();
        }
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        PlayerStats.Instance.levelSelect++;
        Debug.Log("On Level " + PlayerStats.Instance.levelSelect);
        Debug.Log("Region Load = " + PlayerStats.Instance.regionLoad);
        Debug.Log("Region Progress = " + PlayerStats.Instance.regionProgress);
        if (PlayerStats.Instance.levelSelect > 25)
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().StartMusic();
            if (PlayerStats.Instance.regionLoad < PlayerStats.Instance.regionProgress)
            {
                PlayerStats.Instance.regionLoad++;
                PlayerStats.Instance.levelSelect = 0;
                SceneManager.LoadScene(5);
            }
            else
            {
                PlayerStats.Instance.levelSelect = 1;
                SceneManager.LoadScene(2);
            }

        }
        else
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<CombatMusic>().StartMusic();
            SceneManager.LoadScene(2);
        }
    }

    public void EndlessReset()
    {
        SceneManager.LoadScene(3);
    }
}
