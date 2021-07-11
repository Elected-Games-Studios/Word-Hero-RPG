using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonManager : MonoBehaviour
{
    public GameObject upButton;
    public GameObject downButton;
    public Image diffSwish;
    public GameObject[] colorTints = new GameObject[3];
    public GameObject[] difficultyIcons = new GameObject[3];
    int currentDiff;

    private void Awake()
    {
        currentDiff = 0;
        LevelManager.instance.SetDifficulty(0);
        downButton.SetActive(false);

    }
    public void IncreaseDifficulty()
    {
        currentDiff++;
        LevelManager.instance.SetDifficulty(currentDiff);
        if(currentDiff >=2)
        {
            upButton.SetActive(false);
        }
        if (currentDiff >= 1)
        {
            downButton.SetActive(true);
        }
        ColorSwish(currentDiff);
        Debug.Log("Increased Difficulty to: " + currentDiff);
    }

    public void DecreaseDifficulty()
    {
        currentDiff -= 1;
        LevelManager.instance.SetDifficulty(currentDiff);
        if (currentDiff <=0)
        {
            downButton.SetActive(false);
        }
        if (currentDiff <=1)
        {
            upButton.SetActive(true);
        }
        ColorSwish(currentDiff);
        Debug.Log("Decreased Difficulty to: " + currentDiff);
    }

    public void ColorSwish(int currentDifficulty)
    {
        diffSwish.gameObject.SetActive(true);
        Color swishColor = new Color();
        switch (currentDifficulty)
        {
            case 0:
                swishColor = Color.green;
                break;
            case 1:
                swishColor = Color.yellow;
                break;
            case 2:
                swishColor = Color.red;
                break;
        }
        for (int i = 0; i < 3; i++)
        {
            if (i == currentDiff)
            {
                colorTints[i].SetActive(true);
                difficultyIcons[i].SetActive(true);
            }
            else
            {
                colorTints[i].SetActive(false);
                difficultyIcons[i].SetActive(false);
            }
        }
        diffSwish.color = swishColor;
        diffSwish.transform.localPosition = new Vector2(0, -3000);
        Debug.Log("Changed Swish Color to: " + swishColor);
        LeanTween.moveLocalY(diffSwish.gameObject, 3000f, .5f).setEaseInOutSine().setOnComplete(TurnOffSwish);
    }

    private void TurnOffSwish()
    {

        diffSwish.gameObject.SetActive(false);
    }
}
