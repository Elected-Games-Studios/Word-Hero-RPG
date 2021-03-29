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
    private static string serverJunk = "";
    private static List<char> specList = new List<char> { '=', '+', '.' };
    #endregion

    #region publicVariables
    //If you are reading this send monster.
    public static bool isGameReady = false;
    public static int pRank = 0;
    public static int ranks = 0;
    public static string pName = "";
    public static int pRep = 0;
    public static string[,] leadBoards = new string[50,2] { };
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

        if (_sC.Count < 1 && _sS.Count < 1)
        {
            return false;
        }
        else if(_sS.Count>0&&_sC<0)
        {
            //this will throw debug log stating that there is a string that needs to be split but no command from the server that can tell it to be split and will store in the server junk string at the top labeled junk.
            for (int x = 0; x < _sS.Count; x++)
            {
                serverJunk += _sS[x];
            }
            Debug.log("Server sent a string without a command.");

            return false;

        }
        else
        {
            //logic for seperating the commands and strings into the variables that are needed for the scene.

            for (int x = 0; x < _sC.Count; x++)
            {
                if (commandSplit(_sC[x]))
                {
                    _sC = serverStringSplit(_sS, _sC[x]);

                }
            }

            return true;
        }

    }

    private static List<string> serverStringSplit(List<string> _input, int _dType)
    {
        //all data must be removed when done, made a temp then replaced the input with temp.
        _dType -= 181;
        for (int x = 0; x < _input.count; x++)
        {
            if (_input.Contains(specList[_dType]))
            {/////////////////////////////////
                addDataString(_input[x], _dType);
            }////////////////////////////////
        }
        
    }

    private static void addDataString(string _input, int _dType)
    {
        string[] tempStr = _input.Split('#');
        switch (_dType)
        {
            case 1:
                if (tempStr.Length > 2)
                {
                    pRank = Convert.ToInt32(tempStr[0]);
                    pRep = Convert.ToInt32(tempStr[1]);
                    ranks = Convert.ToInt32(tempStr[2]);   
                }
                else Debug.log("Server (001): Server Did not send full string, missing arguements.");
                break;
            case 2:
                if (tempStr.Length > 0) fillMyBoard(tempStr);
                else Debug.log("Server (002): Server Did not send full string, missing arguements.");
                break;
            default:
                Debug.log("Client (001): There was a command that was not meant to go here.");
        }
    }
    private static void fillMyBoard(string[] _data)
    {
        if (_data.Length < 2)
        {
            Debug.log("Server (003): Server Did not send full string, missing arguements.");
            _data = new string[1] { "" };
        }
        int stepC = 0;
        int fluctC = 0;
        for (int x = 0; x < (_data.Length-1); x++)
        {
            if (stepC == 1)
            {
                leadBoards[fluctC, stepC] = Convert.ToInt32(_data);
                fluctC++;
                stepC = 0;
            }
            else 
            {
                leadBoards[fluctC, stepC] = Convert.ToInt32(_data);
                stepC++;
            }
        }    
    }
    private static bool commandSplit(int _dType)
    {
        switch (_dType)
        {
            case 182:
                return true;
            case 183:
                return true;
            default:
                return false;
        }
    }
    

   
    //load sequencing needs to be done.
}