using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class CombatWordManager
{
	public static List<string> wordsUsed;
	public static List<string> wordsDid;
	public static string checkString;
	public static void Shuffle()
	{
		List<string> tempList = wordsUsed;
		string tempWord = tempList[(tempList.count() - 1)];
		tempList = new List<string> { };
		for (int x = 0; x < tempWord.count(); x++) tempList.add(tempWord[x]);
		int counter = wordsUsed.count();
		for (int x = 0; x < counter; x++)
		{
			remover = UnityEngine.Random.Range(0, tempList.count());
			tempLetter = tempList[remover];
			tempList.RemoveAt(remover);
			//your code here
		}
	}
	public static void wordBreak(int currentword)
	{
		wordsUsed = new List<string[]> { };
		string[] tempArrString = GameWordHandler.Words[currentword].split(',');
		wordsUsed = tempArrString.ToList;
		wordsUsed.RemoveAt(0);
	}
	private static boolean checkAdd(string word)
	{
		return (!wordsUsed.Except(word).Any());
	}
	public static void checkWord()
	{
		if (checkAdd)
		{
			wordsDid.add(checkString);
			wordsUsed.Remove(wordsUsed);
			resetString();
		}
        else
        {
			resetString();
		}
	}
	public static void addToString(string c)
	{
		checkString.add(c);
	}
	public static void removeString()
	{
		checkstring.Remove(checkString.count - 1);
	}
	public static void resetString()
	{
		checkString = "";
	}
}