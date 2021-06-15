using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSet : MonoBehaviour
{
    public int desiredLevel; //Unity Editor Input them as 1-25 for ease of use, but back end uses index at 0, so this becomes -1
    LevelManager levelMan;
    Button menuButton;
    [SerializeField]
    private openPanel confirmLevelSelectionPanel;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        levelMan = LevelManager.instance;
        confirmLevelSelectionPanel.OpenLevelStartPanel();
    }

    void OnButtonClicked()
    {
        levelMan.SetLevel(desiredLevel - 1);
    }
}