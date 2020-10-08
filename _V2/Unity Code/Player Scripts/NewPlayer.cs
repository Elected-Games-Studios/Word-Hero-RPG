using System;
using UnityEngine;

public class NewPlayer : Player
{
        public void Start()
        {
            //Set Stats now
            //normally you would pull from a db but for now it's static
            Lvl = 1;
            Str = 10;
            Hlt = 200;
            PlayerSelect( 1);
        }
        public void Update()
        {
// any get functions could go here
        }
        
        
    }