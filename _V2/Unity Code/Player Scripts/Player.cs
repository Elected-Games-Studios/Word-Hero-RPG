using System;
using System.Diagnostics;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField]
    protected Transform shootPoint;
    public long Str { get; set; }
    public long Lvl { get; set; }    
    public long Hlt { get; set; }
    public enum PlayerType {Folk, Mage, Ranger, Archer, Rogue }

    public static void PlayerSelect(int selection)
    {
        switch (selection)
        {
            case 1:
                //PlayerType.Folk;
                break;
            case 2:
                //PlayerType.Mage;
                break;
            case 3:
                //i guess
                break;
            case 4:
                //not sure
                break;
            case 5:
                //will fill in later
                break;
            default:
                Console.WriteLine("default case");
                break;
        }
    }
    //handle turret level prefab variants
        //you'll have to actually make an enum class for this
}