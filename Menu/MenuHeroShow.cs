using UnityEngine;

public class MenuHeroShow : MonoBehaviour
{
    PlayerStats stats;
    GameObject hero;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.Instance;
        HeroSelect();
    }

    public void HeroSelect()
    {
        Debug.Log("Selecting Hero");
        hero = null;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        Debug.Log("Heroes reset");
        gameObject.transform.GetChild(stats.activeHero).gameObject.SetActive(true);
        Debug.Log("Hero set to " + gameObject.transform.GetChild(stats.activeHero).gameObject.name);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                hero = gameObject.transform.GetChild(i).gameObject;
                Debug.Log("Hero is " + hero.name);
            }
        }
        if (GameObject.FindGameObjectWithTag("Manager"))
        {
            Debug.Log("You have a mnanager?");
            GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>().heroName = hero.name;
        }

        anim = hero.GetComponent<Animator>();
        Debug.Log("Got Animator");
    }
}
