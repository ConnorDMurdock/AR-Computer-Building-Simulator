using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    public Dictionary<string, int> saveDataFlags;

    public bool saveDataExists;

    public string fileName;

    public int numberOfBadges;

    private string filePath;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one SaveDataManager in this scene!");
        }
        else
        {
            instance = this;
        }

        filePath = Application.persistentDataPath + "/SaveData.txt";

        try
        {
            saveDataFlags = GetSaveData();
        }
        catch
        {
            Debug.LogError("No Save Data Found!");
            saveDataExists = false;
            saveDataFlags = NewSaveData();
        }
    }
    public void CreateNewSaveFile(string name)
    {
        saveDataFlags = NewSaveData();
        SaveDataToFile(name, saveDataFlags);
        saveDataExists = true;
    }

    public string GetFileName()
    {
        return fileName;
    }

    public int GetNumberOfBadges()
    {
        return numberOfBadges;
    }

    public Dictionary<string, int> GetSaveData()
    {
        numberOfBadges = 0;
        Dictionary<string, int> saveDataFromFile = new Dictionary<string, int>();
        string line = "";
        using (StreamReader sr = new StreamReader(filePath))
        {
            fileName = sr.ReadLine();

            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
                string[] splitLine = line.Split(',');
                saveDataFromFile.Add(splitLine[0], int.Parse(splitLine[1]));
                numberOfBadges += int.Parse(splitLine[1]);
                Debug.Log(numberOfBadges);
            }
        }
        Debug.Log("Save Data Loaded!");
        saveDataExists = true;
        return saveDataFromFile;
    }

    private void SaveDataToFile(string fileNameSave, Dictionary<string, int> saveDataFlags)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(fileNameSave);

            foreach (KeyValuePair<string, int> flag in saveDataFlags)
            {
                string line = flag.Key + "," + flag.Value;
                sw.WriteLine(line);
            }
        }
        Debug.Log("Data Saved Successfully");
    }

    private Dictionary<string, int> NewSaveData()
    {
        Dictionary<string, int> newSaveData = new Dictionary<string, int>();
        newSaveData.Add("Case", 0);
        newSaveData.Add("CPU", 0);
        newSaveData.Add("RAM", 0);
        newSaveData.Add("HardDrive", 0);
        newSaveData.Add("USB", 0);
        newSaveData.Add("SSD", 0);
        newSaveData.Add("GraphicsCard", 0);
        newSaveData.Add("Cooling", 0);
        newSaveData.Add("PowerSupply", 0);
        newSaveData.Add("PluggingIn", 0);
        return newSaveData;
    }

    public void DeleteSaveData()
    {
        if (saveDataExists)
        {
            File.Delete(filePath);
            fileName = "";
            saveDataExists = false;
        }
    }

    public void SaveFlag(string partName, int flag)
    {
        if (saveDataFlags.ContainsKey(partName))
        {
            saveDataFlags.Remove(partName);
            saveDataFlags.Add(partName, flag);
        }
        SaveDataToFile(fileName, saveDataFlags);
    }

    public bool GetFlagData(string partName, int flag)
    {
        bool hasData = false;
        foreach (KeyValuePair<string, int> entry in saveDataFlags)
        {
            if (entry.Key == partName && entry.Value == flag)
            {
                hasData = true;
            }
        }
        return hasData;
    }

    public static SaveDataManager GetInstance()
    {
        return instance;
    }
}
