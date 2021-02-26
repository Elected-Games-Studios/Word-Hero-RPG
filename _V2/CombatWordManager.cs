using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class CombatWordManager
{
    public static List<int> WordsIndex = new List<int>();
    public static List<string> Words = new List<string> { };// List of string arrays of all words in the level
    public static List<string> currentUsableWords;//This is the string list, just Comma separated subwords of a single word
    public static List<string> wordsSpelled;//This is string array of completed subwords
    public static int enemyHealth;
	public static string checkString = "";
    public static List<string> shuffledWord = new List<string>{ };
    public static string longestWord = "";
    public static event Action<int> onMaxLengthFound;
    public static event Action onUpdateString;
    public static event Action<int> onCorrectWord;
    public static event Action playerKilledTrigger;
    public static event Action onIncorrectWord;
    

    public static void StartLevel() //called on awake for 
    {
        WordsIndex = WordDatav2.GetDataLevel(GameMaster.Region, GameMaster.Level, GameMaster.Difficulty);
        Words = new List<string> { };
        //WordsIndex = WordDatav2.GetDataLevel(0,0,0);
        GetActualWords();
        wordBreak(0);
        InitializeLetters();
        onMaxLengthFound?.Invoke(longestWord.Count());
        
    }

    public static void InitializeLetters()
	{
		List<string> tempList = currentUsableWords;    
        int i = 0;
        while(tempList[i].Count() != 5)//<<needs to be changed to global dictionary value
        {
            i++;
        }
        longestWord = tempList[i];
        
        Shuffle();	
	}

    public static void Shuffle()
    {
        shuffledWord.Clear();
        List<string> tempList = new List<string> { };
        for (int x = 0; x < longestWord.Count(); x++) tempList.Add(longestWord[x].ToString());
        int counter = longestWord.Count();

        for (int x = 0; x < counter; x++)
        {
            int remover = UnityEngine.Random.Range(0, tempList.Count());
            string tempLetter = tempList[remover];
            tempList.RemoveAt(remover);

            shuffledWord.Add(tempLetter);
           
        }
    }

    private static void GetActualWords()
    {

        int tempcount = WordsIndex.Count();
        tempcount = tempcount / 2;
        for (int x = 0; x < tempcount; x++)
        {
            Words.Add(Convert.ToString(WordDatav2.GetWords(WordsIndex[0], WordsIndex[1 + x * 2])));
        }
    }

    public static void wordBreak(int currentword)
	{
        wordsSpelled = new List<string> { };
        currentUsableWords = new List<string> { };
		string[] tempArrString = Words[currentword].Split(',');
		currentUsableWords = tempArrString.ToList();
        string [] frontNums = currentUsableWords[0].Split('/');
        //enemyHealth = Convert.ToInt32(frontNums[0]);
		currentUsableWords.RemoveAt(0); 
    }
	private static bool checkAdd()
	{
		for(int x = 0; x < currentUsableWords.Count(); x++)
        {
			if (currentUsableWords[x].ToUpper().Equals(checkString.ToUpper())) return true;
        }
		return false;
	}
	public static void checkWord() // OnFingerUp will pack your string and call your attack 
	{
		if (checkAdd())
		{
			wordsSpelled.Add(checkString);
			currentUsableWords.Remove(checkString.ToLower());
            onCorrectWord?.Invoke(checkString.Count());
			resetString();
		}
        else
        {
			resetString();
            onIncorrectWord?.Invoke();
		}
	}
	public static void addToString(string c) 
	{
        checkString += c;
        onUpdateString?.Invoke();
	}
	public static void removeString() // if you want to walk back the the line you can call this and it will remove it 
	{
		checkString = checkString.Remove(checkString.Length-1);
        onUpdateString?.Invoke();
    }
	public static void resetString()
	{
		checkString = "";
        onUpdateString?.Invoke();
    }
    public static void GameOverTrigger()
    {
        playerKilledTrigger?.Invoke();
    }
}