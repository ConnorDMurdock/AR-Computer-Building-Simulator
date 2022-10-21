using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateNewFile : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void CreateNewSaveFile()
    {
        if (nameInput.text != "")
        {
            SaveDataManager.GetInstance().CreateNewSaveFile(nameInput.text);
            Debug.Log(nameInput.text);
            MainMenuCameraManager.GetInstance().RotateMenuLeft(1);
            nameInput.text = "";
        }
    }
}
