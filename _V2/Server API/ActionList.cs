using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ActionList : MonoBehaviour
{
	public static ActionList instance;

	#region PrivateVariables
	//Joe do not change to public!!!
	private static string[] ServerEvents = new string[]
		{"01","02","03","04","05","06","07","08"};
	private static string[] ServerGeneralEvents = new string[]
		{"81","82","83","84","85","86","87","88"};
	private static string[] ClientEvents = new string[]
		{"01","02","03","04","05","06"};
	private static string[] ClientOverLoadEvents = new string[]
		{"10","11","12",};
	private static string[] ClientGeneralEvents = new string[]
		{"81","82","83","84","85","86","87","88"};
	private List<string> ServerString = new List<string> { };
	private List<int> _dataTypeReturns = new List<int> { };
	private string _sendData = "";
	#endregion

	#region PublicVariables
	//Joe's variables.
	public static bool UpdateServer = new bool();
	//Kyle's variables.
	public static bool SendServerCheck = false;
	#endregion

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Debug.Log("Instance already exists, destroying object!");
			Destroy(this);
		}
	}

	#region Joe's Calls

	public void initialConnection()
	{
		AddData(Convert.ToString(ClientGeneralEvents[0]) + "-");
		SendServerCheck = true;
	}

	public void getRank()
	{
		AddData(Convert.ToString(ClientGeneralEvents[1]) + "-");
		SendServerCheck = true;
	}

	public void getLeaderBoards()
	{
		AddData(Convert.ToString(ClientGeneralEvents[2]) + "-");
		SendServerCheck = true;
	}

	public void sendAttack(int _damage, string _word)
	{
		AddData(Convert.ToString(ClientEvents[0]) + "-" + _word + ',' + Convert.ToString(_damage));
		SendServerCheck = true;
	}

	public void getNewWord()
	{
		AddData(Convert.ToString(ClientEvents[2]) + "-" + Convert.ToString(ClientOverLoadEvents[0]) + "-");
		SendServerCheck = true;
	}

	public void setWord()
	{
		AddData(Convert.ToString(ClientEvents[2]) + "-" + Convert.ToString(ClientOverLoadEvents[2]) + "-");
		SendServerCheck = true;
	}

	public void setStats(string _stats)
	{
		AddData(Convert.ToString(ClientGeneralEvents[4]) + "-" + _stats + "-");
		SendServerCheck = true;
	}

	public void readySet()
	{
		AddData(Convert.ToString(ClientGeneralEvents[3]) + "-");
		SendServerCheck = true;
	}

	public void getEnemyStats()
	{
		AddData(Convert.ToString(ClientGeneralEvents[5]) + "-");
		SendServerCheck = true;
	}
	#endregion

	#region ClientCalls


	#endregion

	#region ServerCalls

	public void AddData(string _packetData)
	{
		ServerString = ""; //This should be null
		_packetData += '_';

		string _temp = "";
		bool _catch = false;

		try
		{
			_temp = _packetData.Split('-');
		}
		catch (Exception)
		{
			Debug.Log("No Data From Server!");
			_catch = true;
		}
		for (int x = 0; x < _temp.Length && !_catch; x++)
		{
			if (_temp[x].Contains("#")) ServerString.Add(_temp[x]);
			else ServerReturn(_temp[x]);
		}
	}

	public static string OutBoundData() 
	{
		string tempStr = _sendData;
		_sendData = "";
		return tempStr;
	}

	public static List<string> ServerString()
	{
		List<string> tempStr = ServerString;
		ServerString = "";
		return tempStr;
	}

	public static List<int> ServerCommands()
	{
		List<int> tempList = _dataTypeReturns;
		_dataTypeReturns = new List<int> { };
		return tempList;
	}

	private static void ServerReturn(string _serverData)
    {
		for(int i = 0; i <ServerEvents.Length; i++)
        {
			if (_packetData.Contains(ServerEvents[i]))
				_dataTypeReturns.Add(i);
        }
		for (int i = 0; i < ServerGeneralEvents.Length; i++)
		{
			if (_packetData.Contains(ServerGeneralEvents[i]))
				_dataTypeReturns.Add(100+i);
		}
		return 0;
    }

	#endregion

}