using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class GameWordHandler
{
   


    

    public static void Shuffle()
    {

        List<string> tempList = wordsUsed;
        string tempWord = tempList[(tempList.Count - 1)];
        tempList = new List<string>();
        for (int x = 0; x < tempWord.Count(); x++) tempList.Add(tempWord[x].ToString());
        int counter = wordsUsed.Count;
        for (int x = 0; x < counter; x++)
        {
            int remover = UnityEngine.Random.Range(0, tempList.Count);
            string tempLetter = tempList[remover];
            tempList.RemoveAt(remover);
            //your code here
        }
    }

    public static void WordBreak(int currentword)
    {
        wordsUsed = new List<string> { };
        string[] tempArrString = Words[currentword].Split(',');
        wordsUsed = tempArrString.ToList();
        wordsUsed.RemoveAt(0);
    }
}

