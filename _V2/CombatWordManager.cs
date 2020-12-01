using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class CombatWordManager
{
    private static List<int> WordsIndex;
    public static List<string> Words;// List of string arrays of all words in the level
    public static List<string> wordsUsed;//This is the string array, just Comma separated subwords of a single word
	public static List<string> wordsDid;//This is string array of completed subwords
	public static string checkString;

    public static void StartLevel() //called on awake for 
    {
        WordsIndex = WordData.GetDataLevel(GameMaster.Region, GameMaster.Level, GameMaster.Difficulty);
        GetActualWords();
    }

    public static void Shuffle() //KYLE: modify to find longest word and return some form of the letters of that word to populate the circle
	{
		List<string> tempList = wordsUsed;
		string tempWord = tempList[(tempList.Count() - 1)];//I have questions about how this works 
		tempList = new List<string> { };
		for (int x = 0; x < tempWord.Count(); x++) tempList.Add(tempWord[x].ToString());
		int counter = wordsUsed.Count();
		for (int x = 0; x < counter; x++)
		{
			int remover = UnityEngine.Random.Range(0, tempList.Count());
			string tempLetter = tempList[remover];
			tempList.RemoveAt(remover);
			//your code here
		}
	}

    private static void GetActualWords()
    {
        int tempcount = WordsIndex.Count();
        tempcount = tempcount / 2;
        for (int x = 0; x < tempcount; x++)
        {
            Words.Add(Convert.ToString(WordData.GetWords(WordsIndex[0], WordsIndex[1 + x * 2])));
        }
    }

    public static void wordBreak(int currentword)
	{
		wordsUsed = new List<string> { };
		string[] tempArrString = Words[currentword].Split(',');
		wordsUsed = tempArrString.ToList();
       
		wordsUsed.RemoveAt(0); //add a line to modify static value for enemy health? Element 0 contains enemy health before first '/'
    }
	private static bool checkAdd()
	{
		for(int x = 0; x < wordsUsed.Count; x++)
        {
			if (wordsUsed[x].Equals(checkString)) return true;
        }
		return false;
	}
	public static void checkWord() // OnFingerUp will pack your string and call your attack 
	{
		if (checkAdd())
		{
			wordsDid.Add(checkString);
			wordsUsed.Remove(checkString);
			resetString();
		}
        else
        {
			resetString();
		}
	}
	public static void addToString(string c) // Onletterhit put gameobject letter here as string param
	{
		checkString.Insert((checkString.Count()-1),c);
	}
	public static void removeString() // if you want to walk back the the line you can call this and it will remove it 
	{
		checkString.Remove(checkString.Count() - 1);
	}
	public static void resetString()
	{
		checkString = "";
	}
}