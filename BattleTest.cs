using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    public PlayerCombat combat;
    public int hero;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        combat.playerHealth = 10;
        
    }

    public void Attack1()
    {
        combat.Attack(3);
    }
    public void Attack2()
    {
        combat.Attack(4);
    }
    public void Attack3()
    {
        combat.Attack(5);
    }
    public void Attack4()
    {
        combat.Attack(6);
    }
    public void Attack5()
    {
        combat.Attack(7);
    }
    public void Attack6()
    {
        combat.Attack(8);
    }

    public void NextHero()
    {
        for (int i = 0; i < combat.gameObject.transform.childCount; i++)
        {
            combat.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        combat.gameObject.transform.GetChild(hero).gameObject.SetActive(true);
        combat.hero = combat.gameObject.transform.GetChild(hero).gameObject;
    }
}
