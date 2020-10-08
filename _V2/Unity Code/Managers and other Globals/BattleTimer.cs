using UnityEngine;

public class BattleTimer : MonoBehaviour
    {
        public static float battleTimer = 1f;
// *** This could be needed by more than just the player so i struggle to keep it private or put it on the player
// *** it has to be Mono to use Update method which factors for cpu time to Real time.
// *** fixing that dependency could make this avoided if necessary 
        public void Update()
        {
            battleTimer -= Time.deltaTime;
            // still needs reset
        }
    }