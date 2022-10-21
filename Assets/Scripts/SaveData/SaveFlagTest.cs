using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFlagTest : MonoBehaviour
{
    public string flagName;
    [Range(0, 1)]
    public int flagValue;

    private void Start()
    {
        bool partAlreadyInstalled = SaveDataManager.GetInstance().GetFlagData(flagName, flagValue);
        if (partAlreadyInstalled)
        {
            Debug.Log("part: " + flagName + " has already been installed!");
        }
        else
        {
            Debug.Log("This part has not yet been installed.");
        }
    }

    public void SaveFlag()
    {
        SaveDataManager.GetInstance().SaveFlag(flagName, flagValue);
    }
}
