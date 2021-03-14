using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LocalSaveEngine
{
    public static void SavePlayer()//PlayerStats player)
    {
        string path = Application.persistentDataPath + "/playerData.stats";
        FileStream stream = new FileStream(path, FileMode.Create);
        byte[] dataToSave = Encoding.ASCII.GetBytes(SaveManager.SaveParse());
        stream.Write(dataToSave);
        stream.Close();
    }

    public static void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerData.stats";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            byte[] data = Encoding.ASCII.GetString(stream);
            stream.Close();
            Debug.Log("Data Opened");
            SaveManager.LoadSplit(data);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
