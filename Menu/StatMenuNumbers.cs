using UnityEngine.UI;
using UnityEngine;

public class StatMenuNumbers : MonoBehaviour
{
    public Text dmgLvl;
    public Text dmgBonus;
    public Text hpLvl;
    public Text hpBonus;
    public Text speedLvl;
    public Text speedBonus;
    public Text evadeLvl;
    public Text evadeBonus;
    public Text luckLvl;
    public Text luckBonus;
    public Text skillPoints;

    PlayerStats stats;

    private void Awake()
    {
        stats = PlayerStats.Instance;
    }
    void Update()
    {
        DisplayStats();
    }

    void DisplayStats()
    {
        skillPoints.text = ("Skill Points: " + stats.skillPoints.ToString());
        dmgLvl.text = ("Level: " + stats.pDmgLvl.ToString());
        dmgBonus.text = ("Bonus: +" + stats.playerDamage.ToString());
        hpLvl.text = ("Level: " + stats.pHpLvl.ToString());
        hpBonus.text = ("Bonus: +" + stats.playerHealth.ToString());
        speedLvl.text = ("Level: " + stats.pSpLvl.ToString());
        speedBonus.text = ("Bonus: +" + stats.playerSpeed.ToString());
        evadeLvl.text = ("Level: " + stats.pEvLvl.ToString());
        evadeBonus.text = ("Bonus: +" + stats.playerEvade.ToString() + "%");
        luckLvl.text = ("Level: " + stats.pLkLvl.ToString());
        luckBonus.text = ("Bonus: +" + stats.playerLuck.ToString() + "%");
    }
}
