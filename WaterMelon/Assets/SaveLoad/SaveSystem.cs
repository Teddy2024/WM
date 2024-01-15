using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore(ScoreRecord scoreRecord)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData(scoreRecord);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadScore()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }
}
