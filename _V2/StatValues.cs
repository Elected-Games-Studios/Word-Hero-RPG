using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatValues : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects = new List<GameObject> { };

    private void Start()
    {
        
        objects[0].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[2].ToString();//XP
        objects[1].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[4].ToString();//health
        objects[2].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[3].ToString();//dmg
        objects[3].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[6].ToString();//agi
        objects[4].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[5].ToString();//crit
        objects[5].GetComponent<Text>().text = CharectorStats.GetCharecterStats(CharectorStats.CurrentHero)[7].ToString();//def
    }
}
