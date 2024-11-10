using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CardMatch
{

[System.Serializable]
public class CardData
{
    public int cardId;
    public bool cardInPlay;

    public CardData(int value, bool flag)
    {
        this.cardId = value;
        this.cardInPlay = flag;
    }
}

[System.Serializable]
public class DataArrayWrapper
{
    public CardData[] dataArray;

    public DataArrayWrapper(CardData[] dataArray)
    {
        this.dataArray = dataArray;
    }
}

public class SaveLoadManager : MonoBehaviour
{

    public static void SaveData(CardData[] dataArray)
    {
        DataArrayWrapper wrapper = new DataArrayWrapper(dataArray);
        string json = JsonUtility.ToJson(wrapper);

        string path = Application.persistentDataPath + "/gameSave.json";
        File.WriteAllText(path, json);

        // Debug.Log("Data saved to " + path);
    }

    public static CardData[] LoadData()
    {
        string path = Application.persistentDataPath + "/gameSave.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataArrayWrapper wrapper = JsonUtility.FromJson<DataArrayWrapper>(json);

            // Debug.Log("Data loaded from " + path);
            return wrapper.dataArray;
        }
        else
        {
            // Debug.LogWarning("No save file found at " + path);
            return new CardData[0]; // Return an empty array if no file exists
        }
    }
}
}