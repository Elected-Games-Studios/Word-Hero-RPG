using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSet : MonoBehaviour
{
    public int desiredLevel;
    LevelManager levelMan;
    Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        levelMan = LevelManager.instance;
    }

    void OnButtonClicked()
    {
        levelMan.SetLevel(desiredLevel);
    }
}