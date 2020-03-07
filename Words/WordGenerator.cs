using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour
{
    private List<string> wordList = new List<string>();
    public int numberOfWords = 5;
    public string FileName = "Tutorial.txt";
    public StreamWriter fileWriter = null;
    public StreamReader fileReader = null;
    public Dictionary dict;

    public void GenerateWords()
    {
        Debug.Log("Begin Writing...");
        FileName = "Indexed5s.txt";
        //SaveSet();
        Debug.Log("WordList Generated");
    }
    
    //public void SaveSet()
    //{
    //    Debug.Log("Writing...");
    //    string path = "Assets/Scripts/Worlds/";
    //    string fileName = path + FileName;

    //    fileWriter = File.CreateText(fileName);
    //    //Debug.Log("File Created");
    //    for (int i = 0; i < 150; i++)
    //    {
    //        //Debug.Log("HP = " + i);
    //        if (File.Exists(fileName))
    //        {
    //            Debug.Log("Writing Line...");
    //            string word = dict.GetWord(5);
    //            Debug.LogError("HP = " + i + " ---- Word = " + word);
    //            if (word != " ")
    //            {
    //                fileWriter.WriteLine(word);
    //                Debug.LogError("Line Written");
    //            }
    //        }
    //    }
    //    fileWriter.Close();
    //}
}
