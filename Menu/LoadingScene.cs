using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    PlayerStats stats;
    public Text tooltip;
    string[] tooltips;

    // Start is called before the first frame update
    void Start()
    {
        tooltips = new string[16];
        stats = PlayerStats.Instance;
        tooltips[0] = "The Strength stat increases the damage done by 1. This is added after the word length damage multiplier.";
        tooltips[1] = "The HP stat increases your health. This is important at higher levels when enemies deal more damage.";
        tooltips[2] = "The Speed stat increases the time between enemy attacks by 0.5 sec. This gives you more time to find words.";
        tooltips[3] = "The Luck stat gives you a percent chance to dodge the enemy's attack. This show of skill also increases you XP multiplier for the fight.";
        tooltips[4] = "The Crit stat gives you a percent chance to land a critical hit dealing 2x damage. This show of skill also increases you XP multiplier for the fight.";
        tooltips[5] = "Spelling a word replenishes your timer. The longer the word, the more time you get back.";
        tooltips[6] = "The Slow Time powerup doubles the time you have between attacks until that enemy is defeated.";
        tooltips[7] = "The Recycle powerup empties the list of words you've used so you can spell them again. Use this if you're stuck on a level.";
        tooltips[8] = "The Health powerup heals you completely. The more points you have in health, the more powerful this is.";
        tooltips[9] = "The Shield powerup blocks the next enemy attack. These are really inexpensive and a good alternative to a health potion if you're close to winning.";
        tooltips[10] = "At level 10, 25, 50 and 100 your hero can class up for new art, animation and stat bonuses. (Tier 2-4 available in future releases.)";
        tooltips[11] = "If you decide you don't like your build or you want to try a different hero, you can respec your class on the stat page. The cost is 25 gold per hero level.";
        tooltips[12] = "Once you reach Red Peak you can play Endless Mode. Once per day endless mode will reward you with gold and free items based on how many enemies you can defeat.";
        tooltips[13] = "Every enemy has health associated with how many words can be spelled. You don't need to find every one. When there are more letters, that's a lot of words!";
        tooltips[14] = "Spelling words adds an XP multiplier to the enemy you're fighting. The longer the words you spell the more XP you'll get from that enemy.";
        tooltips[15] = "When your mana bar is full, you get a free special attack to use. This deals a lot of damage and also increases you XP multiplier.";
        //tooltips[16] = "";
        //tooltips[17] = "";
        //tooltips[18] = "";
        //tooltips[19] = "";
        //tooltips[20] = "";
        //tooltips[21] = "";
        //tooltips[22] = "";
        //tooltips[23] = "";
        //tooltips[24] = "";
        //tooltips[25] = "";
        //tooltips[26] = "";
        //tooltips[27] = "";
        tooltip.text = tooltips[Random.Range(0, tooltips.Length)];
        Invoke("Loading", 6f);
    }

    void Loading()
    {
        if(stats.levelProgress == 0)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            SceneManager.LoadScene(stats.regionLoad + 5);
        }

    }
}
