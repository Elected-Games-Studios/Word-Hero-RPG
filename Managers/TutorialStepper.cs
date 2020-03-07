using UnityEngine;
public abstract class TutorialStepper : MonoBehaviour
{
    public GameObject card;
    public bool checkBool = false;
    public virtual void PauseGame()
    {
        Time.timeScale = 0;
        card.SetActive(true);
    }
    public virtual void UnPauseGame()
    {
        Time.timeScale = 1;
        card.SetActive(false);
    }
}