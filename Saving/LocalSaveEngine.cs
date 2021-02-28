using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LocalSaveEngine
{
    public static void SavePlayer()//PlayerStats player)
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/playerData.stats";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //SavedVariables data = new SavedVariables(player);

        //formatter.Serialize(stream, data);

        //stream.Close();

    }

    public static void LoadPlayer()
    {
        //string path = Application.persistentDataPath + "/playerData.stats";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);
        //    SavedVariables data = formatter.Deserialize(stream) as SavedVariables;
        //    stream.Close();
        //    //Debug.Log("Data Opened");
        //    return data;
        //}
        //else
        //{
        //    Debug.LogError("Save file not found in " + path);
        //    return null;
        //}
    }
}
