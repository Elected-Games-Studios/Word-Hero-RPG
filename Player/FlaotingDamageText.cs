using UnityEngine;
using TMPro;

public class FlaotingDamageText : MonoBehaviour
{
    Rect myRect;
    void Awake()
    {
        GetComponent<TextMesh>().text = ("-" + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().givenDamage.ToString());
        GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1f));
        Destroy(gameObject, 1);        
    }

}
