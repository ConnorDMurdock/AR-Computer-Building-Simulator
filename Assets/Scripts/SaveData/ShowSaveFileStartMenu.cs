using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSaveFileStartMenu : MonoBehaviour
{
    private bool doesSaveDataExist;

    public GameObject newFile;
    public GameObject savedFile;

    public TMP_Text fileName;
    public TMP_Text numberOfBadges;

    private void Start()
    {
        CheckForFile();
    }

    public void CheckForFile()
    {
        doesSaveDataExist = SaveDataManager.GetInstance().saveDataExists;

        if (doesSaveDataExist)
        {
            newFile.SetActive(false);
            savedFile.SetActive(true);
            SaveDataManager.GetInstance().GetSaveData();
            fileName.text = SaveDataManager.GetInstance().GetFileName();
            numberOfBadges.text = SaveDataManager.GetInstance().GetNumberOfBadges().ToString();
        }
        else
        {
            savedFile.SetActive(false);
            newFile.SetActive(true);
        }
    }
}
