using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LocalSaveEngine
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.stats";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, SaveManager.SaveParse());
        stream.Close();
    }

    public static void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerData.stats";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string data = formatter.Deserialize(stream) as string;
            stream.Close();
            Debug.Log("Data Opened");
            SaveManager.LoadSplit(data);
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            SaveManager.LoadSplit(" # # ");
        }
    }
}
