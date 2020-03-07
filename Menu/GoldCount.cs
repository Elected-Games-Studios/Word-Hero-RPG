using UnityEngine;
using UnityEngine.UI;

public class GoldCount : MonoBehaviour
{
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = PlayerStats.Instance.playerGold.ToString() + "g";
    }
}
