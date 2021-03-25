using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ActionCommands : MonoBehaviour
{
    public static ActionCommands instance;

    #region privateVariables
    //I was here.
    private static int loadSeq = 0;
    private static bool loadComplete = false;
    private static List<string> LeaderBoards = new List<string> { };

    #endregion

    #region publicVariables
    //If you are reading this send monster.
    public static bool isGameReady = false;

    #endregion

    private static void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ActionList.instance.initialConnection();
            loadSeq++;
        }
        else if (instance != this)
        {
            Debug.Log("ActionCommands already exists, destroying object!");//Unique Log, should help.
            Destroy(this);
        }
    }

    private static void Update()
    {
        if (!ActionList.instance.SendServerCheck && loadSeq < 3)
        {
            initialServerReq();
        }
        else if (!ActionList.instance.SendServerCheck && loadSeq == 3)
        {
            //set all buttons and put all lists into the boards, this will be where you initialize the leaderboards.
        }
    }

    private static void initialServerReq() //this will get the data from the server so that it is there for the scene when it is done loading it will then push off to default, used a case switch so that if more commmands are needed in the future they may be added.
    {
        switch (loadSeq)
        {
            case 1:
                ActionList.instance.getRank();
                loadSeq++;  // step sequencing so that you can check with above to ensure that all of the game is loading in order.
                break;
            case 2:
                ActionList.instance.getLeaderBoards();
                loadSeq++;
                break;
            default:
                break;
        }
    }

    private static bool serverRead()
    {
        //short hand, if you want it long hand you may change.
        List<int> _sC = ActionList.instance.ServerCommands(); 
        List<string> _sS = ActionList.instance.ServerString();

        if( _sC.Count < 1 && _sS.Count < 1)
        {
            return false;
        }
        else
        {
            //logic for seperating the commands and strings into the variables that are needed for the scene.
            return true;
        }

    }

    private static void serverStringSplit(List<string> _input, int _dType)
    {
        for (int x = 0; x < _input.count; x++)
        {

        }
    }
    

   
    //load sequencing needs to be done.
}