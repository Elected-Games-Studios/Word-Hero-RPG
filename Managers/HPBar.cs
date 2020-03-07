using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Text hpText;
    Slider hpSlider;

    private void Awake()
    {
        hpText = GetComponentInChildren<Text>();
        hpSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        hpText.text = (hpSlider.value + "/" + hpSlider.maxValue);
    }

}
